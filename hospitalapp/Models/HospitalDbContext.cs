using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace hospitalapp.Models;

public partial class HospitalDbContext : IdentityDbContext
{
    public HospitalDbContext()
    {

    }
    public HospitalDbContext(DbContextOptions<HospitalDbContext> options)
        : base(options)
    {
    }


    public virtual DbSet<Allergy> Allergies { get; set; }

    public virtual DbSet<AssignBed> AssignBeds { get; set; }

    public virtual DbSet<AssignDoctor> AssignDoctors { get; set; }

    public virtual DbSet<AssignNurse> AssignNurses { get; set; }

    public virtual DbSet<Bed> Beds { get; set; }

    public virtual DbSet<Billing> Billings { get; set; }

    public virtual DbSet<BillingCalculation> BillingCalculations { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<DiagnosticTest> DiagnosticTests { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Equipment> Equipment { get; set; }

    public virtual DbSet<HaveInsurance> HaveInsurances { get; set; }

    public virtual DbSet<Insurance> Insurances { get; set; }

    public virtual DbSet<Maintain> Maintains { get; set; }

    public virtual DbSet<MedicalHistory> MedicalHistories { get; set; }

    public virtual DbSet<Nurse> Nurses { get; set; }

    public virtual DbSet<OtherEmployee> OtherEmployees { get; set; }

    public virtual DbSet<PatientRecord> PatientRecords { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PerformSurgery> PerformSurgeries { get; set; }

    public virtual DbSet<PerformTest> PerformTests { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Surgery> Surgeries { get; set; }

    public virtual DbSet<Technician> Technicians { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-DKGNHNC;Database=HospitalDB;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Allergy>(entity =>
        {
            entity.HasKey(e => new { e.RecordId, e.Allergy1 }).HasName("PK__ALLERGY__0AE675BD31AABFA7");

            entity.ToTable("ALLERGY");

            entity.Property(e => e.RecordId).HasColumnName("RecordID");
            entity.Property(e => e.Allergy1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Allergy");

            entity.HasOne(d => d.Record).WithMany(p => p.Allergies)
                .HasForeignKey(d => d.RecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ALLERGY__RecordI__114A936A");
        });

        modelBuilder.Entity<AssignBed>(entity =>
        {
            entity.HasKey(e => new { e.BedNumber, e.RoomId }).HasName("PK_ASSIGN_BED_Bed_Room");

            entity.ToTable("ASSIGN_BED");

            entity.Property(e => e.RoomId).HasColumnName("RoomID");
            entity.Property(e => e.PatientId).HasColumnName("PatientID");

            entity.HasOne(d => d.Patient).WithMany(p => p.AssignBeds)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ASSIGN_BE__Patie__0D7A0286");

            entity.HasOne(d => d.Bed).WithOne(p => p.AssignBed)
                .HasForeignKey<AssignBed>(d => new { d.BedNumber, d.RoomId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ASSIGN_BED__0E6E26BF");
        });

        modelBuilder.Entity<AssignDoctor>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PK__ASSIGN_D__FBDF78C9C6C560E8");

            entity.ToTable("ASSIGN_DOCTOR");

            entity.Property(e => e.RecordId)
                .ValueGeneratedNever()
                .HasColumnName("RecordID");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

            entity.HasOne(d => d.Doctor).WithMany(p => p.AssignDoctors)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK__ASSIGN_DO__Docto__00200768");

            entity.HasOne(d => d.Record).WithOne(p => p.AssignDoctor)
                .HasForeignKey<AssignDoctor>(d => d.RecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ASSIGN_DO__Recor__01142BA1");
        });

        modelBuilder.Entity<AssignNurse>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PK__ASSIGN_N__FBDF78C9270F944F");

            entity.ToTable("ASSIGN_NURSE");

            entity.Property(e => e.RecordId)
                .ValueGeneratedNever()
                .HasColumnName("RecordID");
            entity.Property(e => e.NurseId).HasColumnName("NurseID");

            entity.HasOne(d => d.Nurse).WithMany(p => p.AssignNurses)
                .HasForeignKey(d => d.NurseId)
                .HasConstraintName("FK__ASSIGN_NU__Nurse__7C4F7684");

            entity.HasOne(d => d.Record).WithOne(p => p.AssignNurse)
                .HasForeignKey<AssignNurse>(d => d.RecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ASSIGN_NU__Recor__7D439ABD");
        });

        modelBuilder.Entity<Bed>(entity =>
        {
            entity.HasKey(e => new { e.BedNumber, e.RoomId }).HasName("PK__BED__BFABAEC5589D7873");

            entity.ToTable("BED");

            entity.Property(e => e.RoomId).HasColumnName("RoomID");

            entity.HasOne(d => d.Room).WithMany(p => p.Beds)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK_Bed_Room");
        });

        modelBuilder.Entity<Billing>(entity =>
        {
            entity.HasKey(e => e.BillingId).HasName("PK__BILLING__F1656D13D0BD0732");

            entity.ToTable("BILLING");

            entity.Property(e => e.BillingId)
                .ValueGeneratedNever()
                .HasColumnName("BillingID");
            entity.Property(e => e.CoverAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InitialAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.StatusBill)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TypeBill)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasMany(d => d.Insurances).WithMany(p => p.Billings)
                .UsingEntity<Dictionary<string, object>>(
                    "Cover",
                    r => r.HasOne<Insurance>().WithMany()
                        .HasForeignKey("InsuranceId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__COVER__Insurance__1F98B2C1"),
                    l => l.HasOne<Billing>().WithMany()
                        .HasForeignKey("BillingId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__COVER__BillingID__1EA48E88"),
                    j =>
                    {
                        j.HasKey("BillingId", "InsuranceId").HasName("PK__COVER__A6275CAFC971B6B7");
                        j.ToTable("COVER");
                        j.IndexerProperty<int>("BillingId").HasColumnName("BillingID");
                        j.IndexerProperty<int>("InsuranceId").HasColumnName("InsuranceID");
                    });
        });

        modelBuilder.Entity<BillingCalculation>(entity =>
        {
            entity.HasKey(e => new { e.InitialAmount, e.CoverAmount }).HasName("PK__BILLING___F8197FB174D019F0");

            entity.ToTable("BILLING_CALCULATION");

            entity.Property(e => e.InitialAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CoverAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FinalAmount).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentNumber).HasName("PK__DEPARTME__718447F81FD8E035");

            entity.ToTable("DEPARTMENT");

            entity.Property(e => e.Location)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ManageId).HasColumnName("ManageID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Manage).WithMany(p => p.Departments)
                .HasForeignKey(d => d.ManageId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Department_ManageID");
        });

        modelBuilder.Entity<DiagnosticTest>(entity =>
        {
            entity.HasKey(e => e.TestId).HasName("PK__DIAGNOST__8CC33100531104A7");

            entity.ToTable("DIAGNOSTIC_TEST");

            entity.Property(e => e.TestId)
                .ValueGeneratedNever()
                .HasColumnName("TestID");
            entity.Property(e => e.TestDescription).HasMaxLength(255);
            entity.Property(e => e.TestName).HasMaxLength(100);
            entity.Property(e => e.TestResult).HasMaxLength(100);

            entity.HasMany(d => d.Equipment).WithMany(p => p.Tests)
                .UsingEntity<Dictionary<string, object>>(
                    "UsedInTest",
                    r => r.HasOne<Equipment>().WithMany()
                        .HasForeignKey("EquipmentId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__USED_IN_T__Equip__778AC167"),
                    l => l.HasOne<DiagnosticTest>().WithMany()
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__USED_IN_T__TestI__76969D2E"),
                    j =>
                    {
                        j.HasKey("TestId", "EquipmentId").HasName("PK__USED_IN___0F8745592BCE5257");
                        j.ToTable("USED_IN_TEST");
                        j.IndexerProperty<int>("TestId").HasColumnName("TestID");
                        j.IndexerProperty<int>("EquipmentId").HasColumnName("EquipmentID");
                    });
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("PK__DOCTOR__2DC00EDF45866AF2");

            entity.ToTable("DOCTOR");

            entity.Property(e => e.DoctorId)
                .ValueGeneratedNever()
                .HasColumnName("DoctorID");
            entity.Property(e => e.Certifica).HasMaxLength(100);
            entity.Property(e => e.Specialty).HasMaxLength(100);

            entity.HasOne(d => d.DoctorNavigation).WithOne(p => p.Doctor)
                .HasForeignKey<Doctor>(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Doctor_Employee");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__EMPLOYEE__7AD04FF192D17CCB");

            entity.ToTable("EMPLOYEE");

            entity.HasIndex(e => e.PhoneNumber, "UQ__EMPLOYEE__85FB4E38F160F089").IsUnique();

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Degree)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.JobType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Job_type");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Salary).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.StartDate).HasColumnName("startDate");

            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_Employee_Department");
        });

        modelBuilder.Entity<Equipment>(entity =>
        {
            entity.HasKey(e => e.EquipmentId).HasName("PK__EQUIPMEN__344745998B314AC3");

            entity.ToTable("EQUIPMENT");

            entity.Property(e => e.EquipmentId)
                .ValueGeneratedNever()
                .HasColumnName("EquipmentID");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(100);
        });

        modelBuilder.Entity<HaveInsurance>(entity =>
        {
            entity.HasKey(e => e.InsuranceId).HasName("PK__HAVE_INS__74231BC4B278FD52");

            entity.ToTable("HAVE_INSURANCE");

            entity.Property(e => e.InsuranceId)
                .ValueGeneratedNever()
                .HasColumnName("InsuranceID");
            entity.Property(e => e.PatientId).HasColumnName("PatientID");

            entity.HasOne(d => d.Insurance).WithOne(p => p.HaveInsurance)
                .HasForeignKey<HaveInsurance>(d => d.InsuranceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HAVE_INSU__Insur__1AD3FDA4");

            entity.HasOne(d => d.Patient).WithMany(p => p.HaveInsurances)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK__HAVE_INSU__Patie__1BC821DD");
        });

        modelBuilder.Entity<Insurance>(entity =>
        {
            entity.HasKey(e => e.InsuranceId).HasName("PK__INSURANC__74231BC4386E2AC0");

            entity.ToTable("INSURANCE");

            entity.Property(e => e.InsuranceId)
                .ValueGeneratedNever()
                .HasColumnName("InsuranceID");
            entity.Property(e => e.CoverLimit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CoverPercentage).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.PolicyNumber)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Provider)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.StatusInsurance)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Maintain>(entity =>
        {
            entity.HasKey(e => new { e.TechId, e.EquipId }).HasName("PK__MAINTAIN__0FF29AAEC6803016");

            entity.ToTable("MAINTAIN");

            entity.Property(e => e.TechId).HasColumnName("TechID");
            entity.Property(e => e.EquipId).HasColumnName("EquipID");
            entity.Property(e => e.TypeMaintain).HasMaxLength(100);

            entity.HasOne(d => d.Equip).WithMany(p => p.Maintains)
                .HasForeignKey(d => d.EquipId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MAINTAIN__EquipI__6E01572D");

            entity.HasOne(d => d.Tech).WithMany(p => p.Maintains)
                .HasForeignKey(d => d.TechId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MAINTAIN__TechID__6D0D32F4");
        });

        modelBuilder.Entity<MedicalHistory>(entity =>
        {
            entity.HasKey(e => new { e.RecordId, e.TypeName }).HasName("PK__MEDICAL___7691053354195B28");

            entity.ToTable("MEDICAL_HISTORY");

            entity.Property(e => e.RecordId).HasColumnName("RecordID");
            entity.Property(e => e.TypeName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.DescriptionDetail).HasColumnType("text");
            entity.Property(e => e.Stage)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Treatment)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Record).WithMany(p => p.MedicalHistories)
                .HasForeignKey(d => d.RecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MEDICAL_H__Recor__14270015");
        });

        modelBuilder.Entity<Nurse>(entity =>
        {
            entity.HasKey(e => e.NurseId).HasName("PK__NURSE__4384786957658202");

            entity.ToTable("NURSE");

            entity.Property(e => e.NurseId)
                .ValueGeneratedNever()
                .HasColumnName("NurseID");
            entity.Property(e => e.Specialty).HasMaxLength(100);

            entity.HasOne(d => d.NurseNavigation).WithOne(p => p.Nurse)
                .HasForeignKey<Nurse>(d => d.NurseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Nurse_Employee");
        });

        modelBuilder.Entity<OtherEmployee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__OTHER_EM__7AD04FF189F72C32");

            entity.ToTable("OTHER_EMPLOYEE");

            entity.Property(e => e.EmployeeId)
                .ValueGeneratedNever()
                .HasColumnName("EmployeeID");
            entity.Property(e => e.Specialty)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Employee).WithOne(p => p.OtherEmployee)
                .HasForeignKey<OtherEmployee>(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OTHER_EMP__Emplo__25518C17");
        });

        modelBuilder.Entity<PatientRecord>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PK__PATIENT___FBDF78C9D7BF653D");

            entity.ToTable("PATIENT_RECORD");

            entity.Property(e => e.RecordId)
                .ValueGeneratedNever()
                .HasColumnName("RecordID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.ContactInfo).HasMaxLength(255);
            entity.Property(e => e.CurrentMedication).HasMaxLength(255);
            entity.Property(e => e.EmerContactInfo).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.ReceiptId).HasName("PK__PAYMENT__CC08C400A8DE2AF1");

            entity.ToTable("PAYMENT");

            entity.Property(e => e.ReceiptId)
                .ValueGeneratedNever()
                .HasColumnName("ReceiptID");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BillingId).HasColumnName("BillingID");
            entity.Property(e => e.Method)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Note)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Billing).WithMany(p => p.Payments)
                .HasForeignKey(d => d.BillingId)
                .HasConstraintName("FK__PAYMENT__Billing__22751F6C");
        });

