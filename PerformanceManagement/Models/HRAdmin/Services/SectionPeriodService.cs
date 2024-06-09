using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PerformanceManagement.Models.HRAdmin.View;
using PerformanceManagement.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models.HRAdmin.Services
{
    public class SectionPeriodService
    {
        private readonly IConnProvider connProvider;
        private readonly AppDbContext appDbContext;

        //private readonly IConfiguration config;

        public SectionPeriodService(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public List<SectionPeriod> SectionPeriodList(int periodDefinitionId)
        {
            var query = appDbContext.SectionPeriod.Where(c => c.PeriodDefinitoionId == periodDefinitionId);
            if (query != null)
            {
                return query.OrderBy(c => c.StatusCode).ToList();
            }
            return (null);
        }
        public List<SectionPeriod> GetSections(int periodDefinitoionId)
        {
            List<SectionPeriod> sectionPeriod = appDbContext.SectionPeriod.Include(c => c.periodDefinitoion).Where(c => c.PeriodDefinitoionId == periodDefinitoionId).OrderBy(c => c.StatusCode).ToList();
            return sectionPeriod;
        }
        public int EditPeriodDefinition(PeriodDefinitoionView periodDefinitoionView, int personId)
        {
            DateTimeCustom dateTimeCustom = new DateTimeCustom();

            TimeSpan timeSpan = new TimeSpan(0, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);

            DateTime periodFrom = dateTimeCustom.Shamsi2Miladi(periodDefinitoionView.PeriodFrom);
            DateTime periodTo = dateTimeCustom.Shamsi2Miladi(periodDefinitoionView.PeriodTo);
            DateTime initialDateFrom = dateTimeCustom.Shamsi2Miladi(periodDefinitoionView.InitialDateFrom);
            DateTime initialDateTo = dateTimeCustom.Shamsi2Miladi(periodDefinitoionView.InitialDateTo);
            DateTime intermediateFrom = dateTimeCustom.Shamsi2Miladi(periodDefinitoionView.InitialDateTo).AddDays(1);
            DateTime intermediateTo = dateTimeCustom.Shamsi2Miladi(periodDefinitoionView.FinalDateFrom).AddDays(-1);
            DateTime finalDateFrom = dateTimeCustom.Shamsi2Miladi(periodDefinitoionView.FinalDateFrom);
            DateTime finalDateTo = dateTimeCustom.Shamsi2Miladi(periodDefinitoionView.FinalDateTo);
            DateTime protestDateFrom = dateTimeCustom.Shamsi2Miladi(periodDefinitoionView.ProtestDateFrom);
            DateTime protestDateTo = dateTimeCustom.Shamsi2Miladi(periodDefinitoionView.ProtestDateTo);

            PeriodDefinitoion periodDefinitoion = appDbContext.PeriodDefinitoion.Where(c => c.PeriodDefinitoionId == periodDefinitoionView.PeriodDefinitoionId).SingleOrDefault();
            SectionPeriod initialSection = appDbContext.SectionPeriod.Where(c => c.PeriodDefinitoionId == periodDefinitoionView.PeriodDefinitoionId &&
              c.StatusCode == 1).SingleOrDefault();
            SectionPeriod intermediateSection = appDbContext.SectionPeriod.Where(c => c.PeriodDefinitoionId == periodDefinitoionView.PeriodDefinitoionId &&
              c.StatusCode == 2).SingleOrDefault();
            SectionPeriod finalSection = appDbContext.SectionPeriod.Where(c => c.PeriodDefinitoionId == periodDefinitoionView.PeriodDefinitoionId &&
              c.StatusCode == 3).SingleOrDefault();
            SectionPeriod protestSection = appDbContext.SectionPeriod.Where(c => c.PeriodDefinitoionId == periodDefinitoionView.PeriodDefinitoionId &&
              c.StatusCode == 4).SingleOrDefault();

            UpdatePeriodDefinition(timeSpan, periodFrom, periodTo, periodDefinitoion);
            UpdateInitialSection(timeSpan, initialDateFrom, initialDateTo, initialSection);
            UpdateIntermediateSection(timeSpan, intermediateFrom, intermediateTo, intermediateSection);
            UpdateFinalSection(timeSpan, finalDateFrom, finalDateTo, finalSection);
            UpdateProtestSection(timeSpan, protestDateFrom, protestDateTo, protestSection);

            int result = appDbContext.SaveChanges();
            return result;
        }

        public string DeletePeriodDefinition(int periodDefinitionId)
        {
            try
            {
                List<PersonPeriodEvaluationHierarchy> personPeriodEvaluationHierarchies = appDbContext.PersonPeriodEvaluationHierarchy.Where(c => c.PeriodDefinitoionId == periodDefinitionId).ToList();
                appDbContext.RemoveRange(personPeriodEvaluationHierarchies);
                List<SectionPeriod> sectionPeriods = appDbContext.SectionPeriod.Where(c => c.PeriodDefinitoionId == periodDefinitionId).ToList();
                appDbContext.RemoveRange(sectionPeriods);
                PeriodDefinitoion periodDefinitoion = appDbContext.PeriodDefinitoion.Where(c => c.PeriodDefinitoionId == periodDefinitionId).SingleOrDefault();
                appDbContext.Remove(periodDefinitoion);
                int result = appDbContext.SaveChanges();
                return result.ToString();
            }
            catch (SqlException se)
            {
                return se.Message;
            }
            catch (DbUpdateException due)
            {
                return "به دلیل تعریف یا اختصاص اهداف یا شایستگی های رفتاری امکان حذف دوره مورد نظر وجود ندارد";
            }
            catch (Exception e2)
            {
                return e2.Message;
            }
        }

        public int ExtendSectionEdition(int extendSectionPeriodId, DateTime? dateFrom, DateTime? dateTo)
        {
            ExtendSectionPeriod extendSectionPeriod = appDbContext.ExtendSectionPeriod.Where(c => c.ExtendSectionPeriodId == extendSectionPeriodId).SingleOrDefault();

            extendSectionPeriod.DateFrom = dateFrom;
            extendSectionPeriod.DateTo = dateTo;

            appDbContext.Update(extendSectionPeriod);

            int result = appDbContext.SaveChanges();
            return result;
        }

        public int ExtendFinalSectionEdition(int extendSectionPeriodId, DateTime? dateFrom, DateTime? dateTo, DateTime selfScoreDateFrom, DateTime selfScoreDateTo
            , DateTime participantScoreDateFrom, DateTime participantScoreDateTo, DateTime coacher1ScoreDateFrom, DateTime coacher1ScoreDateTo
            , DateTime coacher2ScoreDateFrom, DateTime coacher2ScoreDateTo, int personId)
        {
            ExtendSectionPeriod extendSectionPeriod = appDbContext.ExtendSectionPeriod.Where(c => c.ExtendSectionPeriodId == extendSectionPeriodId).SingleOrDefault();

            extendSectionPeriod.DateFrom = dateFrom;
            extendSectionPeriod.DateTo = dateTo;

            appDbContext.Update(extendSectionPeriod);

            List<ExtendScoreSchedule> extendScoreSchedule = appDbContext.ExtendScoreSchedule.Where(c => c.ExtendSectionPeriodId == extendSectionPeriodId).ToList();
            foreach (var item in extendScoreSchedule)
            {
                switch (item.ScoreScheduleTypeId)
                {
                    case 1:
                        if (item.DateFrom != selfScoreDateFrom || item.DateTo != selfScoreDateTo)
                        {
                            item.DateFrom = selfScoreDateFrom;
                            item.DateTo = selfScoreDateTo;
                            item.LastUpdatedBy = personId;
                            item.LastUpdatedDate = DateTime.Now;
                        }
                        break;
                    case 2:
                        if (item.DateFrom != participantScoreDateFrom || item.DateTo != participantScoreDateTo)
                        {
                            item.DateFrom = participantScoreDateFrom;
                            item.DateTo = participantScoreDateTo;
                            item.LastUpdatedBy = personId;
                            item.LastUpdatedDate = DateTime.Now;
                        }
                        break;
                    case 3:
                        if (item.DateFrom != coacher1ScoreDateFrom || item.DateTo != coacher1ScoreDateTo)
                        {
                            item.DateFrom = coacher1ScoreDateFrom;
                            item.DateTo = coacher1ScoreDateTo;
                            item.LastUpdatedBy = personId;
                            item.LastUpdatedDate = DateTime.Now;
                        }
                        break;
                    case 4:
                        if (item.DateFrom != coacher2ScoreDateFrom || item.DateTo != coacher2ScoreDateTo)
                        {
                            item.DateFrom = coacher2ScoreDateFrom;
                            item.DateTo = coacher2ScoreDateTo;
                            item.LastUpdatedBy = personId;
                            item.LastUpdatedDate = DateTime.Now;
                        }
                        break;
                    default:
                        break;
                }
                appDbContext.Update(item);
            }
            int result = appDbContext.SaveChanges();
            return result;
        }

        public string ExtendSectionDeletion(int extendSectionPeriodId)
        {
            try
            {
                List<ExtendSectionPeriodWithPeople> extendSectionPeriodWithPeople = appDbContext.ExtendSectionPeriodWithPeople.Where(c => c.ExtendSectionPeriodId == extendSectionPeriodId).ToList();
                appDbContext.RemoveRange(extendSectionPeriodWithPeople);
                List<ExtendScoreSchedule> extendScoreSchedules = appDbContext.ExtendScoreSchedule.Where(c => c.ExtendSectionPeriodId == extendSectionPeriodId).ToList();
                appDbContext.RemoveRange(extendScoreSchedules);
                ExtendSectionPeriod extendSectionPeriod = appDbContext.ExtendSectionPeriod.Where(c => c.ExtendSectionPeriodId == extendSectionPeriodId).SingleOrDefault();
                appDbContext.Remove(extendSectionPeriod);
                int result = appDbContext.SaveChanges();
                return result.ToString();
            }
            catch (SqlException se)
            {
                return se.Message;
            }
            catch (DbUpdateException due)
            {
                return due.Message;
            }
            catch (Exception e2)
            {
                return e2.Message;
            }
        }

        public string RelatedPeopleWithSectionDeletion(int ExtendSectionPeriodWithPeopleId)
        {
            try
            {
                ExtendSectionPeriodWithPeople extendSectionPeriodWithPeople = appDbContext.ExtendSectionPeriodWithPeople.Where(c => c.ExtendSectionPeriodWithPeopleId == ExtendSectionPeriodWithPeopleId).SingleOrDefault();
                appDbContext.Remove(extendSectionPeriodWithPeople);
                int result = appDbContext.SaveChanges();
                return result.ToString();
            }
            catch (SqlException se)
            {
                return se.Message;
            }
            catch (DbUpdateException due)
            {
                return due.Message;
            }
            catch (Exception e2)
            {
                return e2.Message;
            }
        }
        private void UpdateProtestSection(TimeSpan timeSpan, DateTime protestDateFrom, DateTime protestDateTo, SectionPeriod protestSection)
        {
            if (protestDateFrom != protestSection.DateFrom.Date)
            {
                protestSection.DateFrom = protestDateFrom + timeSpan;
                appDbContext.Update(protestSection);
            }
            if (protestDateTo != protestSection.DateTo.Date)
            {
                protestSection.DateTo = protestDateTo + timeSpan;
                appDbContext.Update(protestSection);
            }
        }

        private void UpdateFinalSection(TimeSpan timeSpan, DateTime finalDateFrom, DateTime finalDateTo, SectionPeriod finalSection)
        {
            if (finalDateFrom != finalSection.DateFrom.Date)
            {
                finalSection.DateFrom = finalDateFrom + timeSpan;
                appDbContext.Update(finalSection);
            }
            if (finalDateTo != finalSection.DateTo.Date)
            {
                finalSection.DateTo = finalDateTo + timeSpan;
                appDbContext.Update(finalSection);
            }
        }

        private void UpdateIntermediateSection(TimeSpan timeSpan, DateTime intermediateFrom, DateTime intermediateTo, SectionPeriod intermediateSection)
        {
            if (intermediateFrom != intermediateSection.DateFrom.Date)
            {
                intermediateSection.DateFrom = intermediateFrom + timeSpan;
                appDbContext.Update(intermediateSection);
            }
            if (intermediateTo != intermediateSection.DateTo.Date)
            {
                intermediateSection.DateTo = intermediateTo + timeSpan;
                appDbContext.Update(intermediateSection);
            }
        }

        private void UpdateInitialSection(TimeSpan timeSpan, DateTime initialDateFrom, DateTime initialDateTo, SectionPeriod initialSection)
        {
            if (initialDateFrom != initialSection.DateFrom.Date)
            {
                initialSection.DateFrom = initialDateFrom + timeSpan;
                appDbContext.Update(initialSection);
            }
            if (initialDateTo != initialSection.DateTo.Date)
            {
                initialSection.DateTo = initialDateTo + timeSpan;
                appDbContext.Update(initialSection);
            }
        }

        private void UpdatePeriodDefinition(TimeSpan timeSpan, DateTime periodFrom, DateTime periodTo, PeriodDefinitoion periodDefinitoion)
        {
            if (periodFrom != periodDefinitoion.DateFrom.Date)
            {
                periodDefinitoion.DateFrom = periodFrom + timeSpan;
                appDbContext.Update(periodDefinitoion);
            }
            if (periodTo != periodDefinitoion.DateTo.Date)
            {
                periodDefinitoion.DateTo = periodTo + timeSpan;
                appDbContext.Update(periodDefinitoion);
            }
        }
    }
}
