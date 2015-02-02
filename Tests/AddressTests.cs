using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FoodPantryLib.Tests
{
    [TestFixture]
    public class AddressTests
    {
        [TestCase]
        public void IsValidAddress_Success()
        {
            Address testObject = new Address { address1 = "123 Main St.", city = "Boise", state = "ID", zipcode = "83701", country = "USA" };
            Assert.AreEqual(true, testObject.isValid.valid);
        }

        [TestCase]
        public void IsValidAddress_Plus4Zip_Success()
        {
            Address testObject = new Address { address1 = "123 Main St.", city = "Boise", state = "ID", zipcode = "83701-1234", country = "USA" };
            Assert.AreEqual(true, testObject.isValid.valid);
        }

        [TestCase]
        public void IsValidAddress_InvalidZip1_Failure()
        {
            Address testObject = new Address { address1 = "123 Main St.", city = "Boise", state = "ID", zipcode = "83701-", country = "USA" };
            Assert.AreEqual(false, testObject.isValid.valid);
            Console.WriteLine(testObject.isValid.message);
        }

        [TestCase]
        public void IsValidAddress_InvalidZip2_Failure()
        {
            Address testObject = new Address { address1 = "123 Main St.", city = "Boise", state = "ID", zipcode = "83701-123", country = "USA" };
            Assert.AreEqual(false, testObject.isValid.valid);
            Console.WriteLine(testObject.isValid.message);
        }

        [TestCase]
        public void IsValidAddress_InvalidZip3_Failure()
        {
            Address testObject = new Address { address1 = "123 Main St.", city = "Boise", state = "ID", zipcode = "837011234", country = "USA" };
            Assert.AreEqual(false, testObject.isValid.valid);
            Console.WriteLine(testObject.isValid.message);
        }

        [TestCase]
        public void IsValidAddress_InvalidZip4_Failure()
        {
            Address testObject = new Address { address1 = "123 Main St.", city = "Boise", state = "ID", zipcode = "83701-12345", country = "USA" };
            Assert.AreEqual(false, testObject.isValid.valid);
            Console.WriteLine(testObject.isValid.message);
        }

        [TestCase, Ignore]
        public void IsValidAddress_InvalidZip5_Failure()
        {
            Address testObject = new Address { address1 = "123 Main St.", city = "Boise", state = "ID", zipcode = string.Empty, country = "USA" };
            Assert.AreEqual(false, testObject.isValid.valid);
            Console.WriteLine(testObject.isValid.message);
        }

        [TestCase, Ignore]
        public void IsValidAddress_InvalidZip6_Failure()
        {
            Address testObject = new Address { address1 = "123 Main St.", city = "Boise", state = "ID", zipcode = null, country = "USA" };
            Assert.AreEqual(false, testObject.isValid.valid);
            Console.WriteLine(testObject.isValid.message);
        }

        [TestCase]
        public void IsValidAddress_InvalidState1_Failure()
        {
            Address testObject = new Address { address1 = "123 Main St.", city = "Boise", state = "I", zipcode = "83701", country = "USA" };
            Assert.AreEqual(false, testObject.isValid.valid);
            Console.WriteLine(testObject.isValid.message);
        }

        [TestCase]
        public void IsValidAddress_InvalidState2_Failure()
        {
            Address testObject = new Address { address1 = "123 Main St.", city = "Boise", state = "ID ", zipcode = "83701", country = "USA" };
            Assert.AreEqual(false, testObject.isValid.valid);
            Console.WriteLine(testObject.isValid.message);
        }

        [TestCase, Ignore]
        public void IsValidAddress_InvalidState3_Failure()
        {
            Address testObject = new Address { address1 = "123 Main St.", city = "Boise", state = string.Empty, zipcode = "83701", country = "USA" };
            Assert.AreEqual(false, testObject.isValid.valid);
            Console.WriteLine(testObject.isValid.message);
        }

    }
}
