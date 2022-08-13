namespace GarageV3.Util.Extensions
{
    public static class StringHandlerExtension
    {
        /// <summary>
        /// Removes white space from target string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RemoveWhiteSpace(this string value) =>
                    string.Concat(value.Where(c => !char.IsWhiteSpace(c)));

        public static string TranslateColorLang(this string value) =>
            value.ToLower()
                .Replace("blue", "Blå")
                .Replace("red", "Röd")
                .Replace("green", "Grön")
                .Replace("magenta", "Magenta")
                .Replace("pink", "Råsa")
                .Replace("yellow", "Gul")
                .Replace("black", "Svart")
                .Replace("brown", "Brown")
                .Replace("white", "Vit")
                .Replace("grey", "Grå")
                .Replace("gold", "Guld")
                .Replace("silver", "Silver")
                .Replace("orange", "Orange")
                .Replace("violet", "Violett")
                .Replace("lime", "Lime")
                .Replace("rust", "Rost")
                .Replace("none", "Ingen")
                .Replace("light", "Ljus")
                .Replace("dark", "Mörk");
    }
}
