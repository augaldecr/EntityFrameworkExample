using EntityFrameworkExample.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkExample.Test
{
    [TestClass]
    public class MovieTheatersControllerTest : TestBase
    {
        [TestMethod]
        public async Task Get_SendedLatitudAndLongitudFromSD_Get2NearbyMovieTheaters()
        {
            var latitud = 18.481139;
            var longitud = -69.938950;

            using (var context = LocalDbInicializer.GetDbContextLocalDb())
            {
                var mapper = ConfigurarAutoMapper();
                var controller = new MovieTheatersController(context, mapper, 
                        actualizadorObservableCollection: null);
                var res = await controller.Get(latitud, longitud);
                var objectResult = res as ObjectResult;
                var movieTheaters = (IEnumerable<object>)objectResult.Value;
                Assert.AreEqual(0, movieTheaters.Count());
            }
        }
    }
}
