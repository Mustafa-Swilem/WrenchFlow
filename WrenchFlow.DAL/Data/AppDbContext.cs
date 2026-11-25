using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WrenchFlow.DAL.Entities;

namespace WrenchFlow.DAL.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActivityLog> ActivityLogs { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceItem> InvoiceItems { get; set; }

    public virtual DbSet<JobOrder> JobOrders { get; set; }

    public virtual DbSet<JobOrderFault> JobOrderFaults { get; set; }

    public virtual DbSet<JobOrderFaultAssignment> JobOrderFaultAssignments { get; set; }

    public virtual DbSet<JobOrderFaultSparePart> JobOrderFaultSpareParts { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<SparePart> SpareParts { get; set; }

    public virtual DbSet<Specialization> Specializations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserSpecialization> UserSpecializations { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    public virtual DbSet<VehicleRelease> VehicleReleases { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__Activity__5E5499A84D2B91F6");

            entity.Property(e => e.LogId).HasColumnName("LogID");
            entity.Property(e => e.Action).HasMaxLength(50);
            entity.Property(e => e.LogDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.ActivityLogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__ActivityL__UserI__22751F6C");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B84F8AE152");

            entity.HasIndex(e => e.PersonId, "UQ__Customer__AA2FFB84B68018C0").IsUnique();

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PersonId).HasColumnName("PersonID");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.Customers)
                .HasForeignKey(d => d.CreatedByUserId)
                .HasConstraintName("FK_Customers_Users");

            entity.HasOne(d => d.Person).WithOne(p => p.Customer)
                .HasForeignKey<Customer>(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customers__Perso__3D5E1FD2");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__Invoices__D796AAD5363BF8B1");

            entity.HasIndex(e => e.InvoiceNumber, "UQ__Invoices__D776E9810C7F5931").IsUnique();

            entity.HasIndex(e => e.JobOrderId, "UQ__Invoices__EACFC527FF4EED92").IsUnique();

            entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");
            entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InvoiceNumber).HasMaxLength(50);
            entity.Property(e => e.InvoiceStatus)
                .HasMaxLength(20)
                .HasDefaultValue("Unpaid");
            entity.Property(e => e.IssueDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.JobOrderId).HasColumnName("JobOrderID");
            entity.Property(e => e.Subtotal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Tax).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TaxRate).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.JobOrder).WithOne(p => p.Invoice)
                .HasForeignKey<Invoice>(d => d.JobOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoices__JobOrd__02084FDA");
        });

        modelBuilder.Entity<InvoiceItem>(entity =>
        {
            entity.HasKey(e => e.InvoiceItemId).HasName("PK__InvoiceI__478FE0FC3F0C1F8E");

            entity.Property(e => e.InvoiceItemId).HasColumnName("InvoiceItemID");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");
            entity.Property(e => e.Quantity).HasDefaultValue(1);
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceItems)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__InvoiceIt__Invoi__0D7A0286");
        });

        modelBuilder.Entity<JobOrder>(entity =>
        {
            entity.HasKey(e => e.JobOrderId).HasName("PK__JobOrder__EACFC52615FBE8D9");

            entity.Property(e => e.JobOrderId).HasColumnName("JobOrderID");
            entity.Property(e => e.CompletionDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(20)
                .HasDefaultValue("Open");
            entity.Property(e => e.ReceivedByUserId).HasColumnName("ReceivedByUserID");
            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");

            entity.HasOne(d => d.ReceivedByUser).WithMany(p => p.JobOrders)
                .HasForeignKey(d => d.ReceivedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__JobOrders__Recei__5812160E");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.JobOrders)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__JobOrders__Vehic__571DF1D5");
        });

        modelBuilder.Entity<JobOrderFault>(entity =>
        {
            entity.HasKey(e => e.JobOrderFaultId).HasName("PK__JobOrder__A2652B0E4A2CAD7C");

            entity.Property(e => e.JobOrderFaultId).HasColumnName("JobOrderFaultID");
            entity.Property(e => e.DecisionDate).HasColumnType("datetime");
            entity.Property(e => e.FaultStatus)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");
            entity.Property(e => e.JobOrderId).HasColumnName("JobOrderID");
            entity.Property(e => e.LaborCost).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.RequiredSpecializationId).HasColumnName("RequiredSpecializationID");
            entity.Property(e => e.Severity)
                .HasMaxLength(20)
                .HasDefaultValue("Medium");

            entity.HasOne(d => d.JobOrder).WithMany(p => p.JobOrderFaults)
                .HasForeignKey(d => d.JobOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__JobOrderF__JobOr__5EBF139D");

            entity.HasOne(d => d.RequiredSpecialization).WithMany(p => p.JobOrderFaults)
                .HasForeignKey(d => d.RequiredSpecializationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__JobOrderF__Requi__60A75C0F");
        });

        modelBuilder.Entity<JobOrderFaultAssignment>(entity =>
        {
            entity.HasKey(e => e.AssignmentId).HasName("PK__JobOrder__32499E5730C7C3D5");

            entity.Property(e => e.AssignmentId).HasColumnName("AssignmentID");
            entity.Property(e => e.AssignedByUserId).HasColumnName("AssignedByUserID");
            entity.Property(e => e.AssignedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.AssignedUserId).HasColumnName("AssignedUserID");
            entity.Property(e => e.AssignmentStatus)
                .HasMaxLength(20)
                .HasDefaultValue("InProgress");
            entity.Property(e => e.JobOrderFaultId).HasColumnName("JobOrderFaultID");
            entity.Property(e => e.TaskType).HasMaxLength(50);

            entity.HasOne(d => d.AssignedByUser).WithMany(p => p.JobOrderFaultAssignmentAssignedByUsers)
                .HasForeignKey(d => d.AssignedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__JobOrderF__Assig__6C190EBB");

            entity.HasOne(d => d.AssignedUser).WithMany(p => p.JobOrderFaultAssignmentAssignedUsers)
                .HasForeignKey(d => d.AssignedUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__JobOrderF__Assig__6B24EA82");

            entity.HasOne(d => d.JobOrderFault).WithMany(p => p.JobOrderFaultAssignments)
                .HasForeignKey(d => d.JobOrderFaultId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__JobOrderF__JobOr__6A30C649");
        });

        modelBuilder.Entity<JobOrderFaultSparePart>(entity =>
        {
            entity.HasKey(e => e.FaultPartId).HasName("PK__JobOrder__8A76D6F676C36279");

            entity.Property(e => e.FaultPartId).HasColumnName("FaultPartID");
            entity.Property(e => e.JobOrderFaultId).HasColumnName("JobOrderFaultID");
            entity.Property(e => e.Quantity).HasDefaultValue(1);
            entity.Property(e => e.SparePartId).HasColumnName("SparePartID");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.JobOrderFault).WithMany(p => p.JobOrderFaultSpareParts)
                .HasForeignKey(d => d.JobOrderFaultId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__JobOrderF__JobOr__797309D9");

            entity.HasOne(d => d.SparePart).WithMany(p => p.JobOrderFaultSpareParts)
                .HasForeignKey(d => d.SparePartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__JobOrderF__Spare__7A672E12");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A58E581442B");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod).HasMaxLength(30);
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .HasDefaultValue("Completed");
            entity.Property(e => e.PaymentType).HasMaxLength(20);

            entity.HasOne(d => d.Invoice).WithMany(p => p.Payments)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payments__Invoic__123EB7A3");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK__People__AA2FFB85C52DAC81");

            entity.HasIndex(e => e.Phone, "UQ__People__5C7E359ECB282ECC").IsUnique();

            entity.HasIndex(e => e.NationalId, "UX_People_NationalID")
                .IsUnique()
                .HasFilter("([NationalID] IS NOT NULL)");

            entity.Property(e => e.PersonId).HasColumnName("PersonID");
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.NationalId)
                .HasMaxLength(20)
                .HasColumnName("NationalID");
            entity.Property(e => e.Phone).HasMaxLength(20);
        });

        modelBuilder.Entity<SparePart>(entity =>
        {
            entity.HasKey(e => e.SparePartId).HasName("PK__SparePar__F5BA41F2EC90A78C");

            entity.HasIndex(e => e.PartCode, "UQ__SparePar__6525D39D16A33288").IsUnique();

            entity.Property(e => e.SparePartId).HasColumnName("SparePartID");
            entity.Property(e => e.PartCode).HasMaxLength(50);
            entity.Property(e => e.PartName).HasMaxLength(100);
            entity.Property(e => e.PurchasePrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SellingPrice).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Specialization>(entity =>
        {
            entity.HasKey(e => e.SpecializationId).HasName("PK__Speciali__5809D84F53F16C48");

            entity.HasIndex(e => e.SpecializationName, "UQ__Speciali__08A8EB9E04EC8D03").IsUnique();

            entity.Property(e => e.SpecializationId).HasColumnName("SpecializationID");
            entity.Property(e => e.SpecializationName).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC9343D571");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E49D853603").IsUnique();

            entity.HasIndex(e => e.PersonId, "UQ__Users__AA2FFB8455FF565C").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PasswordHash).HasMaxLength(256);
            entity.Property(e => e.PersonId).HasColumnName("PersonID");
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Person).WithOne(p => p.User)
                .HasForeignKey<User>(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__PersonID__440B1D61");
        });

        modelBuilder.Entity<UserSpecialization>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.SpecializationId }).HasName("PK__UserSpec__F2085128BE121E00");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.SpecializationId).HasColumnName("SpecializationID");

            entity.HasOne(d => d.Specialization).WithMany(p => p.UserSpecializations)
                .HasForeignKey(d => d.SpecializationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserSpeci__Speci__4D94879B");

            entity.HasOne(d => d.User).WithMany(p => p.UserSpecializations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserSpeci__UserI__4CA06362");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.VehicleId).HasName("PK__Vehicles__476B54B2F44C4185");

            entity.HasIndex(e => e.PlateNumber, "UQ__Vehicles__0369262477E06298").IsUnique();

            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");
            entity.Property(e => e.Color).HasMaxLength(30);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Make).HasMaxLength(50);
            entity.Property(e => e.Model).HasMaxLength(50);
            entity.Property(e => e.PlateNumber).HasMaxLength(20);

            entity.HasOne(d => d.Customer).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Vehicles__Custom__52593CB8");
        });

        modelBuilder.Entity<VehicleRelease>(entity =>
        {
            entity.HasKey(e => e.ReleaseId).HasName("PK__VehicleR__5D7A69EDCEA632BF");

            entity.HasIndex(e => e.JobOrderId, "UQ__VehicleR__EACFC52785DD8616").IsUnique();

            entity.Property(e => e.ReleaseId).HasColumnName("ReleaseID");
            entity.Property(e => e.JobOrderId).HasColumnName("JobOrderID");
            entity.Property(e => e.ReceivedByName).HasMaxLength(100);
            entity.Property(e => e.ReleaseDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ReleasedByUserId).HasColumnName("ReleasedByUserID");

            entity.HasOne(d => d.JobOrder).WithOne(p => p.VehicleRelease)
                .HasForeignKey<VehicleRelease>(d => d.JobOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VehicleRe__JobOr__1BC821DD");

            entity.HasOne(d => d.ReleasedByUser).WithMany(p => p.VehicleReleases)
                .HasForeignKey(d => d.ReleasedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VehicleRe__Relea__1CBC4616");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