        modelBuilder.Entity<PerformSurgery>(entity =>
        {
            entity.HasKey(e => new { e.RecordId, e.SurgeryId, e.DoctorId }).HasName("PK__PERFORM___E5786D9A2C72412A");

            entity.ToTable("PERFORM_SURGERY");

            entity.Property(e => e.RecordId).HasColumnName("RecordID");
            entity.Property(e => e.SurgeryId).HasColumnName("SurgeryID");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

            entity.HasOne(d => d.Doctor).WithMany(p => p.PerformSurgeries)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PERFORM_S__Docto__05D8E0BE");

            entity.HasOne(d => d.Record).WithMany(p => p.PerformSurgeries)
                .HasForeignKey(d => d.RecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PERFORM_S__Recor__03F0984C");

            entity.HasOne(d => d.Surgery).WithMany(p => p.PerformSurgeries)
                .HasForeignKey(d => d.SurgeryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PERFORM_S__Surge__04E4BC85");
        });

        modelBuilder.Entity<PerformTest>(entity =>
        {
            entity.HasKey(e => new { e.RecordId, e.TestId, e.NurseId }).HasName("PK__PERFORM___8B50CFA17720B3A3");

            entity.ToTable("PERFORM_TEST");

            entity.Property(e => e.RecordId).HasColumnName("RecordID");
            entity.Property(e => e.TestId).HasColumnName("TestID");
            entity.Property(e => e.NurseId).HasColumnName("NurseID");

            entity.HasOne(d => d.Nurse).WithMany(p => p.PerformTests)
                .HasForeignKey(d => d.NurseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PERFORM_T__Nurse__0A9D95DB");

            entity.HasOne(d => d.Record).WithMany(p => p.PerformTests)
                .HasForeignKey(d => d.RecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PERFORM_T__Recor__08B54D69");

            entity.HasOne(d => d.Test).WithMany(p => p.PerformTests)
                .HasForeignKey(d => d.TestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PERFORM_T__TestI__09A971A2");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK__ROOM__32863919548AC209");

            entity.ToTable("ROOM");

            entity.Property(e => e.RoomId)
                .ValueGeneratedNever()
                .HasColumnName("RoomID");
            entity.Property(e => e.StatusRoom).HasMaxLength(100);
            entity.Property(e => e.TypeRoom).HasMaxLength(100);

            entity.HasOne(d => d.DnoNavigation).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.Dno)
                .HasConstraintName("FK_Room_Department");
        });

        modelBuilder.Entity<Surgery>(entity =>
        {
            entity.HasKey(e => e.SurgeryId).HasName("PK__SURGERY__08AD55DD0260492A");

            entity.ToTable("SURGERY");

            entity.Property(e => e.SurgeryId)
                .ValueGeneratedNever()
                .HasColumnName("SurgeryID");
            entity.Property(e => e.Complication).HasMaxLength(100);
            entity.Property(e => e.Outcome).HasMaxLength(100);
            entity.Property(e => e.TypeSurgery).HasMaxLength(100);

            entity.HasMany(d => d.Equipment).WithMany(p => p.Surgeries)
                .UsingEntity<Dictionary<string, object>>(
                    "UsedInSurgery",
                    r => r.HasOne<Equipment>().WithMany()
                        .HasForeignKey("EquipmentId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__USED_IN_S__Equip__71D1E811"),
                    l => l.HasOne<Surgery>().WithMany()
                        .HasForeignKey("SurgeryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__USED_IN_S__Surge__70DDC3D8"),
                    j =>
                    {
                        j.HasKey("SurgeryId", "EquipmentId").HasName("PK__USED_IN___8BE92184ADDBBF14");
                        j.ToTable("USED_IN_SURGERY");
                        j.IndexerProperty<int>("SurgeryId").HasColumnName("SurgeryID");
                        j.IndexerProperty<int>("EquipmentId").HasColumnName("EquipmentID");
                    });
        });

        modelBuilder.Entity<Technician>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__TECHNICI__7AD04FF1C4BB4C20");

            entity.ToTable("TECHNICIAN");

            entity.Property(e => e.EmployeeId)
                .ValueGeneratedNever()
                .HasColumnName("EmployeeID");
            entity.Property(e => e.Specialty).HasMaxLength(100);

            entity.HasOne(d => d.Employee).WithOne(p => p.Technician)
                .HasForeignKey<Technician>(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Technician_Employee");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
