using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BusBooking.Models
{
    public partial class BusTicketContext : DbContext
    {
        public BusTicketContext()
        {
        }

        public BusTicketContext(DbContextOptions<BusTicketContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Booking> Booking { get; set; }
        public virtual DbSet<BookingDetail> BookingDetail { get; set; }
        public virtual DbSet<Bus> Bus { get; set; }
        public virtual DbSet<CmnUser> CmnUser { get; set; }
        public virtual DbSet<Districts> Districts { get; set; }
        public virtual DbSet<Divisions> Divisions { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<ReturnPolicy> ReturnPolicy { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Route> Route { get; set; }
        public virtual DbSet<Schedule> Schedule { get; set; }
        public virtual DbSet<ScheduleDetail> ScheduleDetail { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-LHDFEG6;Initial Catalog=BusTicket;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.Property(e => e.BookedDate).HasColumnType("datetime");

                entity.Property(e => e.BookedStatus)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CancelDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerMobile)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerName).HasMaxLength(50);
            });

            modelBuilder.Entity<BookingDetail>(entity =>
            {
                entity.HasKey(e => e.BookingDetailsId);

                entity.Property(e => e.CustomerMobile)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SeatNo)
                    .HasMaxLength(2)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Bus>(entity =>
            {
                entity.Property(e => e.BusName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BusType)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LicenseNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CmnUser>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserPass)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Districts>(entity =>
            {
                entity.ToTable("districts");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.BnName)
                    .HasColumnName("bn_name")
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DivisionId).HasColumnName("division_id");

                entity.Property(e => e.Lat)
                    .HasColumnName("lat")
                    .HasColumnType("decimal(18, 7)");

                entity.Property(e => e.Lon)
                    .HasColumnName("lon")
                    .HasColumnType("decimal(18, 7)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Website)
                    .HasColumnName("website")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Divisions>(entity =>
            {
                entity.ToTable("divisions");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.BnName)
                    .IsRequired()
                    .HasColumnName("bn_name")
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('0000-00-00 00:00:00')");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.AccountNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.BankName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BranchName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CardName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CardNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionId)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RoleName);

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<Route>(entity =>
            {
                entity.Property(e => e.EndPoint)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RouteName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StartPoint)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.Property(e => e.ActualArrivalTime).HasColumnType("datetime");

                entity.Property(e => e.ActualDepartureTime).HasColumnType("datetime");

                entity.Property(e => e.ArrivalTime).HasColumnType("datetime");

                entity.Property(e => e.BusStatus)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.DepartureTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<ScheduleDetail>(entity =>
            {
                entity.HasKey(e => e.ScheduleDetailsId);

                entity.Property(e => e.ScheduleStatus)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SeatNo)
                    .HasMaxLength(2)
                    .IsUnicode(false);
            });
        }
    }
}
