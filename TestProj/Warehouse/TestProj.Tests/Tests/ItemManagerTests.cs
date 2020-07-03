using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Warehouse;
using Xunit;

namespace TestProj.Tests.Tests
{
    public class ItemManagerTests
    {
        Item testItem;
        Mock<IRepository<Item>> mockRepository;

        public ItemManagerTests()
        {
            testItem = new Item(1, DateTime.Now, "It", "King");

            mockRepository = new Mock<IRepository<Item>>();

            mockRepository.Setup(x => x.Insert(testItem)).Returns(true);

            mockRepository.Setup(x => x.GetElementBy((t) => t.Name.Equals("It"))).Returns(testItem);
            mockRepository.Setup(x => x.Update(testItem)).Returns(true);

        }

        [Fact]
        public void InsertOperation()
        {
            var itMan = new ItemManager(mockRepository.Object);

            var resultNorm = itMan.AddItem(testItem);
            var resultNull = itMan.AddItem(null);

            testItem = new Item(1, DateTime.Now, "", "King");
            var resultNotValidManufacturer = itMan.AddItem(testItem);

            Assert.True(resultNorm);
            Assert.False(resultNull);
            Assert.False(resultNotValidManufacturer);
        }

        [Fact]
        public void SetCustomerValidationOfInputs()
        {
            var itManager = new ItemManager(mockRepository.Object);

            var setCustomerEmptyName = itManager.SetCustomer("", "cName");
            var setCustomerEmptyCustomer = itManager.SetCustomer("It", "");

            Assert.False(setCustomerEmptyName);
            Assert.False(setCustomerEmptyCustomer);
        }

        [Fact]
        public void SetCustomerOperationFindAndUpdate()
        {
            //var mockItemManager = new Mock<IItemManager>();
            var mockItemManager = new Mock<ItemManager>(mockRepository.Object);
            mockItemManager.Setup(x => x.GetByName("It")).Returns(testItem);

            var setCustomerRightQuery = mockItemManager.Object.SetCustomer("It", "cName");
            var setCustomerNotExistedName = mockItemManager.Object.SetCustomer("Bla", "cName");

            Assert.True(setCustomerRightQuery);
            Assert.False(setCustomerNotExistedName);
        }
    }
}
