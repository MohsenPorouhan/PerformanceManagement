using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PerformanceManagement.Models.Coacher;
using PerformanceManagement.Models.HRAdmin;
using PerformanceManagement.Models.ICTAdmin;

namespace PerformanceManagement.Models
{
    public class AppDbContext : IdentityDbContext
    {
        public DbSet<EvaluationHierarchy> evaluationHierarchies { get; set; }
        public DbSet<ApplicationUser> applicationUsers { get; set; }
        public DbSet<People> People { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<PeriodDefinitoion> PeriodDefinitoion { get; set; }
        public DbSet<EvaluationCoefficient> EvaluationCoefficient { get; set; }
        public DbSet<SectionPeriod> SectionPeriod { get; set; }
        public DbSet<PersonPeriodEvaluationHierarchy> PersonPeriodEvaluationHierarchy { get; set; }
        public DbSet<LikertScale> LikertScale { get; set; }
        public DbSet<LikertDescribor> LikertDescribor { get; set; }
        public DbSet<ChartConfirmation> ChartConfirmation { get; set; }
        public DbSet<Task> Task { get; set; }
        public DbSet<Criteria> Criteria { get; set; }
        public DbSet<Evaluation> Evaluation { get; set; }
        public DbSet<EvaluationParticipant> EvaluationParticipant { get; set; }
        public DbSet<TrainingNeed> TrainingNeed { get; set; }
        public DbSet<CriteriaWeight> CriteriaWeight { get; set; }
        public DbSet<CriteriaWeightScore> CriteriaWeightScore { get; set; }
        public DbSet<EvaluationScore> EvaluationScore { get; set; }
        public DbSet<BehaviouralCompetency> BehaviouralCompetency { get; set; }
        public DbSet<Truth> Truth { get; set; }
        public DbSet<Job> Job { get; set; }
        public DbSet<CorrespondentJob> CorrespondentJob { get; set; }
        public DbSet<PublicTask> PublicTask { get; set; }
        public DbSet<EvaluationBehaviouralCompetency> EvaluationBehaviouralCompetency { get; set; }
        public DbSet<EvaluationBehaviourCompetencyScore> EvaluationBehaviourCompetencyScore { get; set; }
        public DbSet<EvaluationBehaviouralParticipant> EvaluationBehaviouralParticipant { get; set; }
        public DbSet<EvaluationAcceptanceStatus> EvaluationAcceptanceStatus { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<NotificationTitle> NotificationTitle { get; set; }
        public DbSet<EvaluationCalculation> EvaluationCalculation { get; set; }
        public DbSet<CriteriaCalculation> CriteriaCalculation { get; set; }
        public DbSet<FinalScoreCalculation> FinalScoreCalculation { get; set; }
        public DbSet<EvaluationCompetencyCalculation> EvaluationCompetencyCalculation { get; set; }
        public DbSet<FinalScoreCompetencyCalculation> FinalScoreCompetencyCalculation { get; set; }
        public DbSet<FinalizeCalculation> FinalizeCalculation { get; set; }
        public DbSet<SensibleEvent> SensibleEvent { get; set; }
        public DbSet<RelatedCompetencyWithSensibleEvent> RelatedCompetencyWithSensibleEvent { get; set; }
        public DbSet<RelatedTaskWithSensibleEvent> RelatedTaskWithSensibleEvent { get; set; }
        public DbSet<ExtendSectionPeriod> ExtendSectionPeriod { get; set; }
        public DbSet<ExtendSectionPeriodWithPeople> ExtendSectionPeriodWithPeople { get; set; }
        public DbSet<MultipleTaskCoacherOfEmployeeFinalCalc> MultipleTaskCoacherOfEmployeeFinalCalc { get; set; }
        public DbSet<MultipleCompetencyCoacherOfEmployeeFinalCalc> MultipleCompetencyCoacherOfEmployeeFinalCalc { get; set; }
        public DbSet<Protest> Protest { get; set; }
        public DbSet<ProtestResponse> ProtestResponse { get; set; }
        public DbSet<Addressee> Addressee { get; set; }
        public DbSet<CoacherTruth> CoacherTruth { get; set; }
        public DbSet<ScoreScheduleType> ScoreScheduleType { get; set; }
        public DbSet<ScoreSchedule> ScoreSchedule { get; set; }
        public DbSet<ExtendScoreSchedule> ExtendScoreSchedule { get; set; }
        public DbSet<ChartInfo> ChartInfo { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new EvaluationHierarchyConfig());
            builder.ApplyConfiguration(new DepartmentConfig());
            builder.ApplyConfiguration(new PeopleConfig());
            builder.ApplyConfiguration(new PeriodDefinitoionConfig());
            builder.ApplyConfiguration(new LikertScaleConfig());
            builder.ApplyConfiguration(new LikertDescriborConfig());
            builder.ApplyConfiguration(new ChartConfirmationConfig());
            builder.ApplyConfiguration(new TaskConfig());
            builder.ApplyConfiguration(new EvaluationConfig());
            builder.ApplyConfiguration(new TrainingNeedConfig());
            builder.ApplyConfiguration(new CriteriaConfig());
            builder.ApplyConfiguration(new CriteriaWeightScoreConfig());
            builder.ApplyConfiguration(new EvaluationScoreConfig());
            builder.ApplyConfiguration(new BehaviouralCompetencyConfig());
            builder.ApplyConfiguration(new TruthConfig());
            builder.ApplyConfiguration(new JobConfig());
            builder.ApplyConfiguration(new CorrespondentJobConfig());
            builder.ApplyConfiguration(new PublicTaskConfig());
            builder.ApplyConfiguration(new EvaluationBehaviouralCompetencyConfig());
            builder.ApplyConfiguration(new EvaluationBehaviourCompetencyScoreConfig());
            builder.ApplyConfiguration(new EvaluationBehaviouralParticipantConfig());
            builder.ApplyConfiguration(new EvaluationAcceptanceStatusConfig());
            builder.ApplyConfiguration(new NotificationConfig());
            builder.ApplyConfiguration(new NotificationTitleConfig());
            builder.ApplyConfiguration(new EvaluationCalculationConfig());
            builder.ApplyConfiguration(new CriteriaCalculationConfig());
            builder.ApplyConfiguration(new FinalScoreCalculationConfig());
            builder.ApplyConfiguration(new EvaluationCompetencyCalculationConfig());
            builder.ApplyConfiguration(new FinalScoreCompetencyCalculationConfig());
            builder.ApplyConfiguration(new FinalizeCalculationConfig());
            builder.ApplyConfiguration(new SensibleEventConfig());
            builder.ApplyConfiguration(new RelatedCompetencyWithSensibleEventConfig());
            builder.ApplyConfiguration(new RelatedTaskWithSensibleEventConfig());
            builder.ApplyConfiguration(new SectionPeriodConfig());
            builder.ApplyConfiguration(new ExtendSectionPeriodConfig());
            builder.ApplyConfiguration(new ExtendSectionPeriodWithPeopleConfig());
            builder.ApplyConfiguration(new MultipleTaskCoacherOfEmployeeFinalCalcConfig());
            builder.ApplyConfiguration(new MultipleCompetencyCoacherOfEmployeeFinalCalcConfig());
            builder.ApplyConfiguration(new ProtestConfig());
            builder.ApplyConfiguration(new ProtestResponseConfig());
            builder.ApplyConfiguration(new AddresseeConfig());
            builder.ApplyConfiguration(new CoacherTruthConfig());
            builder.ApplyConfiguration(new ScoreScheduleTypeConfig());
            builder.ApplyConfiguration(new ScoreScheduleConfig());
            builder.ApplyConfiguration(new ExtendScoreScheduleConfig());
            builder.ApplyConfiguration(new ChartInfoConfig());




            builder.Entity<People>().HasMany(c => c.EvaluationHierarchies).WithOne(c => c.Supervisor).HasForeignKey(c => new { c.SupervisorId, c.SupervisorEffectiveStartDate }).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<People>().HasMany(c => c.Createdby).WithOne(c => c.CreatedBy).HasForeignKey(c => new { c.CreatedById, c.CreatedByEffectiveStartDate }).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<People>().HasMany(c => c.LastUpdatedBy).WithOne(c => c.LastUpdatedBy).HasForeignKey(c => new { c.LastUpdatedById, c.LastUpdatedByEffectiveStartDate }).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<EvaluationHierarchy>().HasMany(c => c.peopledepartmentHierarchy).WithOne(c => c.evaluationHierarchy).HasForeignKey(c => new { c.EvaluationHierarchyID, c.EvaluationHierarchyDate }).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Department>().HasMany(c => c.EvaluationHierarchies).WithOne(c => c.Department).HasForeignKey(c => new { c.DepartmentId, c.DepartmentEffectiveStartDate }).OnDelete(DeleteBehavior.Restrict);


            //builder.Entity<EvaluationHierarchy>().HasIndex(c => c.Supervisor).IsUnique(false);
            //builder.Entity<EvaluationHierarchy>().HasMany(c => c.Supervisor).WithOne().OnDelete(DeleteBehavior.Restrict);
            //builder.Entity<EvaluationHierarchy>().HasKey(e => e.EvaluationHierarchyId);
            //builder.Entity<EvaluationHierarchy>().Property(e => e.EvaluationHierarchyId).ValueGeneratedNever();
            builder.Entity<EvaluationHierarchy>().HasOne(e => e.Parent).WithMany(e => e.Children).HasForeignKey(e => new { e.ParentEvaluationHierarchyId, e.ParentEffectiveStartDate });

            base.OnModelCreating(builder);
        }

        public DbQuery<AscendHierarchyWithSeparatorDepartmentSqlQuery> AscendHierarchyWithSeparatorDepartmentSqlQuery { get; set; }
    }
}
