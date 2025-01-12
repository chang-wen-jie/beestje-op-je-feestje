using BeestjeOpJeFeestje.Data.DbContext;
using BeestjeOpJeFeestje.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BeestjeOpJeFeestje.Data.Seeders
{
    public static class DatabaseSeeder
    {
        public static void SeedDatabase(BeestjeOpJeFeestjeDbContext context, UserManager<Customer> userManager, RoleManager<IdentityRole> roleManager)
        {
            try
            {
                var needsSeeding = false;

                if (!roleManager.Roles.Any())
                {
                    var roles = new[] { "Manager", "Customer" };

                    foreach (var role in roles)
                    {
                        roleManager.CreateAsync(new IdentityRole(role)).Wait();
                    }
                }

                if (!context.AnimalTypes.Any())
                {
                    var animalTypes = new List<AnimalType>
                {
                    new() { Name = "Jungle" },
                    new() { Name = "Boerderij" },
                    new() { Name = "Sneeuw" },
                    new() { Name = "Woestijn" },
                    new() { Name = "VIP" }
                };

                    context.AnimalTypes.AddRange(animalTypes);
                    context.SaveChanges();
                    needsSeeding = true;
                }
                
                if (!context.CustomerTypes.Any())
                {
                    var customerTypes = new List<CustomerType>
                    {
                        new() { Name = "Zilver"},
                        new() { Name = "Goud" },
                        new() { Name = "Platina" },
                    };

                    context.CustomerTypes.AddRange(customerTypes);
                    context.SaveChanges();
                    needsSeeding = true;
                }
                
                if (!context.Animals.Any())
                {
                    var jungleType = context.AnimalTypes.SingleOrDefault(at => at.Name == "Jungle");
                    var farmType = context.AnimalTypes.SingleOrDefault(at => at.Name == "Boerderij");
                    var snowType = context.AnimalTypes.SingleOrDefault(at => at.Name == "Sneeuw");
                    var desertType = context.AnimalTypes.SingleOrDefault(at => at.Name == "Woestijn");
                    var vipType = context.AnimalTypes.SingleOrDefault(at => at.Name == "VIP");
                    
                    var animals = new List<Animal>()
                    {
                        new() { Name = "Aap", TypeId = jungleType.Id, Price = 50, ImageUrl = "aap.png" },
                        new() { Name = "Olifant", TypeId = jungleType.Id, Price = 100, ImageUrl = "olifant.png" },
                        new() { Name = "Zebra", TypeId = jungleType.Id, Price = 75, ImageUrl = "zebra.png" },
                        new() { Name = "Leeuw", TypeId = jungleType.Id, Price = 120, ImageUrl = "leeuw.png" },

                        new() { Name = "Hond", TypeId = farmType.Id, Price = 30, ImageUrl = "hond.png" },
                        new() { Name = "Ezel", TypeId = farmType.Id, Price = 40, ImageUrl = "ezel.png" },
                        new() { Name = "Koe", TypeId = farmType.Id, Price = 80, ImageUrl = "koe.png" },
                        new() { Name = "Eend", TypeId = farmType.Id, Price = 20, ImageUrl = "eend.png" },
                        new() { Name = "Kuiken", TypeId = farmType.Id, Price = 10, ImageUrl = "kuiken.png" },

                        new() { Name = "Pinguïn", TypeId = snowType.Id, Price = 60, ImageUrl = "pinguin.png" },
                        new() { Name = "IJsbeer", TypeId = snowType.Id, Price = 110, ImageUrl = "ijsbeer.png" },
                        new() { Name = "Zeehond", TypeId = snowType.Id, Price = 70, ImageUrl = "zeehond.png" },

                        new() { Name = "Kameel", TypeId = desertType.Id, Price = 90, ImageUrl = "kameel.png" },
                        new() { Name = "Slang", TypeId = desertType.Id, Price = 45, ImageUrl = "slang.png" },

                        new() { Name = "T-Rex", TypeId = vipType.Id, Price = 200, ImageUrl = "trex.png" },
                        new() { Name = "Unicorn", TypeId = vipType.Id, Price = 300, ImageUrl = "unicorn.png" }
                    };

                    context.Animals.AddRange(animals);
                    needsSeeding = true;
                }

                if (!context.Users.Any())
                {
                    var customer = new Customer()
                    {
                        UserName = "admin@example.com",
                        Email = "admin@example.com",
                        Name = "admin",
                        HouseNumber = 1,
                        ZipCode = "1111AA",
                    };
                    
                    const string password = "admin";
                    userManager.CreateAsync(customer, password).Wait();
                    userManager.AddToRoleAsync(customer, "Manager").Wait();

                    needsSeeding = true;
                }

                if (needsSeeding) context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database kan niet worden geseed:: {ex.Message}");
            }
        }
    }
}
