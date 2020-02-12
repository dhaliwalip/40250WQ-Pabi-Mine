using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mine.Models;
using Mine.ViewModels;

namespace UnitTests.ViewModels
{
    [TestFixture]
    public class ItemViewModelTests
    {
        [Test]
        public void ItemViewModel_Constructor_Valid_Data_Should_Pass()
        {
            // Arrange
            var data = new ItemModel();
            data.Name = "Name";

            // Act
            var result = new ItemViewModel(data);
            // Reset

            // Assert
            Assert.AreEqual("Name", result.Data.Name);
        }
    }
}
