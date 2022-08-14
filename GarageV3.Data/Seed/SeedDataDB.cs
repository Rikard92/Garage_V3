using GarageV3.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace GarageV3.Data.Seed
{
    public static class SeedDataDB
    {
        private static Random rnd = new Random();

        public static async Task Run(GarageDBContext db, int nrOfItems = 10)
        {

            try
            {
                EnsureDeleted(db);

                //Seed Vehicle type
                var vehicleTypes = GetVehicleTypes();
                db.AddRange(vehicleTypes);


                //Seed Owner
                var owners = GetOwners(nrOfItems);
                db.AddRange(owners);


                // Seed vehicle
                var vehicles = GetVehicles(owners, vehicleTypes);
                db.AddRange(vehicles);


                await db.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw;
            }
        }


        private static void EnsureDeleted(GarageDBContext _db)
        {

            _db.Database.EnsureDeleted();
            _db.Database.Migrate();
        }



        private static IEnumerable<Vehicle> GetVehicles(IEnumerable<Owner> _owners, IEnumerable<VehicleType> _vtypes)
        {
            List<Vehicle> vehicles = new();
            Vehicle vehicle;

            var brands = new List<string> { "Saab", "Volvo", "Ford" };
            var colors = new List<string> { "Röd", "Grön", "Svart", "Blå", "Gul" };


            for (int i = 0; i < _owners.Count(); i++)
            {
                var owner = _owners.ElementAt(rnd.Next(_owners.Count()));
                var vehicleType = _vtypes.ElementAt(rnd.Next(_vtypes.Count()));

                vehicle = new Vehicle
                {
                    Brand = brands[rnd.Next(brands.Count())],
                    Model = $"X{i}",
                    Color = colors[rnd.Next(colors.Count())],
                    RegNr = RandomGenerator.GenerateRegNr().ToUpper(),
                    Wheels = rnd.Next(5),
                    Owner = owner,
                    OwnerId = owner.Id,
                    VehicleType = vehicleType,
                    VehicleTypeId = vehicleType.Id,
                    ArrivalTime = DateTime.Now.AddHours(rnd.Next(-435, 0)),


                };

                vehicles.Add(vehicle);
            }

            return vehicles;
        }

        private static Owner GetOwner()
        {

            var firstNames = new List<string> { "Pelle", "Anders", "Anki", "John", "Steve", "Katarin" };
            var lastNames = new List<string> { "Andersson", "Pettersson", "Karlsson", "Österberg", "Larsson", "Frid" };


            var _persNumber = RandomGenerator.GeneratePersNr();

            var _persNr = _persNumber.Substring(2);
            var year = _persNumber.Substring(0, 4);


            var owner = new Owner
            {
                FirstName = firstNames[rnd.Next(firstNames.Count())],
                LastName = lastNames[rnd.Next(lastNames.Count())],
                PersonNumber = double.Parse(_persNr),
                Membership = new Membership { MembershipCategory = int.Parse(year) < 1957 ? "Pro" : "Normal" },

            };

            owner.Membership.Owner = owner;

            //ownerId++;
            return owner;
        }


        private static VehicleType GetVehicleType()
        {

            var vehicleTypes = new List<string>
            {
                "LastBil",
                "PersonBil",
                "Motorcykel",
                "Skoter",
                "Flygplan",
                "Enhjuling",
                "Minibuss"
            };

            return new VehicleType
            {
                VType = vehicleTypes[rnd.Next(vehicleTypes.Count())]
            };
        }


        private static IEnumerable<VehicleType> GetVehicleTypes()
        {
            var vehicleTypes = new List<string>
            {
                "LastBil",
                "PersonBil",
                "Motorcykel",
                "Skoter",
                "Flygplan",
                "Enhjuling",
                "Minibuss"
            };

            return vehicleTypes.Select(i => new VehicleType { VType = i.ToUpper() }).ToList();
        }


        public static IEnumerable<Owner> GetOwners(int nrOfOwners = 10)
        {
            var own = new Owner();

            List<Owner> owners = new List<Owner>();

            for (int i = 0; i < nrOfOwners; i++)
            {
                own = GetOwner();

                var year = own.PersonNumber.ToString().Substring(0, 2);

                own.Membership = new Membership
                {
                    MembershipCategory = int.Parse(year) < 57 ? "Pro" : "Normal"
                };

                owners.Add(own);

            }

            return owners;
        }
    }
}
