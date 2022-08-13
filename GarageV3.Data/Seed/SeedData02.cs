using GarageV3.Core.Models;

namespace GarageV3.Data.Seed
{
    public static class SeedData02
    {
        private static Random rnd = new Random();

        //private static int ownerId = 10;


        public static IEnumerable<Vehicle> GetVehicles(int nrOfVehicles = 10)
        {
            List<Vehicle> vehicles = new();
            Vehicle vehicle;

            var brands = new List<string> { "Saab", "Volvo", "Ford" };
            var colors = new List<string> { "Röd", "Grön", "Svart", "Blå", "Gul" };

            var id = 1;

            for (int i = 0; i < nrOfVehicles; i++)
            {
                vehicle = new Vehicle
                {
                    Brand = brands[rnd.Next(brands.Count())],
                    Model = $"X{i}",
                    Color = colors[rnd.Next(colors.Count())],
                    RegNr = $"abc12{i}",
                    Wheels = rnd.Next(5),
                    Owner = GetOwner(),
                    ArrivalTime = DateTime.Now.AddDays(-2),
                };

                vehicle.VehicleTypeId = GetVehicleType().Id;

                vehicles.Add(vehicle);

                id++;

            }

            return vehicles;
        }


        private static Owner GetOwner()
        {

            var firstNames = new List<string> { "Pelle", "Anders", "Anki", "John", "Steve", "Katarin" };
            var lastNames = new List<string> { "Andersson", "Pettersson", "Karlsson", "Österberg", "Larsson", "Frid" };


            var _persNumber = rnd.Next(1947, 2001);

            var owner = new Owner
            {
                //Id = ownerId,
                FirstName = firstNames[rnd.Next(firstNames.Count())],
                LastName = lastNames[rnd.Next(lastNames.Count())],
                PersonNumber = _persNumber,
                Membership = new Membership { MembershipCategory = _persNumber < 1957 ? "Pro" : "Normal" },

            };

            owner.Membership.Owner = owner;
            //owner.Membership.OwnerId = owner.Id;

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


        public static IEnumerable<VehicleType> GetVehicleTypes()
        {
            List<VehicleType> _types = new();
            VehicleType _type;

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

            var id = 1;

            foreach (var item in vehicleTypes)
            {
                _type = new VehicleType
                {
                    Id = id,
                    VType = item
                };

                _types.Add(_type);
                id++;
            }

            return _types;
        }


        public static IEnumerable<Owner> GetOwners(int nrOfOwners = 10)
        {
            var own = new Owner();

            List<Owner> owners = new List<Owner>();

            for (int i = 0; i < nrOfOwners; i++)
            {
                own = GetOwner();

                own.Membership = new Membership
                {
                    MembershipCategory = own.PersonNumber < 1957 ? "Pro" : "Normal"
                };

                owners.Add(own);

            }

            return owners;
        }




    }
}
