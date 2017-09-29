using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Questioning.Interface;
using Questioning.Entity;
using Questioning.Services;
using Questioning.Repository.Interface;
using Questioning.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Questioning.Tests
{
    [TestClass]
    public class QuestionServiceTest
    {
        QuestionService service;
                
        [TestMethod]
        public void GetStatistics_Null()
        {
            Mock<IRepository<Profile>> profileRepository = new Mock<IRepository<Profile>>(MockBehavior.Strict);
            profileRepository.Setup(x => x.GetList())
               .Returns(() => { return null; });


            service = new QuestionService(profileRepository.Object, Mock.Of<IPersistenceSettings>());
            StatisticsDTO s = service.GetStatistics();

            Assert.IsTrue(s == null);
        }

        [TestMethod]
        public void GetStatistics_NotNull()
        {
            Mock<IRepository<Profile>> profileRepository = new Mock<IRepository<Profile>>(MockBehavior.Strict);
            profileRepository.Setup(x => x.GetList())
               .Returns(() => { return TestDataFactory.GetCorrectProfileList(); });


            service = new QuestionService(profileRepository.Object, Mock.Of<IPersistenceSettings>());
            StatisticsDTO s = service.GetStatistics();

            Assert.IsTrue(s.AverageAge == 32);
            Assert.IsTrue(s.TopProgramLanguage == "PHP");
        }

        [TestMethod]
        public void GetAllProfileNames()
        {
            Mock<IRepository<Profile>> profileRepository = new Mock<IRepository<Profile>>(MockBehavior.Strict);
            profileRepository.Setup(x => x.GetList())
               .Returns(() => { return TestDataFactory.GetCorrectProfileList(); });


            service = new QuestionService(profileRepository.Object, Mock.Of<IPersistenceSettings>());
            IEnumerable<string> profiles = service.GetAllProfileNames();

            Assert.IsTrue(profiles.Count() == TestDataFactory.GetCorrectProfileList().Count());
        }

        [TestMethod]
        public void GetAllTodayProfileNames()
        {
            Mock<IRepository<Profile>> profileRepository = new Mock<IRepository<Profile>>(MockBehavior.Strict);
            profileRepository.Setup(x => x.GetList())
               .Returns(() => { return TestDataFactory.GetCorrectProfileList(); });


            service = new QuestionService(profileRepository.Object, Mock.Of<IPersistenceSettings>());
            IEnumerable<string> profiles = service.GetAllTodayProfileNames();

            Assert.IsTrue(profiles.Count() == 2);
        }
        
    }
}
