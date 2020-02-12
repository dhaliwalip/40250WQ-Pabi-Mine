using Mine.Services;
using Mine.Models;
using Mine.Views;
using System.Diagnostics;
using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Linq;

namespace Mine.ViewModels
{
    /// <summary>
    /// Index View Model
    /// Manages the list of data records
    /// </summary>
    public class ItemIndexViewModel : BaseViewModel
    {
        // The Data set of records
        public ObservableCollection<ItemModel> Dataset { get; set; }

        /// <summary>
        /// Connection to the Data store
        /// </summary>
        //public IDataStore<ItemModel> DataStore => DependencyService.Get<IDataStore<ItemModel>>();
        public IDataStore<ItemModel> DataSource_Mock => new MockDataStore();
        public IDataStore<ItemModel> DataSource_SQL => new DatabaseService();
        public IDataStore<ItemModel> DataStore;

        public int CurrentDataSource = 0;

        public bool SetDataSource(int isSql)
        {
            if (isSql == 1)
            {
                DataStore = DataSource_SQL;
            }
            else
            {
                DataStore = DataSource_Mock;
            }

            SetNeedsRefresh(true);
            return true;
        }
        // Command to force a Load of data
        public Command LoadDatasetCommand { get; set; }

        private bool _needsRefresh;

        /// <summary>
        /// Constructor
        /// 
        /// The constructor subscribes message listeners for crudi operations
        /// </summary>
        public ItemIndexViewModel()
        {
            SetDataSource(0);
            Title = "Items";

            Dataset = new ObservableCollection<ItemModel>();
            LoadDatasetCommand = new Command(async () => await ExecuteLoadDataCommand());

            // Register the Create Message
            MessagingCenter.Subscribe<ItemCreatePage, ItemModel>(this, "Create", async (obj, data) =>
            {
                await CreateAsync(data as ItemModel);
            });

            // Register the Delete Message
            MessagingCenter.Subscribe<ItemDeletePage, ItemModel>(this, "Delete", async (obj, data) =>
            {
                await DeleteAsync(data as ItemModel);
            });

            // Register the Update Message
            MessagingCenter.Subscribe<ItemUpdatePage, ItemModel>(this, "Update", async (obj, data) =>
            {
                await UpdateAsync(data as ItemModel);
            });

            //Register the Set Data Source Message
            MessagingCenter.Subscribe<AboutPage, int>(this, "SetDataSource", (obj, data) =>
             {
                 SetDataSource(data);
             });

            //Register the update Message
            MessagingCenter.Subscribe<AboutPage, int>(this, "SetDataSource", (obj, data) =>
            {
                SetDataSource(data);
            });
        }

        /// <summary>
        /// API to Read the Data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<ItemModel> Read(string id)
        {
            var result = await DataStore.ReadAsync(id);
            return result;
        }

        /// <summary>
        /// API to add the Data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> Add(ItemModel data)
        {
            // Don't try to add bad records
            if (data == null)
            {
                return false;
            }

            Dataset.Add(data);
            var result = await DataStore.CreateAsync(data);

            return true;
        }

        /// <summary>
        /// API to Delete the Data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> Delete(ItemModel data)
        {
            if (data == null)
            {
                return false;
            }

            // Check that the record exists, if it does not, then exit with false
            var record = await Read(data.Id);
            if (record == null)
            {
                return false;
            }

            Dataset.Remove(data);

            var result = await DataStore.DeleteAsync(data.Id);

            return result;
        }

        /// <summary>
        /// API to Update the Data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> Update(ItemModel data)
        {
            if (data == null)
            {
                return false;
            }

            // Check that the record exists, if it does not, then exit with false
            var record = await Read(data.Id);
            if (record == null)
            {
                return false;
            }

            record.Update(data);

            var result = await DataStore.UpdateAsync(record);

            await ExecuteLoadDataCommand();

            return result;
        }

        #region Refresh
        // Return True if a refresh is needed
        // It sets the refresh flag to false
        public bool NeedsRefresh()
        {
            if (_needsRefresh)
            {
                _needsRefresh = false;
                return true;
            }

            return false;
        }

        public bool GetNeedsRefresh()
        {
            return _needsRefresh;
        }

        // Sets the need to refresh
        public void SetNeedsRefresh(bool value)
        {
            _needsRefresh = value;
        }

        // Command that Loads the Data
        private async Task ExecuteLoadDataCommand()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                if (Dataset == null)
                {
                    throw new InvalidOperationException();
                }

                Dataset.Clear();
                var dataset = await DataStore.IndexAsync();

                // Example of how to sort the database output using a linq query.
                // Sort the list
                dataset = dataset
                    .OrderBy(a => a.Name)
                    .ThenBy(a => a.Description)
                    .ToList();

                foreach (var data in dataset)
                {
                    Dataset.Add(data);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Force data to refresh
        /// </summary>
        public void ForceDataRefresh()
        {
            // Reset
            var canExecute = LoadDatasetCommand.CanExecute(null);
            LoadDatasetCommand.Execute(null);
        }
        #endregion Refresh

        /// <summary>
        /// API to add the Data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(ItemModel data)
        {
            Dataset.Add(data);
            var result = await DataStore.CreateAsync(data);

            SetNeedsRefresh(true);

            return result;
        }


        /// <summary>
        /// Get the data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ItemModel> ReadAsync(string id)
        {
            var myData = await DataStore.ReadAsync(id);
            return myData;
        }

        /// <summary>
        /// Update the data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(ItemModel data)
        {
            // Check that the record exists, if it does not, then exit with false
            var record = await ReadAsync(((ItemModel)(object)data).Id);
            if (record == null)
            {
                return false;
            }

            // Save the change to the Data Store
            var result = await DataStore.UpdateAsync(record);

            SetNeedsRefresh(true);

            return result;
        }


        /// <summary>
        /// Delete the data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(ItemModel data)
        {
            // Check that the record exists, if it does not, then exit with false
            var record = await ReadAsync(((ItemModel)(object)data).Id);
            if (record == null)
            {
                return false;
            }

            // remove the record from the current data set in the viewmodel
            Dataset.Remove(data);

            // Have the record deleted from the data source
            var result = await DataStore.DeleteAsync(((ItemModel)(object)record).Id);

            SetNeedsRefresh(true);

            return result;
        }

        /// <summary>
        /// Having this at the ViewModel, because it has the DataStore
        /// That allows the feature to work for both SQL and the Mock datastores...
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> CreateUpdateAsync(ItemModel data)
        {
            // Check to see if the data exist
            var oldData = await ReadAsync(((ItemModel)(object)data).Id);
            if (oldData == null)
            {
                await CreateAsync(data);
                return true;
            }

            // Compare it, if different update in the DB
            var UpdateResult = await UpdateAsync(data);
            if (UpdateResult)
            {
                return true;
            }

            return false;
        }
    }
}