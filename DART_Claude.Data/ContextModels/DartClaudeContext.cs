using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DART_Claude.Data.ContextModels;

public partial class DartClaudeContext : DbContext
{
    public DartClaudeContext(DbContextOptions<DartClaudeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AppCurrentEmployee> AppCurrentEmployees { get; set; }

    public virtual DbSet<DartAppType> DartAppTypes { get; set; }

    public virtual DbSet<DartApplication> DartApplications { get; set; }

    public virtual DbSet<DartCriticality> DartCriticalities { get; set; }

    public virtual DbSet<DartDependency> DartDependencies { get; set; }

    public virtual DbSet<DartFeedback> DartFeedbacks { get; set; }

    public virtual DbSet<DartFeedbackFile> DartFeedbackFiles { get; set; }

    public virtual DbSet<DartPath> DartPaths { get; set; }

    public virtual DbSet<DartPathType> DartPathTypes { get; set; }

    public virtual DbSet<DartSdlcPhase> DartSdlcPhases { get; set; }

    public virtual DbSet<DartTelemetry> DartTelemetries { get; set; }

    public virtual DbSet<VwDartApplicationList> VwDartApplicationLists { get; set; }

    public virtual DbSet<VwDartDependencyList> VwDartDependencyLists { get; set; }

    public virtual DbSet<VwDartPathList> VwDartPathLists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppCurrentEmployee>(entity =>
        {
            entity.HasKey(e => e.EmpId).HasName("PK__APP_Curr__262359AB36BEE8E4");

            entity.ToTable("APP_CurrentEmployees");

            entity.Property(e => e.EmpId)
                .ValueGeneratedNever()
                .HasColumnName("Emp_Id");
            entity.Property(e => e.EmpPreferredName)
                .HasMaxLength(500)
                .HasColumnName("Emp_PreferredName");
        });

        modelBuilder.Entity<DartAppType>(entity =>
        {
            entity.HasKey(e => e.TypeId);

            entity.ToTable("DART_AppTypes");

            entity.Property(e => e.TypeId).HasColumnName("Type_Id");
            entity.Property(e => e.TypeName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Type_Name");
        });

        modelBuilder.Entity<DartApplication>(entity =>
        {
            entity.HasKey(e => e.AppId);

            entity.ToTable("DART_Applications");

            entity.Property(e => e.AppId).HasColumnName("App_Id");
            entity.Property(e => e.AppAllowFeedback).HasColumnName("App_AllowFeedback");
            entity.Property(e => e.AppAnalystEmpId).HasColumnName("App_Analyst_EmpId");
            entity.Property(e => e.AppCriticalityId).HasColumnName("App_CriticalityId");
            entity.Property(e => e.AppCurrentVersion)
                .HasColumnType("decimal(6, 4)")
                .HasColumnName("App_CurrentVersion");
            entity.Property(e => e.AppDescription)
                .IsUnicode(false)
                .HasColumnName("App_Description");
            entity.Property(e => e.AppFriendlyName)
                .IsUnicode(false)
                .HasColumnName("App_FriendlyName");
            entity.Property(e => e.AppName)
                .IsUnicode(false)
                .HasColumnName("App_Name");
            entity.Property(e => e.AppPrimDeveloperEmpId).HasColumnName("App_PrimDeveloper_EmpId");
            entity.Property(e => e.AppSdlcCheckDate)
                .HasColumnType("datetime")
                .HasColumnName("App_SDLC_CheckDate");
            entity.Property(e => e.AppSdlcPhaseId).HasColumnName("App_SDLC_Phase_Id");
            entity.Property(e => e.AppSecondaryDeveloperEmpId).HasColumnName("App_SecondaryDeveloper_EmpId");
            entity.Property(e => e.AppType).HasColumnName("App_Type");
            entity.Property(e => e.CreateBy).HasColumnName("Create_By");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("Create_Date");
            entity.Property(e => e.UpdateBy).HasColumnName("Update_By");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("Update_Date");

            entity.HasOne(d => d.AppAnalystEmp).WithMany(p => p.DartApplicationAppAnalystEmps)
                .HasForeignKey(d => d.AppAnalystEmpId)
                .HasConstraintName("FK_DART_Applications_APP_CurrentEmployees2");

            entity.HasOne(d => d.AppCriticality).WithMany(p => p.DartApplications)
                .HasForeignKey(d => d.AppCriticalityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DART_Applications_DART_Criticality");

            entity.HasOne(d => d.AppPrimDeveloperEmp).WithMany(p => p.DartApplicationAppPrimDeveloperEmps)
                .HasForeignKey(d => d.AppPrimDeveloperEmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DART_Applications_APP_CurrentEmployees");

            entity.HasOne(d => d.AppSdlcPhase).WithMany(p => p.DartApplications)
                .HasForeignKey(d => d.AppSdlcPhaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DART_Applications_DART_SDLCphase");

            entity.HasOne(d => d.AppSecondaryDeveloperEmp).WithMany(p => p.DartApplicationAppSecondaryDeveloperEmps)
                .HasForeignKey(d => d.AppSecondaryDeveloperEmpId)
                .HasConstraintName("FK_DART_Applications_APP_CurrentEmployees1");

            entity.HasOne(d => d.AppTypeNavigation).WithMany(p => p.DartApplications)
                .HasForeignKey(d => d.AppType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DART_Applications_DART_AppTypes");
        });

        modelBuilder.Entity<DartCriticality>(entity =>
        {
            entity.HasKey(e => e.CritId);

            entity.ToTable("DART_Criticality");

            entity.Property(e => e.CritId).HasColumnName("Crit_Id");
            entity.Property(e => e.CritName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Crit_Name");
        });

        modelBuilder.Entity<DartDependency>(entity =>
        {
            entity.HasKey(e => new { e.DependAppId, e.DependOnId });

            entity.ToTable("DART_Dependencies");

            entity.Property(e => e.DependAppId).HasColumnName("Depend_App_Id");
            entity.Property(e => e.DependOnId).HasColumnName("Depend_On_Id");

            entity.HasOne(d => d.DependOn).WithMany(p => p.DartDependencies)
                .HasForeignKey(d => d.DependOnId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DART_Dependencies_DART_Applications");
        });

        modelBuilder.Entity<DartFeedback>(entity =>
        {
            entity.HasKey(e => e.FbId);

            entity.ToTable("DART_Feedback");

            entity.Property(e => e.FbId).HasColumnName("FB_Id");
            entity.Property(e => e.CreateBy).HasColumnName("Create_By");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("Create_Date");
            entity.Property(e => e.FbAppId).HasColumnName("FB_App_Id");
            entity.Property(e => e.FbDescription)
                .IsUnicode(false)
                .HasColumnName("FB_Description");
            entity.Property(e => e.FbFollowupComplete).HasColumnName("FB_FollowupComplete");
            entity.Property(e => e.FbImplementedComments)
                .IsUnicode(false)
                .HasColumnName("FB_ImplementedComments");
            entity.Property(e => e.FbImplementedInVersion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FB_ImplementedInVersion");
            entity.Property(e => e.FbIsActive).HasColumnName("FB_isActive");
            entity.Property(e => e.FbWasImplemented).HasColumnName("FB_WasImplemented");
            entity.Property(e => e.UpdateBy).HasColumnName("Update_By");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("Update_Date");

            entity.HasOne(d => d.CreateByNavigation).WithMany(p => p.DartFeedbacks)
                .HasForeignKey(d => d.CreateBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DART_Feedback_APP_CurrentEmployees");
        });

        modelBuilder.Entity<DartFeedbackFile>(entity =>
        {
            entity.HasKey(e => e.FfId);

            entity.ToTable("DART_FeedbackFiles");

            entity.Property(e => e.FfId).HasColumnName("FF_Id");
            entity.Property(e => e.FfCreateBy).HasColumnName("FF_CreateBy");
            entity.Property(e => e.FfCreateDate)
                .HasColumnType("datetime")
                .HasColumnName("FF_CreateDate");
            entity.Property(e => e.FfDartFeedbackId).HasColumnName("FF_DartFeedbackId");
            entity.Property(e => e.FfFilePathway)
                .HasMaxLength(4000)
                .HasColumnName("FF_FilePathway");
            entity.Property(e => e.FfUpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("FF_UpdateDate");
            entity.Property(e => e.FfUpdatedBy).HasColumnName("FF_UpdatedBy");
        });

        modelBuilder.Entity<DartPath>(entity =>
        {
            entity.HasKey(e => e.PathId);

            entity.ToTable("DART_Paths");

            entity.Property(e => e.PathId).HasColumnName("Path_Id");
            entity.Property(e => e.CreateBy).HasColumnName("Create_By");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("Create_Date");
            entity.Property(e => e.PathAppId).HasColumnName("Path_App_Id");
            entity.Property(e => e.PathLocation)
                .IsUnicode(false)
                .HasColumnName("Path_Location");
            entity.Property(e => e.PathTypeId).HasColumnName("Path_Type_Id");
            entity.Property(e => e.UpdateBy).HasColumnName("Update_By");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("Update_Date");
        });

        modelBuilder.Entity<DartPathType>(entity =>
        {
            entity.HasKey(e => e.PathTypeId);

            entity.ToTable("DART_PathTypes");

            entity.Property(e => e.PathTypeId).HasColumnName("PathType_Id");
            entity.Property(e => e.PathTypeName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PathType_Name");
        });

        modelBuilder.Entity<DartSdlcPhase>(entity =>
        {
            entity.HasKey(e => e.SdlcId).HasName("PK_DART_SDLCphase");

            entity.ToTable("DART_SDLC_Phase");

            entity.Property(e => e.SdlcId).HasColumnName("SDLC_Id");
            entity.Property(e => e.SdlcName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("SDLC_Name");
        });

        modelBuilder.Entity<DartTelemetry>(entity =>
        {
            entity.HasKey(e => e.TeleId);

            entity.ToTable("DART_Telemetry");

            entity.Property(e => e.TeleId).HasColumnName("Tele_Id");
            entity.Property(e => e.TeleAppId).HasColumnName("Tele_App_Id");
            entity.Property(e => e.TeleCreateDate)
                .HasColumnType("datetime")
                .HasColumnName("Tele_Create_Date");
            entity.Property(e => e.TeleMethodName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("Tele_MethodName");
        });

        modelBuilder.Entity<VwDartApplicationList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_DART_ApplicationList");

            entity.Property(e => e.Analyst).HasMaxLength(500);
            entity.Property(e => e.AppType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApplicationName).IsUnicode(false);
            entity.Property(e => e.Criticality)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PrimaryDeveloper).HasMaxLength(500);
            entity.Property(e => e.SdlcPhase)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.SecondaryDeveloper).HasMaxLength(500);
        });

        modelBuilder.Entity<VwDartDependencyList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_DART_DependencyList");

            entity.Property(e => e.DependOnAppName).IsUnicode(false);
        });

        modelBuilder.Entity<VwDartPathList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_DART_PathList");

            entity.Property(e => e.PathLocation)
                .IsUnicode(false)
                .HasColumnName("Path_Location");
            entity.Property(e => e.PathTypeName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PathType_Name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
