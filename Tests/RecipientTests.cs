using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FoodPantryLib.Tests
{
    [TestFixture]
    public class RecipientTests
    {

        [TestCase]
        public void GetRecipient_Success()
        {
            Recipient testObject = new Recipient();
            testObject.id = 1;

            if (!testObject.Get())
            {
                Console.WriteLine(testObject.errorMessage);
            }
            else
            {
                TestUtilities.DumpRecipient(testObject);
            }

            Assert.AreEqual(false, string.IsNullOrEmpty(testObject.first));
        }
        
        [TestCase]
        public void SaveRecipient_NoFirstName_Failure()
        {
            Recipient testObject = new Recipient {id = 1, last = TestUtilities.getLastName() };
            SaveResult result = testObject.Save();
            Console.WriteLine(result.message);
            Assert.AreEqual(false, result.success);
        }

        [TestCase]
        public void SaveRecipient_NoLastName_Failure()
        {
            Recipient testObject = new Recipient { id = 1, first = TestUtilities.getFirstName() };
            SaveResult result = testObject.Save();
            Console.WriteLine(result.message);
            Assert.AreEqual(false, result.success);
        }

        [TestCase]
        public void SaveRecipient_InvalidPhone_Failure()
        {
            Recipient testObject = new Recipient { id = 1, first = TestUtilities.getFirstName(), last = TestUtilities.getLastName(), phone = new Phone("123") };
            SaveResult result = testObject.Save();
            Console.WriteLine(result.message);
            Assert.AreEqual(false, result.success);
        }

        [TestCase]
        public void SaveRecipient_MinimumData_Success()
        {
            Recipient testObject = new Recipient { id = 1, first = TestUtilities.getFirstName(), last = TestUtilities.getLastName(), dob = DateTime.Now };
            SaveResult result = testObject.Save();
            Console.WriteLine(result.message);
            Assert.AreEqual(true, result.success);
        }

        [TestCase]
        public void SaveRecipient_WithPhone_Success()
        {
            Recipient testObject = new Recipient { id = 1, first = TestUtilities.getFirstName(), last = TestUtilities.getLastName(), dob = DateTime.Now, phone = TestUtilities.getPhone() };
            SaveResult result = testObject.Save();
            Console.WriteLine(result.message);
            Assert.AreEqual(true, result.success);
        }

        [TestCase]
        public void SaveRecipient_WithAddress_Success()
        {
            Recipient testObject = new Recipient { id = 1, first = TestUtilities.getFirstName(), last = TestUtilities.getLastName(), dob = DateTime.Now,address = TestUtilities.getAddress() };
            SaveResult result = testObject.Save();
            Console.WriteLine(result.message);
            Assert.AreEqual(true, result.success);
        }

        [TestCase]
        public void SaveRecipient_WithAddressAndPhone_Success()
        {
            Recipient testObject = new Recipient {id = 1, first = TestUtilities.getFirstName(), last = TestUtilities.getLastName(), dob = DateTime.Now, address = TestUtilities.getAddress(), phone = TestUtilities.getPhone() };
            SaveResult result = testObject.Save();
            Console.WriteLine(result.message);
            Assert.AreEqual(true, result.success);
        }

        [TestCase]
        public void CreateRecipient_WithAddressAndPhone_Success()
        {
            Recipient testObject = new Recipient { first = TestUtilities.getFirstName(), last = TestUtilities.getLastName(), dob = DateTime.Now, address = TestUtilities.getAddress(), phone = TestUtilities.getPhone() };
            SaveResult result = testObject.Save();
            Console.WriteLine(result.message);

            TestUtilities.DumpRecipient(testObject);

            Assert.AreEqual(true, result.success);
        }

       
    }
}
