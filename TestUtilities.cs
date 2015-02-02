using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPantryLib
{
    public static class TestUtilities
    {
        public static void DumpRecipient(Recipient r)
        {
            try
            {
                Console.WriteLine(string.Format("id: {0}", r.id));
                Console.WriteLine(string.Format("first: {0}", r.first));
                Console.WriteLine(string.Format("last: {0}", r.last));
                Console.WriteLine(string.Format("phone: {0}", r.phone));
                Console.WriteLine(string.Format("address1: {0}", r.address.address1));
                Console.WriteLine(string.Format("address2: {0}", r.address.address2));
                Console.WriteLine(string.Format("city: {0}", r.address.city));
                Console.WriteLine(string.Format("state: {0}", r.address.state));
                Console.WriteLine(string.Format("zipcode: {0}", r.address.zipcode));
                Console.WriteLine(string.Format("country: {0}", r.address.country));
            }
            catch
            {
            }
        }

        public static string getFirstName()
        {
            return "John";
        }

        public static string getLastName()
        {
            return "Doe";
        }

        public static Phone getPhone()
        {
            return new Phone("1231231234");
        }

        public static Address getAddress()
        {
            return new Address { address1 = "123 Main St.", city = "Boise", state = "ID", zipcode = "83701", country = "USA" };
        }
    }
}
