using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Airline.Models
{
    public partial class AirLineContext : DbContext
    {
        public AirLineContext()
        {
        }

        public AirLineContext(DbContextOptions<AirLineContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<Passenger> Passengers { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-PO60U43\\SQLEXPRESS;Database=AirLine;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Flight>(entity =>
            {
                entity.HasKey(e => e.FlightNumber)
                    .HasName("PK__flight__340D78BA18D65E70");

                entity.ToTable("flight");

                entity.Property(e => e.FlightNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("flight_number");

                entity.Property(e => e.ArrCity)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("arr_city");

                entity.Property(e => e.DateOfArr)
                    .HasColumnType("date")
                    .HasColumnName("date_of_arr");

                entity.Property(e => e.DateOfDept)
                    .HasColumnType("date")
                    .HasColumnName("date_of_dept");

                entity.Property(e => e.DepCity)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("dep_city");

                entity.Property(e => e.Duration)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("duration");

                entity.Property(e => e.FlightName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("flight_name");

                entity.Property(e => e.PriceBn).HasColumnName("price_bn");

                entity.Property(e => e.PriceEco).HasColumnName("price_eco");

                entity.Property(e => e.SeatsBussiness).HasColumnName("seats_bussiness");

                entity.Property(e => e.SeatsEco).HasColumnName("seats_eco");

                entity.Property(e => e.TimeOfArr).HasColumnName("time_of_arr");

                entity.Property(e => e.TimeOfDept).HasColumnName("time_of_dept");
            });

            modelBuilder.Entity<Passenger>(entity =>
            {
                entity.HasKey(e => e.PId)
                    .HasName("PK__passenge__82E06B9154E0B358");

                entity.ToTable("passenger");

                entity.Property(e => e.PId).HasColumnName("p_id");

                entity.Property(e => e.PAge).HasColumnName("p_age");

                entity.Property(e => e.PFullName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("p_fullName");

                entity.Property(e => e.TicketId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ticket_id");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.Passengers)
                    .HasForeignKey(d => d.TicketId)
                    .HasConstraintName("FK__passenger__ticke__5BE2A6F2");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("tickets");

                entity.Property(e => e.TicketId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ticket_id");

                entity.Property(e => e.DateOfIssue)
                    .HasColumnType("date")
                    .HasColumnName("date_of_issue");

                entity.Property(e => e.EmailId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email_id");

                entity.Property(e => e.FlightNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("flight_number");

                entity.Property(e => e.TicketStatus)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("ticket_status");

                entity.HasOne(d => d.Email)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.EmailId)
                    .HasConstraintName("FK__tickets__email_i__5812160E");

                entity.HasOne(d => d.FlightNumberNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.FlightNumber)
                    .HasConstraintName("FK__tickets__flight___59063A47");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.EmailId)
                    .HasName("PK__users__3FEF87664BBC604F");

                entity.ToTable("users");

                entity.HasIndex(e => e.Userid, "UQ__users__CBA1B256B7E44A55")
                    .IsUnique();

                entity.Property(e => e.EmailId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email_id");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("date")
                    .HasColumnName("date_of_birth");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.Mobile)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("mobile");

                entity.Property(e => e.UserPwd)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("user_pwd");

                entity.Property(e => e.Userid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("userid");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
