using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextAdventure.Domain;
using TextAdventure.Infrastructure;

namespace TextAdventure.UnitTests
{
    [TestClass]
    public class GameObjectUnitTest
    {
        [TestMethod]
        public void TestCreateWorld()
        {
            var objectRepo = new JsonObjectRepository();

            // create dir if it doesn't already exist, for testing with json
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.textadventure\Logs\");

            var entireWorld = new GameContainer
            {
                Name = "Entire World",
                Description = "The entire world."
            };
            objectRepo.Add(entireWorld);

            var loadedWorld = objectRepo.GetObject<GameContainer>(entireWorld.Id);

            Assert.AreEqual(entireWorld, loadedWorld);
        }
    }
}
