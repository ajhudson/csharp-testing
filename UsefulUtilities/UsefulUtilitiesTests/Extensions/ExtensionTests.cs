using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsefulUtilities.Extensions;

namespace UsefulUtilitiesTests.Extensions
{
    [TestClass]
    public class ExtensionTests
    {
        [TestMethod]
        public void IfFromAndToAreNullThenAllResultsAreReturned()
        {
            // Arrange
            var items = this.GetTestData();

            // Act
            var result = items.WhereInDateRange(null, null, x => x.WhenSomethingHappened).ToList();

            // Assert
            Assert.AreEqual(4, result.Count());
        }

        [TestMethod]
        public void IfFromAndToAreCorrectlyPopulatedThenResultsAreReturned()
        {
            // Arrange 
            var items = this.GetTestData();
            DateTime? from = new DateTime(2020, 4, 1);
            DateTime? to = new DateTime(2020, 6, 29);

            // Act
            var result = items.WhereInDateRange(from, to, x => x.WhenSomethingHappened).ToList();

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(2, result[0].Id);
            Assert.AreEqual(3, result[1].Id);
        }

        [TestMethod]
        public void IfFromNotDefinedAndToIsDefinedThenResultsAreReturned()
        {
            // Arrange
            var items = this.GetTestData();
            DateTime? from = null;
            DateTime? to = new DateTime(2020, 5, 30);

            // Act
            var result = items.WhereInDateRange(from, to, x => x.WhenSomethingHappened).ToList();

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual(2, result[1].Id);
        }

        [TestMethod]
        public void IfFromDefinedAndToNotDefinedThenResultsAreReturned()
        {
            // Arrange
            var items = this.GetTestData();
            DateTime? from = new DateTime(2020, 5, 1);
            DateTime? to = null;

            // Act
            var result = items.WhereInDateRange(from, to, x => x.WhenSomethingHappened).ToList();

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(3, result[0].Id);
            Assert.AreEqual(4, result[1].Id);
        }

        [TestMethod]
        public void IfDateRangeIsMuddledThenItIsCorrectedAndResultsAreReturned()
        {
            // Arrange
            var items = this.GetTestData();
            DateTime? from = new DateTime(2020, 6, 29);
            DateTime? to = new DateTime(2020, 4, 1);


            // Act
            var result = items.WhereInDateRange(from, to, x => x.WhenSomethingHappened).ToList();

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(2, result[0].Id);
            Assert.AreEqual(3, result[1].Id);
        }

        private List<Item> GetTestData()
        {
            var items = new List<Item>
            {
                new Item { Id = 1, WhenSomethingHappened = new DateTime(2020, 3, 31) },
                new Item { Id = 2, WhenSomethingHappened = new DateTime(2020, 4, 30) },
                new Item { Id = 3, WhenSomethingHappened = new DateTime(2020, 5, 31) },
                new Item { Id = 4, WhenSomethingHappened = new DateTime(2020, 6, 30) }
            };

            return items;
        }
    }

    public class Item
    {
        public int Id { get; set; }

        public DateTime? WhenSomethingHappened { get; set; }
    }
}
