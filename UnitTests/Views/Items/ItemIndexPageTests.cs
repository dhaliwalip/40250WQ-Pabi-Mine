using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mine.Views;
using Mine.ViewModels;
using Mine.Models;
using Xamarin.Forms;

namespace UnitTests.Views.Game
{
    [TestFixture]
    public class ItemIndexPageTests : ItemIndexPage
    {
        [Test]
        public void ItemIndexPage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = new ItemIndexPage();

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ItemIndexPage_AddItem_Clicked_Default_Should_Pass()
        {
            // Arrange
            var ViewModel = new ItemViewModel();

            var page = new ItemIndexPage();

            // Act
            page.AddItem_Clicked(null,null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemIndexPage_OnItemSelected_Clicked_Default_Should_Pass()
        {
            // Arrange
            var ViewModel = new ItemViewModel();

            var page = new ItemIndexPage();

            var selectedItem = new ItemModel();

            var selectedItemChangedEventArgs = new SelectedItemChangedEventArgs(selectedItem,0);


            // Act
            page.OnItemSelected(null, selectedItemChangedEventArgs);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemIndexPage_OnItemSelected_Clicked_Invalid_Null_Should_Fail ()
        {
            // Arrange
            var ViewModel = new ItemViewModel();

            var page = new ItemIndexPage();

            var selectedItemChangedEventArgs = new SelectedItemChangedEventArgs(null, 0);


            // Act
            page.OnItemSelected(null, selectedItemChangedEventArgs);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemIndexPage_OnAppearing_Valid_Should_Pass()
        {
            // Arrange
            var ViewModel = new ItemViewModel();

            // Act
            OnAppearing();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
    }
}