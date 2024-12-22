using BeestjeOpJeFeestje.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BeestjeOpJeFeestje.Data.DbContext
{
    public class BeestjeOpJeFeestjeDbContext(DbContextOptions<BeestjeOpJeFeestjeDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<AnimalType> AnimalTypes { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("accounts");

                entity.Property(a => a.Id)
                    .HasColumnName("account_id");

                entity.Property(a => a.Password)
                    .HasColumnName("account_password")
                    .IsRequired();

                entity.Property(a => a.Name)
                    .HasColumnName("account_name")
                    .IsRequired();

                entity.Property(a => a.HouseNumber)
                    .HasColumnName("account_house_number")
                    .IsRequired();

                entity.Property(a => a.ZipCode)
                      .HasColumnName("account_zip_code")
                      .IsRequired();

                entity.Property(a => a.EmailAddress)
                      .HasColumnName("account_email_address");

                entity.Property(a => a.PhoneNumber)
                      .HasColumnName("account_phone_number");

                entity.HasOne(a => a.Type)
                .WithMany(at => at.Accounts)
                .HasForeignKey(a => a.TypeId)
                .HasConstraintName("fk_accounts_account_types_account_type_id")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

                entity.HasMany(a => a.Bookings)
                    .WithOne(b => b.Account)
                    .HasForeignKey(b => b.AccountId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<AccountType>(entity =>
            {
                entity.ToTable("account_types");

                entity.Property(at => at.Id)
                    .HasColumnName("account_type_id");

                entity.Property(at => at.Name)
                    .HasColumnName("account_type_name")
                    .IsRequired();
            });

            modelBuilder.Entity<Animal>(entity =>
            {
                entity.ToTable("animals");

                entity.Property(a => a.Id)
                    .HasColumnName("animal_id");

                entity.Property(a => a.Name)
                    .HasColumnName("animal_name")
                    .IsRequired();

                entity.HasOne(a => a.Type)
                    .WithMany(at => at.Animals)
                    .HasForeignKey(a => a.TypeId)
                    .HasConstraintName("fk_animals_animal_types_animal_type_id")
                    .OnDelete(DeleteBehavior.Cascade);

                entity.Property(a => a.Price)
                    .HasColumnName("animal_price")
                    .HasColumnType("decimal(10, 2)")
                    .IsRequired();

                entity.Property(a => a.ImageUrl)
                    .HasColumnName("animal_image_url")
                    .IsRequired();

                entity.HasMany(a => a.Bookings)
                    .WithMany(b => b.Animals)
                    .UsingEntity<Dictionary<string, object>>(
                        "animals_bookings",
                        j => j.HasOne<Booking>()
                            .WithMany()
                            .HasForeignKey("booking_id")
                            .HasConstraintName("fk_animals_bookings_booking_id")
                            .OnDelete(DeleteBehavior.Cascade),
                        j => j.HasOne<Animal>()
                            .WithMany()
                            .HasForeignKey("animal_id")
                            .HasConstraintName("fk_animals_bookings_animal_id")
                            .OnDelete(DeleteBehavior.Cascade),
                        j =>
                        {
                            j.Property("booking_id").HasColumnName("booking_id");
                            j.Property("animal_id").HasColumnName("animal_id");
                        });
            });

            modelBuilder.Entity<AnimalType>(entity =>
            {
                entity.ToTable("animal_types");

                entity.Property(at => at.Id)
                    .HasColumnName("animal_type_id");

                entity.Property(at => at.Name)
                    .HasColumnName("animal_type_name")
                    .IsRequired();
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("bookings");

                entity.Property(b => b.Id)
                      .HasColumnName("booking_id");

                entity.Property(b => b.Date)
                      .HasColumnName("booking_date")
                      .IsRequired();

                entity.Property(b => b.TotalPrice)
                      .HasColumnName("booking_total_price")
                      .HasColumnType("decimal(10, 2)")
                      .IsRequired();

                entity.Property(b => b.IsConfirmed)
                      .HasColumnName("booking_is_confirmed")
                      .IsRequired();

                entity.Property(b => b.AccountId)
                      .HasColumnName("booking_account_id")
                      .IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
