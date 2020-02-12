using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mine.Views;
using Xamarin.Forms.Mocks;
using Xamarin.Forms;
using Mine.Models;

namespace UnitTests.Views.Game
{
    [TestFixture]
    public class MenuPageTests
    {
        [Test]
        public void MenuPage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Initilize Xamarin Forms
            MockForms.Init();

            // Act
            var result = new MenuPage();

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void MenuPage_Get_Default_Should_Pass()
        {
            // Arrange

            // Initilize Xamarin Forms
            MockForms.Init();

            var page = new MenuPage();
            // Act
            var result = page.RootPage;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void MenuPage_ListViewMenu_InValid_Null_Should_Fail()
        {
            // Arrange

            // Initilize Xamarin Forms
            MockForms.Init();

            var page = new MenuPage();
            var content = (StackLayout)page.Content;
            var listview = (ListView)content.Children.FirstOrDefault();

            // Act
            listview.SelectedItem = null;

            // Reset

            // Assert
            Assert.IsTrue(true);
        }

        [Test]
        public void MenuPage_ListViewMenu_Valid_Game_Should_Pass()
        {
            // Arrange

            // Initilize Xamarin Forms
            MockForms.Init();

            var data = new HomeMenuItemModel { Id = MenuItemEnum.Game, Title = "Game" };

            var page = new MenuPage();
            var content = (StackLayout)page.Content;
            var listview = (ListView)content.Children.FirstOrDefault();

            // Act
            listview.SelectedItem = data;

            // Reset

            // Assert
            Assert.IsTrue(true);
        }

    }
}