using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextAdventure.Application;
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

            var relationshipRepo = new JsonRelationshipRepository();

            // create dir if it doesn't already exist, for testing with json
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.textadventure\Logs\");

            var entireWorld = new GameContainer
            {
                Name = "Entire World",
                Description = "The entire world."
            };
            objectRepo.Add(entireWorld);

            var room = new GameLocation("A room");
            objectRepo.Add(room);

            relationshipRepo.Add(new GameObjectRelationship(entireWorld.Id, room.Id, RelationshipType.Contains));

            var character = new GameCharacter("Player");
            objectRepo.Add(character);

            relationshipRepo.Add(new GameObjectRelationship(room.Id, character.Id, RelationshipType.Contains));

            var sword = new GameObject("Sword");
            objectRepo.Add(sword);

            relationshipRepo.Add(new GameObjectRelationship(room.Id, sword.Id, RelationshipType.Contains));

            var parser = new Parser(new CommandCoordinator(new CommandExecutor(), objectRepo, relationshipRepo));

            Assert.IsTrue(parser.ParseInput(character.Id, "take Sword").Status);
        }
    }
}
