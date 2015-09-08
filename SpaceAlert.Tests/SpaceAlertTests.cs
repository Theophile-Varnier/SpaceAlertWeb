using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spring.Context;
using Spring.Context.Support;
using SpaceAlert.Model.Jeu;
using SpaceAlert.Business;
using SpaceAlert.Model.Helpers.Enums;
using System.Collections.Generic;

namespace SpaceAlert.Tests
{
    [TestClass]
    public class SpaceAlertTests
    {
        [TestMethod]
        public void TestResolution()
        {
            SpaceAlertData.Init();
            IApplicationContext context = ContextRegistry.GetContext();
            //GameContext game = (GameContext)context.GetObject("GameContext");
            GameService service = new GameService();
            Guid gameId = service.InitialiserGame(TypeMission.SIMPLE, 1, true, false, false, new KeyValuePair<long, string>(42, "Dieu"));
            GameContext game = SpaceAlertData.Game(gameId);
            service.DemarrerGame(game);

            GameManager manager = new GameManager(game);
            manager.Resolve();
            Assert.IsTrue(true);
        }
    }
}
