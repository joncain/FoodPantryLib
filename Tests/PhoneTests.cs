using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FoodPantryLib.Tests
{
    [TestFixture]
    public class PhoneTests
    {
        [TestCase]
        public void IsValidPhone_Success()
        {
            Phone testObject = new Phone("1231231234");
            Assert.AreEqual(true, testObject.isValid);
        }

        [TestCase]
        public void IsValidPhone_Accessor_Success()
        {
            Phone testObject = new Phone("1231231234");
            Assert.AreEqual("1231231234", testObject.value);
        }

        [TestCase]
        public void IsValidPhone_ToString_Success()
        {
            Phone testObject = new Phone("1231231234");
            Assert.AreEqual("1231231234", testObject.ToString());
        }

        [TestCase]
        public void IsValidPhone_Empty_Failure()
        {
            Phone testObject = new Phone();
            Assert.AreEqual(false, testObject.isValid);
        }

        [TestCase]
        public void IsValidPhone_Null_Failure()
        {
            Phone testObject = new Phone(null);
            Assert.AreEqual(false, testObject.isValid);
        }

        [TestCase]
        public void IsValidPhone_Chars_Failure()
        {
            Phone testObject = new Phone("123a231234");
            Assert.AreEqual(false, testObject.isValid);
        }

    }
}
