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
            stairs.GoesUpTo(landing); 
            landing.Contains(stairs);
            stairs.GoesDownTo(hallway);


            var bedroomDoor = new GameObject("Bedroom door") { IsOpenable = true, IsOpen = false };
            var bedroom = new GameObject("Bedroom");
            bedroomDoor.LeadsTo(bedroom);
            bedroomDoor.LeadsTo(landing);
            landing.Contains(bedroomDoor);


            var wardrobe = new GameObject("Wardrobe") { IsOpenable = true,IsOpen=true };
            bedroom.Contains(wardrobe);

            var trousers = new GameObject("Trousers") { Description = "Faded blue jeans" };
            wardrobe.Contains(trousers);

            var henry = new GameCharacter("Henry");
            ourRoad.Contains(henry);

            Debug.Write(henry.GetCurrentLocation());

            CommandExecutor commandExecutor = new CommandExecutor();
            var status = commandExecutor.GoThrough(henry, door);
            Debug.Write(status.Message);

            status=commandExecutor.Open(henry,door);
            Debug.Write(status.Message);

            status=commandExecutor.GoThrough(henry,door);
            Debug.Write(status.Message);
            //TODO: yes, but the door goes back to outside the house
            Debug.Write(henry.GetCurrentLocation());

            status = commandExecutor.Take(henry, phone);
            Debug.Write(status.Message);

            status = commandExecutor.GoUp(henry, stairs);
            Debug.Write(status.Message);

            Debug.Write(henry.GetCurrentLocation());

            status = commandExecutor.Open(henry,bedroomDoor);
            Debug.Write(status.Message);

            Debug.Write(henry.GetCurrentLocation());

            status = commandExecutor.GoThrough(henry, bedroomDoor);



            status = commandExecutor.Examine(henry,wardrobe);
            Debug.Write(status.Message);

        }
    }
}
