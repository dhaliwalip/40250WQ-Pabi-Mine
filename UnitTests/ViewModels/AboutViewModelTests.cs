using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mine.ViewModels;

namespace UnitTests.ViewModels
{
    [TestFixture]
    public class AboutViewModelTests
    {
        [Test]
        public void AboutViewModel_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = new AboutViewModel();

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void AboutViewModel_Get_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = new AboutViewModel();

            // Reset

            // Assert
            Assert.AreEqual("About", result.Title);
        }
    }
}
