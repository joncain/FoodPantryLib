using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodPantryLib
{
    public static class Utility
    {
        public static string makeSQLSafe(string text)
        {

            if (!string.IsNullOrEmpty(text))
            {
                text = text.Replace("'", "''");
            }

            return text;
        }

    }
}
