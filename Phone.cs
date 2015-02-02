using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace FoodPantryLib
{
    public class Phone
    {
        private string _value = null;

        public string value
        {
            get
            {
                return _value;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _value = null;
                }
                else
                {
                    Regex invalidChars = new Regex("[^0-9]?");
                    _value = invalidChars.Replace(value, string.Empty);
                }
            }
        }

        public Regex validPattern = new Regex("^[0-9]{10}$");

        public Phone()
        {
        }

        public Phone(string value)
        {
            this.value = value;
        }

        public bool isValid
        {
            get
            {
                if (string.IsNullOrEmpty(_value))
                {
                    return false;
                }

                if (!validPattern.IsMatch(value))
                {
                    return false;
                }

                return true;
            }
        }

        public override string ToString()
        {
            return value;
        }
    }
}
