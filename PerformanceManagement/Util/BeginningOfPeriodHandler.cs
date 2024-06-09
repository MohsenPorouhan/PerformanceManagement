
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PerformanceManagement.Models.HRAdmin;
using PerformanceManagement.Models.HRAdmin.Services;
using PerformanceManagement.ViewModels;

namespace PerformanceManagement.Util
{
    public class BeginningOfPeriodHandler : AuthorizationHandler<BeginningOfPeriodRequirement>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, BeginningOfPeriodRequirement requirement)
        {
            AccessControlDecisionViewModel acdvm = context.Resource as AccessControlDecisionViewModel;
            bool extendSection = false;
            ShareService shareService = new ShareService(acdvm.AppDbContext, null);
            SectionPeriod sectionPeriod = shareService.BeginOfPeriod();
            List<ExtendSectionPeriod> extendSectionPeriod = acdvm.AppDbContext.ExtendSectionPeriod.
                Where(c => c.SectionPeriodId == sectionPeriod.SectionPeriodId).ToList();
            if (extendSectionPeriod.Count > 0)
            {
                foreach (var item in extendSectionPeriod)
                {
                    ExtendSectionPeriodWithPeople espwp = acdvm.AppDbContext.ExtendSectionPeriodWithPeople.Include
                        (c => c.ExtendSectionPeriod).Where(c =>
                           c.ExtendSectionPeriod.DateFrom <= DateTime.Now
                        && c.ExtendSectionPeriod.DateTo >= DateTime.Now
                        && c.ExtendSectionPeriodId == item.ExtendSectionPeriodId
                        && c.EvaluationHierarchyId == acdvm.DepartmentId
                        && c.PeopleId == acdvm.PeopleId).SingleOrDefault();
                    if (espwp != null)
                    {
                        extendSection = true;
                        break;
                    }
                }
            }
            int periodDefinitionId = acdvm.PeriodDefinitionId ?? 0;
            if (((DateTime.Now >= sectionPeriod.DateFrom && DateTime.Now <= sectionPeriod.DateTo) || extendSection) &&
                periodDefinitionId == shareService.GetMaxPeriodDefinitionId())
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
