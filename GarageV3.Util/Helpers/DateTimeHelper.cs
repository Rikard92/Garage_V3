namespace GarageV3.Util.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime GetCurrentDate(bool isUtc = false) => isUtc ? DateTime.UtcNow : DateTime.Now;
    }
}
