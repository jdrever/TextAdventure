using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
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
            var entireWorld=new GameContainer();

            var bedroom=new GameLocation("Bedroom");
            bedroom.Description = "An untidy bedroom";
            entireWorld.AddRelationship(RelationshipType.Contains, bedroom);

            var mainCharacter = new GameCharacter("Alife","Drever");
            mainCharacter.Gender = "Male";
            bedroom.AddRelationship(RelationshipType.Contains, mainCharacter);

            var bed = new GameObject("Bed");
            bedroom.AddRelationship(RelationshipType.Contains, bed);

            var wallet = new GameObject("Wallet");
            wallet.IsOpenable = true; 
            wallet.AddRelationship(RelationshipType.IsUnder, bed);

            var money = new GameCurrencyObject("Money",10);
            wallet.AddRelationship(RelationshipType.Contains,money);

            var bedroomDoor=new GameObject("Bedroom Door");
            bedroom.AddRelationship(RelationshipType.Contains, bedroomDoor);

            var landing = new GameLocation("The landing");
            landing.Description = "Area of carpeted land outside bedroom door";
            bedroomDoor.AddRelationship(RelationshipType.LeadsTo,landing);

            var objectRepository = new ObjectRepository();

            Assert.IsTrue(bedroom.HasRelationshipWith(bedroomDoor, RelationshipType.Contains));

            Assert.AreEqual(objectRepository.GetObject(bed.Name, bedroom), bed);

            // Updates based on code that was once here:
            //HasRelationshipWith sort of replaces IsWithin and the like.
            //TODO: character.Move
        }
    }
}
