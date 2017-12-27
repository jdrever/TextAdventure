using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextAdventure.Application;
using System.Diagnostics;

namespace TextAdventure.Domain.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            GameContainer container = new GameContainer("Copthorne");
            GameObject ourRoad = new GameObject("Oakfield Road");
            GameObject ourHouse = new GameObject("83 Oakfield Road") { Description = "1930s semi-detached" };
            container.Contains(ourRoad);
            ourRoad.Contains(ourHouse);
            GameObject door = new GameObject("White door")
            {
                IsOpenable = true, IsOpen = false
            };
            ourHouse.AddRelationship(RelationshipType.Contains,door);
            GameObject hallway = new GameObject("Hallway");

            var phone = new Phone("Phone", "01743360772") { Description="Old"};
            hallway.Contains(phone);

            door.AddRelationship(RelationshipType.LeadsTo, hallway);
            var stairs = new GameObject("Stairs") { IsClimbable = true,Description="Sad looking carpet" };
            hallway.Contains(stairs);
            var landing = new GameObject("Landing");
            stairs.LeadsTo(landing);
            //but the stairs also lead down to the hallway? 
            //landing.Contains(stairs);

            var bedroomDoor = new GameObject("Bedroom door");
            var bedroom = new GameObject("Bedroom");
            bedroomDoor.LeadsTo(bedroom);
            landing.Contains(bedroomDoor);

            var wardrobe = new GameObject("Wardrobe") { IsOpenable = true,IsOpen=true };
            bedroom.Contains(wardrobe);

            var trousers = new GameObject("Trousers") { Description = "Faded blue jeans" };
            wardrobe.Contains(trousers);

            var henry = new GameCharacter("Henry");
            ourRoad.Contains(henry);

            Debug.Write(henry.GetCurrentLocation());

            CommandExecutor commandExecutor = new CommandExecutor();
            var status = commandExecutor.GoThrough(door, henry);
            Debug.Write(status.Message);

            status =commandExecutor.Open(door, henry);
            Debug.Write(status.Message);

            status=commandExecutor.GoThrough(door, henry);
            Debug.Write(status.Message);

            Debug.Write(henry.GetCurrentLocation());

            status = commandExecutor.Take(phone, henry);
            Debug.Write(status.Message);







        }
    }
}
