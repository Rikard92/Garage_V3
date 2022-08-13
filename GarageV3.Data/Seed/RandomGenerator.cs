using System.Text;

namespace GarageV3.Data
{
    public static class RandomGenerator
    {
        private static Random rnd = new Random();

        private const string _characters = "abcdefghijklmopqrstuvwxyz";
        private const string _digits = "0123456789";

        private static StringBuilder strBuilder = new();


        public static string GeneratePersNr()
        {
            strBuilder.Clear();

            var month = rnd.Next(1, 12);
            var day = rnd.Next(1, 30);

            strBuilder.Append(rnd.Next(1947, 2001));
            strBuilder.Append(month < 10 ? $"0{month}" : month);
            strBuilder.Append(day < 10 ? $"0{day}" : day);
            strBuilder.Append(_digits[rnd.Next(_digits.Length)]);
            strBuilder.Append(_digits[rnd.Next(_digits.Length)]);
            strBuilder.Append(_digits[rnd.Next(_digits.Length)]);
            strBuilder.Append(_digits[rnd.Next(_digits.Length)]);

            return strBuilder.ToString();
        }

        public static string GenerateRegNr()
        {
            strBuilder.Clear();

            strBuilder.Append(_characters[rnd.Next(_characters.Length)]);
            strBuilder.Append(_characters[rnd.Next(_characters.Length)]);
            strBuilder.Append(_characters[rnd.Next(_characters.Length)]);

            strBuilder.Append(_digits[rnd.Next(_digits.Length)]);
            strBuilder.Append(_digits[rnd.Next(_digits.Length)]);
            strBuilder.Append(_digits[rnd.Next(_digits.Length)]);

            return strBuilder.ToString();

        }
    }
}
