using EntityFrameworkExample.Controllers;
using EntityFrameworkExample.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkExample.Test
{
    [TestClass]
    public class GenresControllerTest : TestBase
    {
        [TestMethod]
        public async Task Post_2Genres()
        {
            // Prep
            var dbName = Guid.NewGuid().ToString();
            var context1 = CreateContext(dbName);
            var genresController = new GenresController(context1, mapper: null);
            var genres = new Genre[]
            {
                new Genre(){Name = "Genre 1"},
                new Genre(){Name = "Genre 2"}
            };

            // Test
            await genresController.Post(genres);

            // Verify
            var context2 = CreateContext(dbName);
            var genresDB = await context2.Genres.ToListAsync();

            Assert.AreEqual(2, genresDB.Count);

            var genre1Exist = genresDB.Any(g => g.Name == "Genre 1");
            Assert.IsTrue(genre1Exist, message: "Genre 1 not found");

            var genre2Exist = genresDB.Any(g => g.Name == "Genre 2");
            Assert.IsTrue(genre2Exist, message: "Genre 1 not found");
        }

        [TestMethod]
        public async Task Put_ErrorHandler()
        {
            // Prep
            var dbName = Guid.NewGuid().ToString();
            var context1 = CreateContext(dbName);
            var mapper = ConfigurarAutoMapper();
            var testGenre = new Genre() { Name = "Genre 1" };
            context1.Add(testGenre);
            await context1.SaveChangesAsync();

            var context2 = CreateContext(dbName);
            var genresController = new GenresController(context2, mapper);

            // Test y Verify
            await Assert.ThrowsExceptionAsync<DbUpdateConcurrencyException>(() =>
            genresController.Put(new DTOs.GenreUpdateDTO()
            {
                Identifier = testGenre.Identifier,
                Name = "Genre 2",
                Original_Name = "Name incorrect"
            }));
        }

        [TestMethod]
        public async Task Put()
        {
            // Prep
            var dbName = Guid.NewGuid().ToString();
            var context1 = CreateContext(dbName);
            var mapper = ConfigurarAutoMapper();
            var testGenre = new Genre() { Name = "Genre 1" };
            context1.Add(testGenre);
            await context1.SaveChangesAsync();

            var context2 = CreateContext(dbName);
            var genresController = new GenresController(context2, mapper);

            // Test

            await genresController.Put(new DTOs.GenreUpdateDTO()
            {
                Identifier = testGenre.Identifier,
                Name = "Genre 2",
                Original_Name = "Genre 1"
            });

            // Verify

            var context3 = CreateContext(dbName);
            var genreDB = await context3.Genres.SingleAsync();
            Assert.AreEqual(testGenre.Identifier, genreDB.Identifier);
            Assert.AreEqual("Genre 2", genreDB.Name);
        }
    }
}
