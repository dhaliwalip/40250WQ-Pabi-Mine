using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Mine.Views;
using System.Reflection;
using Xamarin.Forms.Mocks;

namespace UnitTests.Views.Game
{
    [TestFixture]
    public class GamePageTests : GamePage
    {
        [Test]
        public void GamePage_Constructor_Default_Should_Pass()
        {
            // Arrange
            MockForms.Init();
            var page = new GamePage();

            // Act
            var result = page.Content;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void GamePage_GameButton_Clicked_Default_Should_Pass()
        {
            // Arrange

            var page = new GamePage();

            // Act
            page.GameButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void GamePage_Get_Default_Should_Pass()
        {
            // Arrange

            var page = new GamePage();

            // Act
            var result = page.Content;

            // Reset

            // Assert
            Assert.NotNull(result); 
        }


        [Test]
        public void GamePage_OnBindingContextChanged_Default_Should_Pass()
        {
            // Arrange

            var page = new GamePage();

            // Act
            OnBindingContextChanged();

            // Reset

            // Assert
            Assert.IsTrue(true);
        }
    }
}
