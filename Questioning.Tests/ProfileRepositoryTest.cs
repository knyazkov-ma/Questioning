using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Questioning.Repository;
using Moq;
using Questioning.Interface;
using Questioning.Entity;
using System.IO;

namespace Questioning.Tests
{
    [TestClass]
    public class ProfileRepositoryTest
    {
        ProfileRepository repository;
        public ProfileRepositoryTest()
        {

            Mock<IPersistenceSettings> persistenceSettings = new Mock<IPersistenceSettings>(MockBehavior.Strict);
            persistenceSettings.Setup(x => x.GetDataPath())
               .Returns(() => { return "Data"; });
            persistenceSettings.Setup(x => x.GetDataFileExtension())
               .Returns(() => { return ".txt"; });

            repository = new ProfileRepository(persistenceSettings.Object);
        }

        [TestMethod]
        public void SaveAndGetCorrectProfile()
        {
            Profile entity = TestDataFactory.GetCorrectProfileList().FirstOrDefault();

            repository.Save(entity);
            Profile savedEntity = repository.Get(entity.Id);

            Assert.IsTrue(entity.Id == savedEntity.Id);
            Assert.IsTrue(entity.Items.Skip(0).FirstOrDefault().Name == savedEntity.Items.Skip(0).FirstOrDefault().Name);
            Assert.IsTrue(entity.Items.Skip(0).FirstOrDefault().Value == savedEntity.Items.Skip(0).FirstOrDefault().Value);

            Assert.IsTrue(entity.Items.Skip(1).FirstOrDefault().Name == savedEntity.Items.Skip(1).FirstOrDefault().Name);
            Assert.IsTrue(entity.Items.Skip(1).FirstOrDefault().Value == savedEntity.Items.Skip(1).FirstOrDefault().Value);

            Assert.IsTrue(entity.Items.Skip(2).FirstOrDefault().Name == savedEntity.Items.Skip(2).FirstOrDefault().Name);
            Assert.IsTrue(entity.Items.Skip(2).FirstOrDefault().Value == savedEntity.Items.Skip(2).FirstOrDefault().Value);
        }


        [TestMethod]
        public void GetCorrectProfileList()
        {
            Profile entity = TestDataFactory.GetCorrectProfileList().FirstOrDefault();

            repository.Save(entity);
            IEnumerable<Profile> entityes = repository.GetList();

            Assert.IsTrue(entityes != null && entityes.Count() == 1);
            Assert.IsTrue(entity.Id == entityes.FirstOrDefault().Id);
        }

        [TestMethod]
        public void DeleteCorrectProfile()
        {
            Profile entity = TestDataFactory.GetCorrectProfileList().FirstOrDefault();

            repository.Save(entity);

            Assert.IsTrue(!File.Exists(entity.Id + ".txt"));
            repository.Delete(entity.Id);
            Assert.IsTrue(!File.Exists(entity.Id + ".txt"));

        }

        [TestMethod]
        public void DeleteNotExistsProfile()
        {
            repository.Delete("NotExists.txt");
        }
    }
}
