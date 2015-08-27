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
            IApplicationContext context = ContextRegistry.GetContext();
            GameContext game = (GameContext)context.GetObject("GameContext");
            GameManager manager = new GameManager(game);
            manager.Resolve();
            Assert.IsTrue(true);
        }
    }
}
