using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mine.Views;
using Mine.ViewModels;
using Mine.Models;

namespace UnitTests.Views.Game
{
    [TestFixture]
    public class ItemDeletePageTests : ItemDeletePage
    {
        [Test]
        public void ItemDeletePage_Constructor_Default_Should_Pass()
        {
            // Arrange
            var ViewModel = new ItemViewModel();

            // Act
            var result = new ItemDeletePage(ViewModel);

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ItemDeletePage_Delete_Clicked_Default_Should_Pass()
        {
            // Arrange
            var ViewModel = new ItemViewModel();

            var page = new ItemDeletePage(ViewModel);

            // Act
            page.Delete_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemDeletePage_Cancel_Clicked_Default_Should_Pass()
        {
            // Arrange
            var ViewModel = new ItemViewModel();

            var page = new ItemDeletePage(ViewModel);

            // Act
            page.Cancel_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemDeletePage_OnBackButtonPressed_Valid_Should_Pass()
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