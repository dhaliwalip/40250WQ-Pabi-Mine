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
    public class ItemUpdatePageTests: ItemUpdatePage
    {
        [Test]
        public void ItemUpdatePage_Constructor_Default_Should_Pass()
        {
            // Arrange
            var ViewModel = new ItemViewModel();

            // Act
            var result = new ItemUpdatePage(ViewModel);

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ItemUpdatePage_Save_Clicked_Default_Should_Pass()
        {
            // Arrange
            var ViewModel = new ItemViewModel();

            var page = new ItemUpdatePage(ViewModel);

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemUpdatePage_Cancel_Clicked_Default_Should_Pass()
        {
            // Arrange
            var ViewModel = new ItemViewModel();

            var page = new ItemUpdatePage(ViewModel);

            // Act
            page.Cancel_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemUpdatePage_Value_OnStepperValueChanged_Default_Should_Pass()
        {
            // Arrange
            var ViewModel = new ItemViewModel();

            var page = new ItemUpdatePage(ViewModel);
            double oldValue = 0.0;
            double newValue = 1.0;

            var args = new ValueChangedEventArgs(oldValue, newValue);

            // Act
            page.Value_OnStepperValueChanged(null, args);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemIndexPage_OnBackButtonPressed_Valid_Should_Pass()
        {
            // Arrange

            // Act
            OnBackButtonPressed();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
    }
}