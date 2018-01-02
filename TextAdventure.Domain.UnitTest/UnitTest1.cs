using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextAdventure.Application;
using System.Diagnostics;

namespace TextAdventure.Domain.UnitTest
{
    [TestClass]
    public class CommmandExecutorUnitTest
    {
        private GameWorld container = new GameWorld("Copthorne");
        private GameObject ourRoad = new GameObject("Oakfield Road");
        private GameObject ourHouse = new GameObject("83 Oakfield Road") { Description = "1930s semi-detached" };
        private GameCharacter henry = new GameCharacter("Henry");
        private GameObject door = new GameObject("White door")
        {
            IsOpenable = true,
            IsOpen = false
        };
        private GameObject hallway = new GameObject("Hallway");
        private Phone phone = new Phone("Phone", "01743360772") { Description = "Old" };
        private GameObject router = new GameObject("Router");
        private GameObject stairs = new GameObject("Stairs") { IsClimbable = true, Description = "Sad looking carpet" };
        private GameObject landing = new GameObject("Landing");
        private GameObject bedroomDoor = new GameObject("Bedroom door") { IsOpenable = true, IsOpen = false };
        private GameObject bedroom = new GameObject("Bedroom");
        private GameObject wardrobe = new GameObject("Wardrobe") { IsOpenable = true, IsOpen = true };
        private GameObject trousers = new GameObject("Trousers") { Description = "Faded blue jeans", IsWearable = true };
        private GameObject leftHandPocket = new GameObject("Left hand pocket");
        private GameObject rightHandPocket = new GameObject("Right hand pocket");
        private GameObject carKey = new GameObject("Car Key");
        private GameObject doorKey = new GameObject("Door Key");
        private GameObject leftEye = new GameObject("Left Eye");
        private GameObject rightEye = new GameObject("Right Eye");
        private GameObject leftHand = new GameObject("Left Hand") { CanHold = true };
        private GameObject rightHand = new GameObject("Right Hand") { CanHold = true };
        private CommandExecutor commandExecutor = new CommandExecutor();
        [TestInitialize]
        public void Initialise()
        {
            container.Contains(ourRoad);
            ourRoad.Contains(ourHouse);
            ourHouse.AddRelationship(RelationshipType.Contains, door);

            hallway.Contains(phone);
            hallway.Contains(router);
            door.AddRelationship(RelationshipType.LeadsTo, hallway);
            hallway.Contains(stairs);

            stairs.GoesUpTo(landing);
            landing.Contains(stairs);
            stairs.GoesDownTo(hallway);

            bedroomDoor.LeadsTo(bedroom);
            bedroomDoor.LeadsTo(landing);
            landing.Contains(bedroomDoor);

            bedroom.Contains(wardrobe);
            wardrobe.Contains(trousers);
            trousers.Contains(leftHandPocket);
            trousers.Contains(rightHandPocket);
            rightHandPocket.Contains(doorKey);

            leftHandPocket.Contains(carKey);

            leftEye.Description = "Blue";
            rightEye.Description = "Blue";
            henry.Contains(leftEye);
            henry.Contains(rightEye);
            henry.SetDefaultHandlingObject(rightHand);
            
        }
        [TestMethod]
        public void TestGoThroughCommand()
        {
            ourRoad.Contains(henry);
            Debug.Write(henry.GetCurrentLocation());

            
            var status = commandExecutor.GoThrough(henry, door);
            Debug.Write(status.Message);
            //should not be able to go through door without opening it
            Assert.IsFalse(status.Status);

            status = commandExecutor.Open(henry, door);
            Debug.Write(status.Message);
            Assert.IsTrue(status.Status);
            Assert.IsTrue(door.IsOpen);

            status = commandExecutor.GoThrough(henry, door);
            Debug.Write(status.Message);
            //TODO: yes, but the door goes back to outside the house
            Debug.Write(henry.GetCurrentLocation());
            Assert.IsTrue(status.Status);
            //TODO: this test should be better expressed)
            Assert.IsTrue(hallway.Contains().Contains(henry));
        }
        [TestMethod]
        public void TestTakeCommand()
        {
            //TODO: split these tests up
            //need to test you can't take an object that isn't within the 
            //current location
            hallway.Contains(henry);
            var status = commandExecutor.Take(henry, phone);
            Debug.Write(status.Message);
            Assert.IsTrue(phone.IsHeld());
            status = commandExecutor.Take(henry, router);
            Debug.Write(status.Message);
            //shouldn't be able to take the router, as already holding the phone
            //with right hand.  Note: really should be taken by left hand
            Assert.IsFalse(router.IsHeld());
        }
        [TestMethod]
        public void TestGoUpCommand()
        {
            hallway.Contains(henry);
            var status = commandExecutor.GoUp(henry, stairs);
            Debug.Write(status.Message);

            Debug.Write(henry.GetCurrentLocation());
        }
        [TestMethod]
        public void TestExamineCommand()
        {
            bedroom.Contains(henry);
            var status = commandExecutor.Examine(henry, wardrobe);
            Debug.Write(status.Message);
            Assert.IsTrue(status.Status);
        }
        public void TextWearCommand()
        {
            bedroom.Contains(henry);
            var status = commandExecutor.Wear(henry, trousers);
            Debug.Write(status.Message);
            Debug.Write(henry);
            Assert.IsTrue(status.Status);
            Assert.IsTrue(henry.Wears()==trousers);
        }
         
    }
}
