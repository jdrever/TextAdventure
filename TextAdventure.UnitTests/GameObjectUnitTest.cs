using System;
using System.Linq;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using TextAdventure.Domain;
using TextAdventure.Infrastructure;
using System.Threading;
using TextAdventure.Application;

namespace TextAdventure.UnitTests
{
    [TestClass]
    public class GameObjectUnitTest
    {
        [TestMethod]
        public void TestCreateWorld()
        {
            // create dir if it doesn't already exist, for testing with json
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.textadventure\Logs\");

            var entireWorld=new GameContainer();

            var bedroom = new GameLocation("Bedroom");
            bedroom.Description = "An untidy bedroom";
            entireWorld.AddRelationship(RelationshipType.Contains, RelationshipDirection.ParentToChild, bedroom);

            var mainCharacter = new GameCharacter("Alfie Drever");
            mainCharacter.Gender = "Male";
            bedroom.AddRelationship(RelationshipType.Contains, RelationshipDirection.ParentToChild, mainCharacter);

            var bed = new GameObject("Bed");
            bedroom.AddRelationship(RelationshipType.Contains, RelationshipDirection.ParentToChild, bed);

            var wallet = new GameObject("Wallet");
            wallet.IsOpenable = true;
            wallet.AddRelationship(RelationshipType.IsUnder, RelationshipDirection.ChildToParent, bed);

            var money = new GameCurrencyObject("Money",10);
            wallet.AddRelationship(RelationshipType.Contains, RelationshipDirection.ParentToChild, money);

            var bedroomDoor=new GameObject("Bedroom Door");
            bedroom.AddRelationship(RelationshipType.Contains, RelationshipDirection.ParentToChild, bedroomDoor);

            var landing = new GameLocation("The landing");
            landing.Description = "Area of carpeted land outside bedroom door";
            bedroomDoor.AddRelationship(RelationshipType.LeadsTo, RelationshipDirection.ParentToChild, landing);

            var objectRepository = new ObjectRepository();

            Assert.AreEqual(objectRepository.GetObjectFromID(bed.ID, bedroom), bed);
        }
    }
}
