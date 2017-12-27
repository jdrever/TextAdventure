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
            GameObject ourHouse = new GameObject("83 Oakfield Road") { Description = "1930s semi-detached" };
            container.AddRelationship(RelationshipType.Contains,ourHouse);
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

            string text = ourHouse.ToString();

            

        }
    }
}
