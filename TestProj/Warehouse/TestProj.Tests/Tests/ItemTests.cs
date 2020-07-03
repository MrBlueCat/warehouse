using System;
using System.Collections.Generic;
using System.Text;
using Warehouse;
using Xunit;

namespace TestProj.Tests
{
    public class ItemTests
    {
        [Fact]
        public void ToStringResultNotNullOrEmpty()
        {
            // Arrange
            Item item = new Item(1, DateTime.Now, "Name1", "");

            // Act
            string result = item.ToString();

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(0, result.Length);
        }

        [Fact]
        public void ToStringResultHasAllProperties()
        {
            // Arrange
            Item item = new Item(1, DateTime.Now, "Name1", "");

            // Act
            string result = item.ToString();

            // Assert
            Assert.Contains("Name1", result);
            Assert.Contains(DateTime.Now.ToShortDateString(), result);
        }
    }
}
