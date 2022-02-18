using EntityFrameworkExample.Test.Mocks;
using EntityFrameworkExample.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkExample.Test
{
    [TestClass]
    public class UpdaterObservableCollectionTest
    {
        [TestMethod]
        public void UpdateIfEntitiesIsEmpty()
        {
            // Prep
            var mapper = new Mapper();
            var updaterObservableCollection = new UpdaterObservableCollection(mapper);
            var Entitites = new ObservableCollection<WithId>();
            var dtos = new List<WithId>() { new WithId { Id = 1 }, new WithId { Id = 2 } };

            // Test
            updaterObservableCollection.Update(Entitites, dtos);

            // Verify
            Assert.AreEqual(2, Entitites.Count);
            Assert.AreEqual(1, Entitites[0].Id);
            Assert.AreEqual(2, Entitites[1].Id);
        }

        [TestMethod]
        public void Update_ifDTOsIsEmpty()
        {
            // Prep
            var mapper = new Mapper();
            var updaterObservableCollection = new UpdaterObservableCollection(mapper);
            var Entitites = new ObservableCollection<WithId>() 
                        { new WithId { Id = 1 }, new WithId { Id = 2 } };
            var dtos = new List<WithId>();

            // Test
            updaterObservableCollection.Update(Entitites, dtos);

            // Verify
            Assert.AreEqual(0, Entitites.Count);
        }

        [TestMethod]
        public void Update_IfDTOsyEntititesAreEqual()
        {
            // Prep
            var mapper = new Mapper();
            var updaterObservableCollection = new UpdaterObservableCollection(mapper);
            var Entitites = new ObservableCollection<WithId>()
                        { new WithId { Id = 1 }, new WithId { Id = 2 } };
            var dtos = new List<WithId>() { new WithId { Id = 1 }, new WithId { Id = 2 } };

            // Test
            updaterObservableCollection.Update(Entitites, dtos);

            // Verificar
            Assert.AreEqual(2, Entitites.Count);
            Assert.AreEqual(2, dtos.Count);
        }
    }
}
