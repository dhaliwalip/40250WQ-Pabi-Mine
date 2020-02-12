using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mine.Views;

namespace UnitTests.Views.Game
{
    [TestFixture]
    public class AboutPageTests
    {
        [Test]
        public void AboutPage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = new AboutPage();

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
