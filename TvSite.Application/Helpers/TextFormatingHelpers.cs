using System;
using System.Collections.Generic;
using System.Text;

namespace TvSite.Application.Helpers
{
    public static class TextFormatingHelpers
    {
        public static string SetFirstLetterToUpper(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return input;
            }

            if(input.Length == 1)
            {
                return input.ToUpper();
            }

            return char.ToUpper(input[0]) + input.Substring(1);
        }
    }
}
