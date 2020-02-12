using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

using Mine.Views;
using Mine.ViewModels;
using Mine.Models;

namespace UnitTests.Views.Game
{
    [TestFixture]
    public class ItemReadPageTests
    {
        [Test]
        public void ItemReadPage_Constructor_Default_Should_Pass()
        {
            // Arrange
            var ViewModel = new ItemViewModel();

            // Act
            var result = new ItemReadPage(ViewModel);

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ItemReadPage_Delete_Clicked_Default_Should_Pass()
        {
            // Arrange
            var ViewModel = new ItemViewModel();

            var page = new ItemReadPage(ViewModel);

            // Act
            page.Delete_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemReadPage_Update_Clicked_Default_Should_Pass()
        {
            // Arrange
            var ViewModel = new ItemViewModel();

            var page = new ItemReadPage(ViewModel);

            // Act
            page.Update_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
    }
}
