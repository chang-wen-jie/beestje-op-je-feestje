using BeestjeOpJeFeestje.Data.DbContext;
using BeestjeOpJeFeestje.Data.Models;

namespace BeestjeOpJeFeestje.Data.Seeders
{
    public static class DatabaseSeeder
    {
        public static void SeedDatabase(BeestjeOpJeFeestjeDbContext context)
        {
            try
            {
                bool needsSeeding = false;

                if (!context.AccountTypes.Any())
                {
                    var accountTypes = new List<AccountType>
                    {
                    new() { Name = "Zilver"},
                    new() { Name = "Goud" },
                    new() { Name = "Platina" },
                    new() { Name = "Eigenaar" },
                    };

                    context.AccountTypes.AddRange(accountTypes);
                    needsSeeding = true;
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
                    needsSeeding = true;
                }

                if (!context.Accounts.Any())
                {
                    var eigenaarType = context.AccountTypes.SingleOrDefault(at => at.Name == "Eigenaar");

                    var account = new Account()
                    {
                        Password = "admin",
                        Name = "admin",
                        HouseNumber = 1,
                        ZipCode = "1111AA",
                        TypeId = eigenaarType.Id,
                    };

                    context.Accounts.Add(account);
                    needsSeeding = true;
                }



                if (!context.Animals.Any())
                {
                    var jungleType = context.AnimalTypes.SingleOrDefault(at => at.Name == "Jungle");
                    var boerderijType = context.AnimalTypes.SingleOrDefault(at => at.Name == "Boerderij");
                    var sneeuwType = context.AnimalTypes.SingleOrDefault(at => at.Name == "Sneeuw");
                    var woestijnType = context.AnimalTypes.SingleOrDefault(at => at.Name == "Woestijn");
                    var vipType = context.AnimalTypes.SingleOrDefault(at => at.Name == "VIP");

                    var animals = new List<Animal>()
                {
                    new() { Name = "Aap", TypeId = jungleType.Id, Price = 50, ImageUrl = "aap.jpg" },
                    new() { Name = "Olifant", TypeId = jungleType.Id, Price = 100, ImageUrl = "olifant.jpg" },
                    new() { Name = "Zebra", TypeId = jungleType.Id, Price = 75, ImageUrl = "zebra.jpg" },
                    new() { Name = "Leeuw", TypeId = jungleType.Id, Price = 120, ImageUrl = "leeuw.jpg" },

                    new() { Name = "Hond", TypeId = boerderijType.Id, Price = 30, ImageUrl = "hond.jpg" },
                    new() { Name = "Ezel", TypeId = boerderijType.Id, Price = 40, ImageUrl = "ezel.jpg" },
                    new() { Name = "Koe", TypeId = boerderijType.Id, Price = 80, ImageUrl = "koe.jpg" },
                    new() { Name = "Eend", TypeId = boerderijType.Id, Price = 20, ImageUrl = "eend.jpg" },
                    new() { Name = "Kuiken", TypeId = boerderijType.Id, Price = 10, ImageUrl = "kuiken.jpg" },

                    new() { Name = "Pinguïn", TypeId = sneeuwType.Id, Price = 60, ImageUrl = "pinguin.jpg" },
                    new() { Name = "IJsbeer", TypeId = sneeuwType.Id, Price = 110, ImageUrl = "ijsbeer.jpg" },
                    new() { Name = "Zeehond", TypeId = sneeuwType.Id, Price = 70, ImageUrl = "zeehond.jpg" },

                    new() { Name = "Kameel", TypeId = woestijnType.Id, Price = 90, ImageUrl = "kameel.jpg" },
                    new() { Name = "Slang", TypeId = woestijnType.Id, Price = 45, ImageUrl = "slang.jpg" },

                    new() { Name = "T-Rex", TypeId = vipType.Id, Price = 200, ImageUrl = "trex.jpg" },
                    new() { Name = "Unicorn", TypeId = vipType.Id, Price = 300, ImageUrl = "unicorn.jpg" }
                };

                    context.Animals.AddRange(animals);
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
