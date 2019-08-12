using System;

namespace Hbm2Code
{
    public static class Util
    {
        public static string ToPascalCase(string input)
        {
            // If there are 0 or 1 characters, just return the string.
            if (input == null) return input;
            if (input.Length < 2) return input.ToUpper();

            // Split the string into words.
            string[] words = input.Split(
                new char[] {' ', '-', '_'},
                StringSplitOptions.RemoveEmptyEntries);

            // Combine the words.
            string result = "";
            foreach (string word in words)
            {
                result +=
                    word.Substring(0, 1).ToUpper() +
                    word.Substring(1);
            }

            return result;
        }
    }
}
