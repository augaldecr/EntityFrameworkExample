using EntityFrameworkExample.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EntityFrameworkExample.Test
{
    [TestClass]
    public class UserServiceTest
    {
        [TestMethod]
        public void GetUserId_EmptyOrNullValue()
        {
            // Prep
            var userService = new UserService();

            // Test
            var res = userService.GetUserId();

            // Verify
            Assert.AreNotEqual("", res);
            Assert.IsNotNull(res);
        }
    }
}