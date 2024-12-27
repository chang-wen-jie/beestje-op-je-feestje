using BeestjeOpJeFeestje.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BeestjeOpJeFeestje.Data.DbContext
{
    public class BeestjeOpJeFeestjeDbContext(DbContextOptions<BeestjeOpJeFeestjeDbContext> options)
        : IdentityDbContext(options)
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<AnimalType> AnimalTypes { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerType> CustomerTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
                
                entity.Property(b => b.DiscountAmount)
                    .HasColumnName("booking_discount_amount")
                    .HasColumnType("decimal(10, 2)")
                    .IsRequired();

                entity.Property(b => b.IsConfirmed)
                      .HasColumnName("booking_is_confirmed")
                      .IsRequired();

                entity.Property(b => b.CustomerId)
                      .HasColumnName("booking_customer_id")
                      .IsRequired();
            });
            
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customers");

                entity.Property(c => c.Id)
                    .HasColumnName("customer_id");

                entity.Property(c => c.Password)
                    .HasColumnName("customer_password")
                    .IsRequired();

                entity.Property(c => c.Name)
                    .HasColumnName("customer_name")
                    .IsRequired();

                entity.Property(c => c.HouseNumber)
                    .HasColumnName("customer_house_number")
                    .IsRequired();

                entity.Property(c => c.ZipCode)
                    .HasColumnName("customer_zip_code")
                    .IsRequired();

                entity.Property(c => c.EmailAddress)
                    .HasColumnName("customer_email_address");

                entity.Property(c => c.PhoneNumber)
                    .HasColumnName("customer_phone_number");

                entity.HasOne(c => c.Type)
                    .WithMany(ct => ct.Customers)
                    .HasForeignKey(c => c.TypeId)
                    .HasConstraintName("fk_customers_customer_types_customer_type_id")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                entity.HasMany(c => c.Bookings)
                    .WithOne(b => b.Customer)
                    .HasForeignKey(b => b.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<CustomerType>(entity =>
            {
                entity.ToTable("customer_types");

                entity.Property(ct => ct.Id)
                    .HasColumnName("customer_type_id");

                entity.Property(ct => ct.Name)
                    .HasColumnName("customer_type_name")
                    .IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
