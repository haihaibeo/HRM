using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HRmanagement.Models
{
    public partial class HrmContext : DbContext
    {
        public HrmContext()
        {
        }

        public HrmContext(DbContextOptions<HrmContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Award> Award { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Discipline> Discipline { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeeHasAward> EmployeeHasAward { get; set; }
        public virtual DbSet<EmployeeHasDiscipline> EmployeeHasDiscipline { get; set; }
        public virtual DbSet<Passport> Passport { get; set; }
        public virtual DbSet<Position> Position { get; set; }
        public virtual DbSet<Recordbook> Recordbook { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=phamngochai;database=hr_management");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Award>(entity =>
            {
                entity.ToTable("award");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'undefined'");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("department");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'undefined'");
            });

            modelBuilder.Entity<Discipline>(entity =>
            {
                entity.ToTable("discipline");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.DisciplineName)
                    .IsRequired()
                    .HasColumnName("discipline_name")
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'undefined'");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.RecordbookId, e.PassportId })
                    .HasName("PRIMARY");

                entity.ToTable("employee");

                entity.HasIndex(e => e.DepartmentId)
                    .HasName("fk_employee_department1_idx");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.PassportId)
                    .HasName("fk_employee_passport1_idx");

                entity.HasIndex(e => e.PositionId)
                    .HasName("fk_employee_position1_idx");

                entity.HasIndex(e => e.RecordbookId)
                    .HasName("fk_employee_recordbook1_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.RecordbookId)
                    .HasColumnName("recordbook_id")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.PassportId)
                    .HasColumnName("passport_id")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.AwardName)
                    .HasColumnName("award_name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.DateEnd)
                    .HasColumnName("date_end")
                    .HasColumnType("date");

                entity.Property(e => e.DateStart)
                    .HasColumnName("date_start")
                    .HasColumnType("date");

                entity.Property(e => e.Degree)
                    .IsRequired()
                    .HasColumnName("degree")
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'undefined'");

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("department_id")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.PensionNumber)
                    .IsRequired()
                    .HasColumnName("pension_number")
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'undefined'");

                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasColumnName("position")
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'undefined'");

                entity.Property(e => e.PositionId)
                    .IsRequired()
                    .HasColumnName("position_id")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.TaxNumber)
                    .IsRequired()
                    .HasColumnName("tax_number")
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'undefined'");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("fk_employee_department1");

                entity.HasOne(d => d.Passport)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.PassportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_employee_passport1");

                entity.HasOne(d => d.PositionNavigation)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.PositionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_employee_position1");

                entity.HasOne(d => d.Recordbook)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.RecordbookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_employee_recordbook1");
            });

            modelBuilder.Entity<EmployeeHasAward>(entity =>
            {
                entity.HasKey(e => new { e.EmployeeId, e.EmployeeRecordbookId, e.EmployeePassportId, e.AwardId })
                    .HasName("PRIMARY");

                entity.ToTable("employee_has_award");

                entity.HasIndex(e => e.AwardId)
                    .HasName("fk_employee_has_award_award1_idx");

                entity.HasIndex(e => new { e.EmployeeId, e.EmployeeRecordbookId, e.EmployeePassportId })
                    .HasName("fk_employee_has_award_employee1_idx");

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("employee_id")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeRecordbookId)
                    .HasColumnName("employee_recordbook_id")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeePassportId)
                    .HasColumnName("employee_passport_id")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.AwardId)
                    .HasColumnName("award_id")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.Award)
                    .WithMany(p => p.EmployeeHasAward)
                    .HasForeignKey(d => d.AwardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_employee_has_award_award1");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeHasAward)
                    .HasForeignKey(d => new { d.EmployeeId, d.EmployeeRecordbookId, d.EmployeePassportId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_employee_has_award_employee1");
            });

            modelBuilder.Entity<EmployeeHasDiscipline>(entity =>
            {
                entity.HasKey(e => new { e.EmployeeId, e.EmployeeRecordbookId, e.EmployeePassportId, e.DisciplineId })
                    .HasName("PRIMARY");

                entity.ToTable("employee_has_discipline");

                entity.HasIndex(e => e.DisciplineId)
                    .HasName("fk_employee_has_discipline_discipline1_idx");

                entity.HasIndex(e => new { e.EmployeeId, e.EmployeeRecordbookId, e.EmployeePassportId })
                    .HasName("fk_employee_has_discipline_employee1_idx");

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("employee_id")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeRecordbookId)
                    .HasColumnName("employee_recordbook_id")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeePassportId)
                    .HasColumnName("employee_passport_id")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.DisciplineId)
                    .HasColumnName("discipline_id")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.Discipline)
                    .WithMany(p => p.EmployeeHasDiscipline)
                    .HasForeignKey(d => d.DisciplineId)
                    .HasConstraintName("fk_employee_has_discipline_discipline1");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeHasDiscipline)
                    .HasForeignKey(d => new { d.EmployeeId, d.EmployeeRecordbookId, d.EmployeePassportId })
                    .HasConstraintName("fk_employee_has_discipline_employee1");
            });

            modelBuilder.Entity<Passport>(entity =>
            {
                entity.ToTable("passport");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.DataIssue)
                    .HasColumnName("data_issue")
                    .HasColumnType("date");

                entity.Property(e => e.Number).HasColumnName("number");

                entity.Property(e => e.Serie)
                    .HasColumnName("serie")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.ToTable("position");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'undefined'");
            });

            modelBuilder.Entity<Recordbook>(entity =>
            {
                entity.ToTable("recordbook");

                entity.HasComment("		");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.ContractEnd)
                    .HasColumnName("contract_end")
                    .HasColumnType("date");

                entity.Property(e => e.ContractStart)
                    .HasColumnName("contract_start")
                    .HasColumnType("date");

                entity.Property(e => e.DateIssue)
                    .HasColumnName("date_issue")
                    .HasColumnType("date");

                entity.Property(e => e.DismissalCount).HasColumnName("dismissal_count");

                entity.Property(e => e.EnrollmentCount).HasColumnName("enrollment_count");

                entity.Property(e => e.Number)
                    .HasColumnName("number")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.TransferCount).HasColumnName("transfer_count");

                entity.Property(e => e.Workload).HasColumnName("workload");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
