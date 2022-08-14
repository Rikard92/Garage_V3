﻿using System.Text;

namespace GarageV3.Util.Helpers
{
    public static class DateTimeHelper
    {
        private static StringBuilder sb = new();

        public static DateTime GetCurrentDate(bool isUtc = false) => isUtc ? DateTime.UtcNow : DateTime.Now;


        public static string BeautifyDate(this DateTime value)
        {
            sb.Clear();

            var diff = DateTime.Now - value;

            if (diff.Days > 0) { sb.Append($"Dagar: {diff.Days} "); }
            if (diff.Hours > 0) { sb.Append($"Timmar: {diff.Hours} "); }
            if (diff.Minutes > 0) { sb.Append($"Minuter: {diff.Minutes} "); }

            return sb.ToString();
        }

    }
}
