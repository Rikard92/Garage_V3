using GarageV3.Data;
using GarageV3.Data.Seed;

namespace GarageV3.Client.Extensions
{
    public static class AddSeedDataExtension
    {
        public static async Task AddSeedData(this WebApplication appl, int nrOfVehicles = 10)
        {
            using (var scope = appl.Services.CreateScope())
            {
                try
                {
                    var db = scope.ServiceProvider.GetRequiredService<GarageDBContext>();
                    await db.AddSeedData(nrOfVehicles);
                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }


        public static async Task AddSeedData(this GarageDBContext db, int nrOfVehicles = 10)
        {

            try
            {
                await SeedDataDB.Run(db, nrOfVehicles);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
