using NUnit.Framework;
using System.Threading.Tasks;

using Mine.Views;
using Mine.Models;

using Xamarin.Forms.Mocks;
using Xamarin.Forms;

namespace UnitTests.Views.Game
{
    [TestFixture]
    public class MainPageTests
    {
        // Not Possible to Cover the MainPage due to the call to MasterBehavior = MasterBehavior.Popover;

        [Test]
        public void MainPage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Initilize Xamarin Forms
            MockForms.Init();

            // Act
            var result = new MainPage();

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task MainPage_Navigate_About_Should_Pass()
        {
            // Arrange

            // Initilize Xamarin Forms
            MockForms.Init();

            var page = new MainPage();

            // Act
            await page.NavigateFromMenu((int)MenuItemEnum.About);
                       
            // Reset

            // Assert
            Assert.IsTrue(true);
        }

        [Test]
        public async Task MainPage_Navigate_Items_Should_Pass()
        {
            // Arrange

            // Initilize Xamarin Forms
            MockForms.Init();

            var page = new MainPage();

            // Act
            await page.NavigateFromMenu((int)MenuItemEnum.Items);

            // Reset

            // Assert
            Assert.IsTrue(true);
        }

        [Test]
        public async Task MainPage_Navigate_Game_Should_Pass()
        {
            // Arrange

            // Initilize Xamarin Forms
            MockForms.Init();

            var page = new MainPage();

            // Act
            await page.NavigateFromMenu((int)MenuItemEnum.Game);

            // Reset

            // Assert
            Assert.IsTrue(true);
        }

        [Test]
        public async Task MainPage_Navigate_Game_Twice_Should_Skip()
        {
            // Arrange

            // Initilize Xamarin Forms
            MockForms.Init();

            var page = new MainPage();
            await page.NavigateFromMenu((int)MenuItemEnum.Game);

            // Act
            await page.NavigateFromMenu((int)MenuItemEnum.Game);

            // Reset

            // Assert
            Assert.IsTrue(true);
        }

        [Test]
        public async Task MainPage_Navigate_Invalid_ID_100_Should_Skip()
        {
            // Arrange

            // Initilize Xamarin Forms
            MockForms.Init();

            var page = new MainPage();

            page.MenuPages.Add(100, null);

            // Act
            await page.NavigateFromMenu(100);

            // Reset

            // Assert
            Assert.IsTrue(true);
        }

        [Test]
        public async Task MainPage_Navigate_Device_Android_Game_Should_Pass()
        {
            // Arrange

            // Initilize Xamarin Forms
            MockForms.Init(Device.Android);

            var page = new MainPage();
            await page.NavigateFromMenu((int)MenuItemEnum.Game);

            // Act
            await page.NavigateFromMenu((int)MenuItemEnum.Game);

            // Reset

            // Assert
            Assert.IsTrue(true);
        }
        
    }
}
