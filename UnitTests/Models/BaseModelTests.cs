using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mine.Models;

namespace UnitTests.Models
{
    [TestFixture]
    public class BaseModelTests
    {
        [Test]
        public void BaseModel_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = new BaseModel();

            // Reset

            // Assert
            Assert.AreEqual("This is an Item", result.Name);

        }

        [Test]
        public void BaseModel_Set_Default_Should_Pass()
        {
            // Arrange
            var result = new BaseModel();

            // Act
            result.Id = "bogus";

            // Reset

            // Assert
            Assert.AreEqual("bogus", result.Id);

        }
    }
}
