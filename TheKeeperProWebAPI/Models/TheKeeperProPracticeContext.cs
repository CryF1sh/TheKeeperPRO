using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TheKeeperProWebAPI.Models;

public partial class TheKeeperProPracticeContext : DbContext
{
    public TheKeeperProPracticeContext()
    {
    }

    public TheKeeperProPracticeContext(DbContextOptions<TheKeeperProPracticeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Division> Divisions { get; set; }

    public virtual DbSet<Employer> Employers { get; set; }

    public virtual DbSet<ListOfVisiter> ListOfVisiters { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<RequestStatus> RequestStatuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VisitPurpose> VisitPurposes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=vpmt.ru\\is20;Initial Catalog=TheKeeperPro_practice;User ID=user30;Password=user30;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("Department");

            entity.Property(e => e.DepartmentId).HasColumnName("departmentId");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Division>(entity =>
        {
            entity.ToTable("Division");

            entity.Property(e => e.DivisionId).HasColumnName("divisionId");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Employer>(entity =>
        {
            entity.HasKey(e => e.EmployeeId);

            entity.Property(e => e.EmployeeId).HasColumnName("employeeId");
            entity.Property(e => e.DepartmentId).HasColumnName("departmentId");
            entity.Property(e => e.DivisionId).HasColumnName("divisionId");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .HasColumnName("surname");

            entity.HasOne(d => d.Department).WithMany(p => p.Employers)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_Employers_Department");

            entity.HasOne(d => d.Division).WithMany(p => p.Employers)
                .HasForeignKey(d => d.DivisionId)
                .HasConstraintName("FK_Employers_Division");
        });

        modelBuilder.Entity<ListOfVisiter>(entity =>
        {
            entity.HasKey(e => e.ListOfVisitersId);

            entity.Property(e => e.ListOfVisitersId).HasColumnName("listOfVisitersId");
            entity.Property(e => e.Creater).HasColumnName("creater");
            entity.Property(e => e.RequestId).HasColumnName("requestId");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Request).WithMany(p => p.ListOfVisiters)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ListOfVisiters_Request");

            entity.HasOne(d => d.User).WithMany(p => p.ListOfVisiters)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ListOfVisiters_User");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.ToTable("Message");

            entity.Property(e => e.MessageId).HasColumnName("messageId");
            entity.Property(e => e.Message1).HasColumnName("message");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Messages)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Message_User");
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.ToTable("Request");

            entity.Property(e => e.RequestId).HasColumnName("requestId");
            entity.Property(e => e.DesiredEndDate)
                .HasColumnType("date")
                .HasColumnName("desiredEndDate");
            entity.Property(e => e.DesiredStartDate)
                .HasColumnType("date")
                .HasColumnName("desiredStartDate");
            entity.Property(e => e.EmployeeId).HasColumnName("employeeId");
            entity.Property(e => e.EndDate)
                .HasColumnType("date")
                .HasColumnName("endDate");
            entity.Property(e => e.GroupRequest).HasColumnName("groupRequest");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.Organisation)
                .HasMaxLength(200)
                .HasColumnName("organisation");
            entity.Property(e => e.QrCode)
                .HasMaxLength(13)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("qrCode");
            entity.Property(e => e.RequestStatusId).HasColumnName("requestStatusId");
            entity.Property(e => e.StartDate)
                .HasColumnType("date")
                .HasColumnName("startDate");
            entity.Property(e => e.TrueEndDate)
                .HasColumnType("date")
                .HasColumnName("trueEndDate");
            entity.Property(e => e.TrueEndDateInDivision)
                .HasColumnType("date")
                .HasColumnName("trueEndDateInDivision");
            entity.Property(e => e.TrueStartDate)
                .HasColumnType("date")
                .HasColumnName("trueStartDate");
            entity.Property(e => e.TrueStartDateInDivision)
                .HasColumnType("date")
                .HasColumnName("trueStartDateInDivision");
            entity.Property(e => e.VisitPurpouseId).HasColumnName("visitPurpouseId");

            entity.HasOne(d => d.Employee).WithMany(p => p.Requests)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Request_Employers");

            entity.HasOne(d => d.RequestStatus).WithMany(p => p.Requests)
                .HasForeignKey(d => d.RequestStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Request_RequestStatus");

            entity.HasOne(d => d.VisitPurpouse).WithMany(p => p.Requests)
                .HasForeignKey(d => d.VisitPurpouseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Request_VisitPurpose");
        });

        modelBuilder.Entity<RequestStatus>(entity =>
        {
            entity.ToTable("RequestStatus");

            entity.Property(e => e.RequestStatusId).HasColumnName("requestStatusId");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.DateOfBorn)
                .HasColumnType("date")
                .HasColumnName("dateOfBorn");
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .HasColumnName("login");
            entity.Property(e => e.Mail)
                .HasMaxLength(100)
                .HasColumnName("mail");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.OnBlackList).HasColumnName("onBlackList");
            entity.Property(e => e.PasportNumber)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("pasportNumber");
            entity.Property(e => e.PasportSeries)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("pasportSeries");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(50)
                .HasColumnName("patronymic");
            entity.Property(e => e.PhotoOfPasport)
                .HasMaxLength(2000)
                .IsFixedLength()
                .HasColumnName("photoOfPasport");
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .HasColumnName("surname");
            entity.Property(e => e.Telephone)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("telephone");
            entity.Property(e => e.VisitersPhote)
                .HasMaxLength(2000)
                .IsFixedLength()
                .HasColumnName("visitersPhote");
        });

        modelBuilder.Entity<VisitPurpose>(entity =>
        {
            entity.ToTable("VisitPurpose");

            entity.Property(e => e.VisitPurposeId).HasColumnName("visitPurposeId");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
