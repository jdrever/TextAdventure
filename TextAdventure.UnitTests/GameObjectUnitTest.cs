using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using TextAdventure.Domain;

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
            bedroom.Description = "an untidy bedroom";
            entireWorld.AddRelationship(RelationshipType.Contains, bedroom);
            var mainCharacter = new GameCharacter("Alife","Drever");
            mainCharacter.Gender = "Male";
            bedroom.AddRelationship(RelationshipType.Contains, mainCharacter);

            var bed = new GameObject("Bed");
            bedroom.AddRelationship(RelationshipType.Contains, bed);

            var wallet = new GameObject("Wallet");
            wallet.IsOpenable = true; 
            wallet.AddRelationship(RelationshipType.IsUnder,bed);

            var money = new GameCurrencyObject("Money",10);
            wallet.AddRelationship(RelationshipType.Contains,money);

            var bedroomDoor=new GameObject("Bedroom Door");
            bedroom.AddRelationship(RelationshipType.Contains, bedroomDoor);
            var landing = new GameLocation("The landing");
            bedroomDoor.AddRelationship(RelationshipType.LeadsTo,landing);

            //Assert.AreEqual(bedroom.Contains().Title, "Bedroom");
            //Assert.AreEqual(bedroomDoor.IsWithin().Title, "Bedroom");

            //mainCharacter.MoveTo(landing);
            //Assert.AreEqual(mainCharacter.IsWithin().Title, "The landing");
            //Assert.AreEqual(bedroomDoor.IsWithin().Title, "Bedroom");

            //test JSON file
            string json = JsonConvert.SerializeObject(bedroom);

            //write string to file
            System.IO.File.WriteAllText(@"C:\Users\Public\Log\json.txt", json);

        }


    }
}
