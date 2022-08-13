namespace GarageV3.Utilities
{
    public class Enums
    {
        /// <summary>
        /// Represent selection for vehicle types
        /// </summary>
        public enum VehicleType
        {
            Välj,
            LastBil,
            PersonBil,
            Motorcykel,
            Skoter,
            Flygplan,
            Enhjuling,
            Minibuss,
        }

        public static VehicleType VehicleTypes { get; set; }

    }
}
