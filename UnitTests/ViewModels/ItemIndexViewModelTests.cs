using NUnit.Framework;
using Mine.ViewModels;
using Xamarin.Forms.Mocks;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Linq;

using Mine.Services;
using Mine.Models;

namespace UnitTests.ViewModels
{
    public class ItemIndexViewModelTests
    {
        ItemIndexViewModel ViewModel;

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            // Activate the Datastore
            DependencyService.Register<MockDataStore>();

            ViewModel = new ItemIndexViewModel();

            // Load Data
            ViewModel.ForceDataRefresh();
        }

        [Test]
        public async Task ItemIndexViewModel_Read_Invalid_ID_Bogus_Should_Fail()
        {
            // Arrange

            // Act
            var result = await ViewModel.Read("bogus");

            // Reset

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task ItemIndexViewModel_Read_Valid_ID_Should_Pass()
        {
            // Arrange

            // Find the First ID
            var first = ViewModel.Dataset.FirstOrDefault();

            // Act
            var result = await ViewModel.Read(first.Id);

            // Reset

            // Assert
            Assert.AreEqual(first.Name, result.Name);
        }

        [Test]
        public async Task ItemIndexViewModel_Update_Valid_Should_Pass()
        {
            // Arrange

            // Find the First ID
            var first = ViewModel.Dataset.FirstOrDefault();

            // Make a new item
            first.Name = "New Item";
            first.Value = 1000;

            // Act
            var result = await ViewModel.Update(first);

            // Reset

            // Assert
            Assert.AreEqual(true, result);  // Update returned Pas
            Assert.AreEqual("New Item", first.Name);  // The Name was updated
            Assert.AreEqual(1000, first.Value);  // The Value was updated
        }

        [Test]
        public async Task ItemIndexViewModel_Update_Invalid_Bogus_Should_Fail()
        {
            // Arrange

            // Update only updates what is in the list, so update on something that does not exist will fail
            var newData = new ItemModel();

            // Act
            var result = await ViewModel.Update(newData);

            // Reset

            // Assert
            Assert.AreEqual(false, result);  // Update returned fail
        }

        [Test]
        public async Task ItemIndexViewModel_Update_Invalid_Null_Should_Fail()
        {
            // Arrange

            // Act
            var result = await ViewModel.Update(null);

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public async Task ItemIndexViewModel_Add_Valid_Should_Pass()
        {
            // Arrange
            var data = new ItemModel
            {
                Name = "New Item"
            };

            // Act
            var result = await ViewModel.Add(data);

            // Reset

            // Need to clear the added item, and reload the dataset
            ViewModel.Dataset.Clear();
            ViewModel.ForceDataRefresh();

            // Assert
            Assert.AreEqual(true, result);  // Update returned Pass
        }

        [Test]
        public async Task ItemIndexViewModel_Add_InValid_Null_Should_Fail()
        {
            // Arrange

            // Act
            var result = await ViewModel.Add(null);

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void ItemIndexViewModel_SetNeedsRefresh_Valid_True_Should_Pass()
        {
            // Arrange
            var originalState = ViewModel.GetNeedsRefresh();

            // Act
            ViewModel.SetNeedsRefresh(true);
            var newState = ViewModel.GetNeedsRefresh();

            // Reset

            // Turn it back to the original state
            ViewModel.SetNeedsRefresh(originalState); 

            // Assert
            Assert.AreEqual(true, newState);
        }

        [Test]
        public void ItemIndexViewModel_NeedsRefresh_Valid_True_Should_Pass()
        {
            // Arrange
            var originalState = ViewModel.GetNeedsRefresh();

            ViewModel.SetNeedsRefresh(true);

            // Act
            var result = ViewModel.NeedsRefresh();

            // Reset

            // Turn it back to the original state
            ViewModel.SetNeedsRefresh(originalState);

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void ItemIndexViewModel_NeedsRefresh_Valid_False_Should_Pass()
        {
            // Arrange
            var originalState = ViewModel.GetNeedsRefresh();

            ViewModel.SetNeedsRefresh(false);

            // Act
            var result = ViewModel.NeedsRefresh();

            // Reset

            // Turn it back to the original state
            ViewModel.SetNeedsRefresh(originalState);

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public async Task ItemIndexViewModel_Delete_Valid_Should_Pass()
        {
            // Arrange
            var first = ViewModel.Dataset.FirstOrDefault();

            // Act
            var result = await ViewModel.Delete(first);
            var exists = await ViewModel.Read(first.Id);

            // Reset

            // Need to clear the added item, and reload the dataset
            ViewModel.Dataset.Clear();
            ViewModel.ForceDataRefresh();

            // Assert
            Assert.AreEqual(true, result);  // Delete returned pass
            Assert.AreEqual(null, exists);  // Should not exist so is null
        }

        [Test]
        public async Task ItemIndexViewModel_Delete_Invalid_Should_Fail()
        {
            // Arrange
            var data = new ItemModel
            {
                Id = "bogus"
            };

            // Act
            var result = await ViewModel.Delete(data);

            // Reset

            // Assert
            Assert.AreEqual(false, result);  // Delete returned fail
        }

        [Test]
        public async Task ItemIndexViewModel_Delete_Invalid_Null_Should_Fail()
        {
            // Arrange

            // Act
            var result = await ViewModel.Delete(null);

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void ItemIndexViewModel_ExecuteLoadDataCommand_Valid_Should_Pass()
        {
            // Arrange
            
            // Clear the Dataset, so no records
            ViewModel.Dataset.Clear();

            // Act
            ViewModel.LoadDatasetCommand.Execute(null);

            // Reset

            // Assert
            Assert.AreEqual(true, ViewModel.Dataset.Count()>0); // Check that there are rows of data
        }

        [Test]
        public void ItemIndexViewModel_ExecuteLoadDataCommand_InValid_Exception_Should_Fail()
        {
            // Arrange
            var oldDataset = ViewModel.Dataset;

            // Null dataset will throw

            ViewModel.Dataset = null;

            // Act
            ViewModel.LoadDatasetCommand.Execute(null);

            // Reset
            ViewModel.Dataset = oldDataset;

            // Assert
            Assert.AreEqual(true, ViewModel.Dataset.Count() > 0); // Check that there are rows of data
        }

        [Test]
        public void ItemIndexViewModel_ExecuteLoadDataCommand_Valid_IsBusy_Should_Pass()
        {
            // Arrange

            // Setting IsBusy will have the Load skip
            ViewModel.IsBusy = true;

            // Clear the Dataset, so no records
            ViewModel.Dataset.Clear();

            // Act
            ViewModel.LoadDatasetCommand.Execute(null);
            var count = ViewModel.Dataset.Count();  // Remember how many records exist

            // Reset
            ViewModel.IsBusy = false;
            ViewModel.ForceDataRefresh();

            // Assert
            Assert.AreEqual(0, count); // Count of 0 for the load was skipped
        }

        [Test]
        public async Task ItemIndexViewModel_Message_Delete_Valid_Should_Pass()
        {
            // Arrange
            MockForms.Init();
            ViewModel.Dataset.Clear();
            ViewModel.ForceDataRefresh();

            // Get the item to delete
            var first = ViewModel.Dataset.FirstOrDefault();

            // Add it to the View Model
            var viewModel = new ItemViewModel
            {
                Data = first
            };

            // Make a Delete Page
            var myPage = new Mine.Views.ItemDeletePage(viewModel);

            // Act
            MessagingCenter.Send(myPage, "Delete", viewModel.Data);

            var data = await ViewModel.Read(first.Id);

            // Reset
            ViewModel.Dataset.Clear();
            ViewModel.ForceDataRefresh();

            // Assert
            Assert.AreEqual(null,data); // Item is removed
        }

        [Test]
        public void ItemIndexViewModel_Message_Create_Valid_Should_Pass()
        {
            // Arrange
            MockForms.Init();
            ViewModel.Dataset.Clear();
            ViewModel.ForceDataRefresh();

            // Make a new Item
            var data = new ItemModel();

            // Add it to the View Model
            var viewModel = new ItemViewModel
            {
                Data = data
            };

            // Make a Delete Page
            var myPage = new Mine.Views.ItemCreatePage(viewModel);

            var countBefore = ViewModel.Dataset.Count();

            // Act
            MessagingCenter.Send(myPage, "Create", viewModel.Data);
            var countAfter = ViewModel.Dataset.Count();

            // Reset
            ViewModel.Dataset.Clear();
            ViewModel.ForceDataRefresh();

            // Assert
            Assert.AreEqual(countBefore + 1, countAfter); // Count of 0 for the load was skipped
        }

        [Test]
        public async Task ItemIndexViewModel_Message_Update_Valid_Should_Pass()
        {
            // Arrange
            MockForms.Init();
            ViewModel.Dataset.Clear();
            ViewModel.ForceDataRefresh();

            // Get the item to delete
            var first = ViewModel.Dataset.FirstOrDefault();
            first.Name = "test";

            // Add it to the View Model
            var viewModel = new ItemViewModel
            {
                Data = first
            };

            // Make a Delete Page
            var myPage = new Mine.Views.ItemUpdatePage(viewModel);

            var countBefore = ViewModel.Dataset.Count();

            // Act
            MessagingCenter.Send(myPage, "Update", viewModel.Data);
            var result = await ViewModel.Read(first.Id);

            // Reset
            ViewModel.Dataset.Clear();
            ViewModel.ForceDataRefresh();

            // Assert
            Assert.AreEqual("test", result.Name); // Count of 0 for the load was skipped
        }
    }
}