using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextAdventure.Domain;

namespace TextAdventure.Infrastructure.UnitTests
{
    [TestClass]
    public class UnitTestObjectRepository
    {
        private GameWorld world = new GameWorld("Copthorne");
        private CharacterLocationDetails details;
        private ObjectRepository objectRepository;
        private GameObject door;

        [TestInitialize]
        public void Initialise()
        {

            GameObject ourRoad = new GameObject("Oakfield Road");
            GameObject ourHouse = new GameObject("83 Oakfield Road") { Description = "1930s semi-detached" };
            world.Contains(ourRoad);
            ourRoad.Contains(ourHouse);
            door = new GameObject("White door")
            {
                IsOpenable = true,
                IsOpen = false
            };
            ourHouse.AddRelationship(RelationshipType.Contains, door);
            GameObject hallway = new GameObject("Hallway");

            var henry = new GameCharacter("Henry");
            hallway.Contains(henry);

            objectRepository = new ObjectRepository(world);
            details = new CharacterLocationDetails() { gameCharacterId = henry.ID, gameObjectId = henry.GetCurrentLocation().ID };
        }

        [TestMethod]
        public void TestSearchForObjectByName()
        {
            var searchforDoor=objectRepository.GetGameObject("White door", details);
            Assert.AreEqual(searchforDoor.Name, door.Name);
            Assert.AreEqual(searchforDoor, door);

        }
    }
}
