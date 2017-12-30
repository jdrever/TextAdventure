using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextAdventure.Domain;

namespace TextAdventure.Infrastructure.UnitTests
{
    [TestClass]
    public class UnitTestObjectRepository
    {
        [TestMethod]
        public void TestSearchForObjectByName()
        {
            GameWorld world = new GameWorld("Copthorne");
            GameObject ourRoad = new GameObject("Oakfield Road");
            GameObject ourHouse = new GameObject("83 Oakfield Road") { Description = "1930s semi-detached" };
            world.Contains(ourRoad);
            ourRoad.Contains(ourHouse);
            GameObject door = new GameObject("White door")
            {
                IsOpenable = true,
                IsOpen = false
            };
            ourHouse.AddRelationship(RelationshipType.Contains, door);
            GameObject hallway = new GameObject("Hallway");

            var henry = new GameCharacter("Henry");
            hallway.Contains(hallway);

            var objectRepository = new MockedObjectRepository(world);
            CharacterLocationDetails details = new CharacterLocationDetails() { gameCharacterId = henry.ID, gameObjectId = henry.GetCurrentLocation().ID };


            var searchforDoor=objectRepository.GetGameObject<GameBaseObject>("White door", details);
            Assert.AreEqual(searchforDoor.Name, door.Name);
            Assert.AreEqual(searchforDoor, door);

        }
    }
}
