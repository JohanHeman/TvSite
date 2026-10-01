using System;
using System.Collections.Generic;
using System.Text;

namespace TvSite.Application.Helpers
{
    public static class TextFormatingHelpers
    {
        public static string MakeFirstLetterToUpper(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return input;
            }
            return char.ToUpper(input[0]) + input.Substring(1);
        }
    }
}
