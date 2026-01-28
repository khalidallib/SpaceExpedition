using System;
using System.IO;
namespace SpaceExpedition
{
    public static class Decoder
    {
        private static char[] original = {
            'A','B','C','D','E','F','G','H','I','J','K','L','M',
            'N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
        };

        private static char[] mapped = {
            'H','Z','A','U','Y','E','K','G','O','T','I','R','J',
            'V','W','N','M','F','Q','S','D','B','X','L','C','P'
        };

        public static string DecodeName(string encoded)
        {
            if (string.IsNullOrWhiteSpace(encoded))
                return string.Empty;

            string result = string.Empty;
            int i = 0;

            while (i < encoded.Length)
            {
                char c = encoded[i];

                if (c == '|' || c == ' ')
                {
                    i++;
                    continue;
                }

                char letter = c;
                i++;

                string numberText = string.Empty;
                while (i < encoded.Length && char.IsDigit(encoded[i]))
                {
                    numberText += encoded[i];
                    i++;
                }

                int level;
                int.TryParse(numberText, out level);

                result += DecodeLetterWithLevel(letter, level);
            }

            return result;
        }

        private static char DecodeLetterWithLevel(char letter, int level)
        {
            char upper = char.ToUpper(letter);

            if (upper < 'A' || upper > 'Z')
                return letter;

            if (level <= 0)
                return Mirror(upper);

            int index = -1;
            for (int i = 0; i < original.Length; i++)
            {
                if (original[i] == upper)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
                return letter;

            return DecodeLetterWithLevel(mapped[index], level - 1);
        }

        private static char Mirror(char c)
        {
            int pos = c - 'A';
            return (char)('Z' - pos);
        }
    }
}
