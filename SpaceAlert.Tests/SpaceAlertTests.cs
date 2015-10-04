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
            //SpaceAlertData.Init();
            //IApplicationContext context = ContextRegistry.GetContext();
            //GameService service = new GameService();
            //int gameId = service.InitialiserGame(TypeMission.SIMPLE, 1, true, false, false, new KeyValuePair<long, string>(42, "Dieu"));
            //GameContext game = SpaceAlertData.Game(gameId);
            //service.DemarrerGame(game);

            //GameResolutionManager manager = new GameResolutionManager(game);
            //manager.Resolve();
            //Assert.IsTrue(true);
        }
    }
}
