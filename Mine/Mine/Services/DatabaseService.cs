﻿using SQLite;
using System;
using System.Linq;
using System.Threading.Tasks;
using Mine.Models;
using System.Collections.Generic;

namespace Mine.Services
{
    /// <summary>
    /// Database Services
    /// Will write to the local data store
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DatabaseService : IDataStore<ItemModel>
    {
        /// <summary>
        /// Set the class to load on demand
        /// Saves app boot time
        /// </summary>
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        /// <summary>
        /// Constructor
        /// All the database to start up
        /// </summary>
        public DatabaseService()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        /// <summary>
        /// Create the Table if it does not exist
        /// </summary>
        /// <returns></returns>
        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(ItemModel).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(ItemModel)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        /// <summary>
        /// Create a new record for the data passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Task<bool> CreateAsync(ItemModel data)
        {
            Database.InsertAsync(data);
            return Task.FromResult(true);
        }

        /// <summary>
        /// Return the record for the ID passed in
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ItemModel> ReadAsync(string id)
        {
            return await Database.Table<ItemModel>().Where(i => i.Id.Equals(id)).FirstOrDefaultAsync();
        }


        /// <summary>
        /// Update the record passed in if it exists
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(ItemModel Data)
        {
            var myRead = await ReadAsync(((ItemModel)(object)Data).Id);
            if (myRead == null)
            {
                return false;
            }

            var result = await Database.UpdateAsync(Data);

            return await Task.FromResult(true);
        }

        /// <summary>
        /// Delete the record of the ID passed in
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(string id)
        {
            var data = await ReadAsync(id);
            if (data == null)
            {
                return false;
            }

            var result = await Database.DeleteAsync(data);

            return (result == 1);
        }

        /// <summary>
        /// Return all records in the database
        /// </summary>
        /// <returns></returns>
        public async Task<List<ItemModel>> IndexAsync()
        {
            return await Database.Table<ItemModel>().ToListAsync();
        }

        /// <summary>
        /// Wipe Data List
        /// Drop the tables and create new ones
        /// </summary>
        public async Task<bool> WipeDataListAsync()
        {
            try
            {
                await Database.DropTableAsync<ItemModel>().ConfigureAwait(false);
                await Database.CreateTablesAsync(CreateFlags.None, typeof(ItemModel));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error WipeData" + e.Message);
            }

            return await Task.FromResult(true);
        }
    }
}