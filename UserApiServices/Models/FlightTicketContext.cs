using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UserApiServices.Models
{
    public partial class FlightTicketContext : DbContext
    {
        public FlightTicketContext()
        {
        }

        public FlightTicketContext(DbContextOptions<FlightTicketContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Flights> Flights { get; set; }
        public virtual DbSet<PassengerDetails> PassengerDetails { get; set; }
        public virtual DbSet<TicketBooking> TicketBooking { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost;Database=FlightTicket;User ID=sa;Password=pass@word1;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flights>(entity =>
            {
                entity.HasKey(e => e.FlightId);

                entity.Property(e => e.FlightId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.AirLine)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FlightStatus)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FromPlace)
                    .HasColumnName("fromPlace")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Meal)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ScheduleDays)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ToPlace)
                    .HasColumnName("toPlace")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PassengerDetails>(entity =>
            {
                entity.HasKey(e => e.Sno);

                entity.Property(e => e.Sno).HasColumnName("SNo");

                entity.Property(e => e.FlightId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PassengerGender)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PassengerName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Pnr).HasColumnName("PNR");

                entity.HasOne(d => d.Flight)
                    .WithMany(p => p.PassengerDetails)
                    .HasForeignKey(d => d.FlightId)
                    .HasConstraintName("FK__Passenger__Fligh__00200768");

                entity.HasOne(d => d.PnrNavigation)
                    .WithMany(p => p.PassengerDetails)
                    .HasForeignKey(d => d.Pnr)
                    .HasConstraintName("FK__PassengerDe__PNR__7F2BE32F");
            });

            modelBuilder.Entity<TicketBooking>(entity =>
            {
                entity.HasKey(e => e.Pnr);

                entity.Property(e => e.Pnr).HasColumnName("PNR");

                entity.Property(e => e.ArrivalDate).HasColumnType("date");

                entity.Property(e => e.BookingStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DepartureDate).HasColumnType("date");

                entity.Property(e => e.EmailId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FlightId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Meal)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TimeOfBooking)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Email)
                    .WithMany(p => p.TicketBooking)
                    .HasForeignKey(d => d.EmailId)
                    .HasConstraintName("FK__TicketBoo__Email__7A672E12");

                entity.HasOne(d => d.Flight)
                    .WithMany(p => p.TicketBooking)
                    .HasForeignKey(d => d.FlightId)
                    .HasConstraintName("FK__TicketBoo__Fligh__7B5B524B");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.EmailId);

                entity.Property(e => e.EmailId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Password)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });
        }
    }
}
