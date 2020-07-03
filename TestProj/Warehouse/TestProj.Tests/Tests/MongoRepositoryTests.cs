using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Warehouse;
using Xunit;

namespace TestProj.Tests.Tests
{
    public class MongoRepositoryTests
    {
        [Fact]
        public void MockFindByIdRight()
        {
            var item = new Item(6, DateTime.Now, "Book", "");
            var mockRepository = new Mock<IRepository<Item>>();

            mockRepository.Setup(x => x.GetById(6)).Returns(item);
            var resultFound = mockRepository.Object.GetById(6).Name;
            var resultNotFound = mockRepository.Object.GetById(3);

            Assert.Equal(item.Name, resultFound);
            Assert.Null(resultNotFound);
        }

        [Fact]
        public void MockFindByIdWrong()
        {
            var item = new Item(6, DateTime.Now, "Book", "");
            var mockRepository = new Mock<IRepository<Item>>();

            mockRepository.Setup(x => x.GetById(item)).Returns(() => null);
            var result = mockRepository.Object.GetById(item);

            Assert.Null(result);
        }

        [Fact]
        public void MockDelete_RightID()
        {
            var mockRepository = new Mock<IRepository<Item>>();

            mockRepository.Setup(x => x.Delete(6)).Returns(true);
            var resultExisted = mockRepository.Object.Delete(6);
            var resultNotExisted = mockRepository.Object.Delete(3);

            Assert.True(resultExisted);
            Assert.False(resultNotExisted);
        }

        [Fact]
        public void MockDelete_WrongID()
        {
            var mockRepository = new Mock<IRepository<Item>>();
            var obj = new Item(1, DateTime.Now, "", "");

            mockRepository.Setup(x => x.Delete(obj)).Returns(false);
            var resultWrongID = mockRepository.Object.Delete(obj);

            Assert.False(resultWrongID);
        }
    }
}
