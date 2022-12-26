using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using TennisMicroserv.Controllers;
using TennisMicroserv.Models;

namespace WebApp.UnitTests
{
    [TestClass]
    public class PlayerControllerTest
    {
        private ILogger<PlayerController> _logger;

        [TestMethod]
        public void TestGetPlayersById()
        {
            var playerController = new PlayerController(_logger);
            var badId = 999;
            var result = playerController.GetPlayersById(badId);
            Assert.AreEqual(null, result);
        }
    }
}