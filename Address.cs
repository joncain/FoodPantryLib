using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace FoodPantryLib
{
    public class Address
    {
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipcode { get; set; }
        public string country { get; set; }

        public ValidationResult isValid
        {
            get
            {
                ValidationResult result = new ValidationResult();

                //if (string.IsNullOrEmpty(address1))
                //{
                //    result.message = "address1 is empty";
                //    result.valid = false;
                //    return result;
                //}

                //if (string.IsNullOrEmpty(city))
                //{
                //    result.message = "city is empty";
                //    result.valid = false;
                //    return result;
                //}

                //if (string.IsNullOrEmpty(state))
                //{
                //    result.message = "state is empty";
                //    result.valid = false;
                //    return result;
                //}

                if (!string.IsNullOrEmpty(state) && !new Regex("^[A-Z]{2}$").IsMatch(state))
                {
                    result.message = "Invalid state format";
                    result.valid = false;
                    return result;
                }

                //if (string.IsNullOrEmpty(zipcode))
                //{
                //    result.message = "zipcode is empty";
                //    result.valid = false;
                //    return result;
                //}

                if (!string.IsNullOrEmpty(zipcode) && !new Regex("^[0-9]{5}(-[0-9]{4})?$").IsMatch(zipcode))
                {
                    result.message = "Invalid zipcode format";
                    result.valid = false;
                    return result;
                }

                //if (string.IsNullOrEmpty(country))
                //{
                //    result.message = "country is empty";
                //    result.valid = false;
                //    return result;
                //}

                if (!string.IsNullOrEmpty(country) && !new Regex("^[A-Z]{3}$").IsMatch(country))
                {
                    result.message = "Invalid country format";
                    result.valid = false;
                    return result;
                }

                return result;
            }
        }
    }
}
