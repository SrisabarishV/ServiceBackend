using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Backend.SQLContext.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appsetting> Appsettings { get; set; }

    public virtual DbSet<Appstatus> Appstatuses { get; set; }

    public virtual DbSet<BookingIntent> BookingIntents { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<RecentActivity> RecentActivities { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Trip> Trips { get; set; }

    public virtual DbSet<Truck> Trucks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=Backend;Username=postgres;Password=pgadmin");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appsetting>(entity =>
        {
            entity.HasKey(e => e.Settingid).HasName("app_settings_pkey");

            entity.ToTable("appsetting");

            entity.HasIndex(e => e.Settingkey, "uq_settingkey").IsUnique();

            entity.Property(e => e.Settingid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("settingid");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Modifiedat).HasColumnName("modifiedat");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.Settingkey)
                .HasMaxLength(100)
                .HasColumnName("settingkey");
            entity.Property(e => e.Settingvalue)
                .HasMaxLength(255)
                .HasColumnName("settingvalue");
        });

        modelBuilder.Entity<Appstatus>(entity =>
        {
            entity.HasKey(e => e.Statusid).HasName("appstatus_pkey");

            entity.ToTable("appstatus");

            entity.HasIndex(e => new { e.StatusCategory, e.StatusName }, "uq_category_name").IsUnique();

            entity.Property(e => e.Statusid)
                .ValueGeneratedNever()
                .HasColumnName("statusid");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Modifiedat).HasColumnName("modifiedat");
            entity.Property(e => e.StatusCategory)
                .HasMaxLength(50)
                .HasColumnName("status_category");
            entity.Property(e => e.StatusName)
                .HasMaxLength(100)
                .HasColumnName("status_name");
        });

        modelBuilder.Entity<BookingIntent>(entity =>
        {
            entity.HasKey(e => e.Intentid).HasName("booking_intents_pkey");

            entity.ToTable("booking_intents");

            entity.HasIndex(e => e.IntentNumber, "booking_intents_intent_number_key").IsUnique();

            entity.Property(e => e.Intentid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("intentid");
            entity.Property(e => e.Clientid).HasColumnName("clientid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.DeliveryLocation)
                .HasMaxLength(500)
                .HasColumnName("delivery_location");
            entity.Property(e => e.IntStatusId).HasColumnName("int_status_id");
            entity.Property(e => e.IntentAmount)
                .HasPrecision(12, 2)
                .HasColumnName("intent_amount");
            entity.Property(e => e.IntentNumber)
                .HasMaxLength(50)
                .HasColumnName("intent_number");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Material)
                .HasMaxLength(255)
                .HasColumnName("material");
            entity.Property(e => e.Modifiedat).HasColumnName("modifiedat");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.OperationMode)
                .HasMaxLength(50)
                .HasColumnName("operation_mode");
            entity.Property(e => e.OperationalComments).HasColumnName("operational_comments");
            entity.Property(e => e.PickupLocation)
                .HasMaxLength(500)
                .HasColumnName("pickup_location");
            entity.Property(e => e.PodStatusId).HasColumnName("pod_status_id");
            entity.Property(e => e.TruckType)
                .HasMaxLength(100)
                .HasColumnName("truck_type");
            entity.Property(e => e.WeightMt)
                .HasPrecision(10, 2)
                .HasColumnName("weight_mt");

            entity.HasOne(d => d.Client).WithMany(p => p.BookingIntents)
                .HasForeignKey(d => d.Clientid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_intent_client");

            entity.HasOne(d => d.IntStatus).WithMany(p => p.BookingIntentIntStatuses)
                .HasForeignKey(d => d.IntStatusId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_intent_int_status");

            entity.HasOne(d => d.PodStatus).WithMany(p => p.BookingIntentPodStatuses)
                .HasForeignKey(d => d.PodStatusId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_intent_pod_status");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Clientid).HasName("clients_pkey");

            entity.ToTable("clients");

            entity.Property(e => e.Clientid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("clientid");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Companyname)
                .HasMaxLength(255)
                .HasColumnName("companyname");
            entity.Property(e => e.Contactnumber)
                .HasMaxLength(20)
                .HasColumnName("contactnumber");
            entity.Property(e => e.Contactperson)
                .HasMaxLength(150)
                .HasColumnName("contactperson");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Email)
                .HasMaxLength(320)
                .HasColumnName("email");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Modifiedat).HasColumnName("modifiedat");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.Driverid).HasName("drivers_pkey");

            entity.ToTable("drivers");

            entity.HasIndex(e => e.Licensenumber, "drivers_licensenumber_key").IsUnique();

            entity.Property(e => e.Driverid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("driverid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.DriverStatusId).HasColumnName("driver_status_id");
            entity.Property(e => e.Firstname)
                .HasMaxLength(150)
                .HasColumnName("firstname");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Lastname)
                .HasMaxLength(150)
                .HasColumnName("lastname");
            entity.Property(e => e.Licensenumber)
                .HasMaxLength(100)
                .HasColumnName("licensenumber");
            entity.Property(e => e.Modifiedat).HasColumnName("modifiedat");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(20)
                .HasColumnName("phonenumber");

            entity.HasOne(d => d.DriverStatus).WithMany(p => p.Drivers)
                .HasForeignKey(d => d.DriverStatusId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_driver_status");
        });

        modelBuilder.Entity<RecentActivity>(entity =>
        {
            entity.HasKey(e => e.Activityid).HasName("recent_activities_pkey");

            entity.ToTable("recent_activities");

            entity.Property(e => e.Activityid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("activityid");
            entity.Property(e => e.ActionType)
                .HasMaxLength(50)
                .HasColumnName("action_type");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnName("createdat");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EntityRef)
                .HasMaxLength(50)
                .HasColumnName("entity_ref");
            entity.Property(e => e.EntityType)
                .HasMaxLength(50)
                .HasColumnName("entity_type");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.RecentActivities)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_activity_user");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Roleid).HasName("role_pkey");

            entity.ToTable("roles");

            entity.HasIndex(e => e.Rolename, "role_rolename").IsUnique();

            entity.Property(e => e.Roleid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("roleid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Modifiedat).HasColumnName("modifiedat");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.Rolename)
                .HasMaxLength(255)
                .HasColumnName("rolename");
        });

        modelBuilder.Entity<Trip>(entity =>
        {
            entity.HasKey(e => e.Tripid).HasName("trips_pkey");

            entity.ToTable("trips");

            entity.HasIndex(e => e.TripNumber, "trips_trip_number_key").IsUnique();

            entity.Property(e => e.Tripid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("tripid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Driverid).HasColumnName("driverid");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.ExternalDriverName)
                .HasMaxLength(150)
                .HasColumnName("external_driver_name");
            entity.Property(e => e.ExternalDriverPhone)
                .HasMaxLength(20)
                .HasColumnName("external_driver_phone");
            entity.Property(e => e.ExternalTransporterName)
                .HasMaxLength(255)
                .HasColumnName("external_transporter_name");
            entity.Property(e => e.ExternalTruckReg)
                .HasMaxLength(50)
                .HasColumnName("external_truck_reg");
            entity.Property(e => e.ExternalTruckType)
                .HasMaxLength(100)
                .HasColumnName("external_truck_type");
            entity.Property(e => e.Intentid).HasColumnName("intentid");
            entity.Property(e => e.IsExternalHire)
                .HasDefaultValue(false)
                .HasColumnName("is_external_hire");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Modifiedat).HasColumnName("modifiedat");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.ProgressPercentage)
                .HasDefaultValue(0)
                .HasColumnName("progress_percentage");
            entity.Property(e => e.StartTime).HasColumnName("start_time");
            entity.Property(e => e.TripNumber)
                .HasMaxLength(50)
                .HasColumnName("trip_number");
            entity.Property(e => e.TripStatusId).HasColumnName("trip_status_id");
            entity.Property(e => e.Truckid).HasColumnName("truckid");

            entity.HasOne(d => d.Driver).WithMany(p => p.Trips)
                .HasForeignKey(d => d.Driverid)
                .HasConstraintName("fk_trip_driver");

            entity.HasOne(d => d.Intent).WithMany(p => p.Trips)
                .HasForeignKey(d => d.Intentid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_trip_intent");

            entity.HasOne(d => d.TripStatus).WithMany(p => p.Trips)
                .HasForeignKey(d => d.TripStatusId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_trip_status");

            entity.HasOne(d => d.Truck).WithMany(p => p.Trips)
                .HasForeignKey(d => d.Truckid)
                .HasConstraintName("fk_trip_truck");
        });

        modelBuilder.Entity<Truck>(entity =>
        {
            entity.HasKey(e => e.Truckid).HasName("trucks_pkey");

            entity.ToTable("trucks");

            entity.HasIndex(e => e.RegistrationNumber, "trucks_registration_number_key").IsUnique();

            entity.Property(e => e.Truckid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("truckid");
            entity.Property(e => e.CapacityMt)
                .HasPrecision(10, 2)
                .HasColumnName("capacity_mt");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Modifiedat).HasColumnName("modifiedat");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.RegistrationNumber)
                .HasMaxLength(50)
                .HasColumnName("registration_number");
            entity.Property(e => e.TruckStatusId).HasColumnName("truck_status_id");
            entity.Property(e => e.TruckType)
                .HasMaxLength(100)
                .HasColumnName("truck_type");

            entity.HasOne(d => d.TruckStatus).WithMany(p => p.Trucks)
                .HasForeignKey(d => d.TruckStatusId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_truck_status");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("user_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Emailid, "uq_user_emailid").IsUnique();

            entity.Property(e => e.Userid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("userid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Emailid)
                .HasMaxLength(320)
                .HasColumnName("emailid");
            entity.Property(e => e.Firstname)
                .HasMaxLength(150)
                .HasColumnName("firstname");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Lastname)
                .HasMaxLength(150)
                .HasColumnName("lastname");
            entity.Property(e => e.Modifiedat).HasColumnName("modifiedat");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.Passwordhash)
                .HasMaxLength(150)
                .HasColumnName("passwordhash");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(20)
                .HasColumnName("phonenumber");
            entity.Property(e => e.Roleid).HasColumnName("roleid");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.Roleid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_userrole");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
