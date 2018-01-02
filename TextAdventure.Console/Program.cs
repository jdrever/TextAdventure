using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Application;
using TextAdventure.Domain;
using TextAdventure.Infrastructure;
using TextAdventure.Interface;

namespace TextAdventure.ConsoleApp
{
    public class Program
    {
        private static IContainer Container { get; set; }
        static void Main(string[] args)
        {
            GameWorld gameWorld = new GameWorld("Copthorne");
            GameObject ourRoad = new GameObject("Oakfield Road");
            GameObject ourHouse = new GameObject("83 Oakfield Road") { Description = "1930s semi-detached" };
            gameWorld.Contains(ourRoad);
            ourRoad.Contains(ourHouse);
            GameObject door = new GameObject("White door")
            {
                IsOpenable = true,
                IsOpen = false
            };
            ourHouse.AddRelationship(RelationshipType.Contains, door);
            GameObject hallway = new GameObject("Hallway");

            var phone = new Phone("Phone", "01743360772") { Description = "Old" };
            hallway.Contains(phone);

            var router = new GameObject("Router");
            hallway.Contains(router);

            door.AddRelationship(RelationshipType.LeadsTo, hallway);
            var stairs = new GameObject("Stairs") { IsClimbable = true, Description = "Sad looking carpet" };
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


            var wardrobe = new GameObject("Wardrobe") { IsOpenable = true, IsOpen = true };
            bedroom.Contains(wardrobe);

            var trousers = new GameObject("Trousers") { Description = "Faded blue jeans", IsWearable = true };

            var leftHandPocket = new GameObject("Left hand pocket");
            var rightHandPocket = new GameObject("Right hand pocket");
            var carKey = new GameObject("Car Key");
            var doorKey = new GameObject("Door Key");

            wardrobe.Contains(trousers);
            trousers.Contains(leftHandPocket);
            trousers.Contains(rightHandPocket);
            rightHandPocket.Contains(doorKey);
            leftHandPocket.Contains(carKey);


            var henry = new GameCharacter("Henry");

            var leftEye = new GameObject("Left Eye");
            var rightEye = new GameObject("Right Eye");

            leftEye.Description = "Blue";
            rightEye.Description = "Blue";

            henry.Contains(leftEye);
            henry.Contains(rightEye);

            var leftHand = new GameObject("Left Hand") { CanHold = true };
            var rightHand = new GameObject("Right Hand") { CanHold = true };


            ourRoad.Contains(henry);


            var builder = new ContainerBuilder();


            builder.RegisterType<Parser>().As<IParser>();
            builder.RegisterType<TextSimplifier>().As<ITextSimplifier>();
            builder.RegisterType<CommandCoordinator>().As<ICommandCoordinator>();
            builder.RegisterType<CommandExecutor>().As<ICommandExecutor>();
            builder.RegisterType<ObjectRepository>().As<IObjectRepository>();

            builder.RegisterInstance(gameWorld).As<GameWorld>();
            // Set the dependency resolver to be Autofac.
            Container = builder.Build();


            using (var scope = Container.BeginLifetimeScope())
            {
                var parser = scope.Resolve<IParser>();
                var details = new CharacterLocationDetails();
                details.gameCharacterId = henry.ID;
                details.gameObjectId = henry.GetCurrentLocation().ID;
                do
                {
                    string input = Console.ReadLine();
                    string message = parser.ParseInput(details, input);
                    Console.WriteLine(message);
                } while (true);
            }

        }

        public static void PlayGame()
        {
            
        }
    }
}
