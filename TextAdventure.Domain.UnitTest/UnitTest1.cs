using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TextAdventure.Domain.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            GameContainer container = new GameContainer("Copthorne");
            GameObject ourHouse = new GameObject("83 Oakfield Road");
            container.AddRelationship(RelationshipType.Contains,ourHouse);
            GameObject door = new GameObject("White door")
            {
                IsOpenable = true
            };
            ourHouse.AddRelationship(RelationshipType.Contains,door);
            GameObject hallway = new GameObject("Hallway");

            door.AddRelationship(RelationshipType.LeadsTo, hallway);
            var stairs = new GameObject("Stairs") { IsClimbable = true };
            hallway.Contains(stairs);
            var landing = new GameObject("Landing");
            stairs.LeadsTo(landing);

            landing.Contains(stairs);


        }
    }
}
