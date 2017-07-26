using System;
using System.IO;
using TextAdventure.Application;
using TextAdventure.Domain;
using TextAdventure.Infrastructure;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var objectRepo = new JsonObjectRepository();

            var relationshipRepo = new JsonRelationshipRepository();

            // create dir if it doesn't already exist, for testing with json
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.textadventure\Logs\");

            var entireWorld = new GameContainer
            {
                Name = "Entire World",
                Description = "The entire world."
            };
            objectRepo.Add(entireWorld);

            var room = new GameLocation("A room")
            { Description = "A small room with walls made from stone bricks. There are no obvious windows but one wall holds a locked wooden door." };
            objectRepo.Add(room);

            relationshipRepo.Add(new GameObjectRelationship(entireWorld.Id, room.Id, RelationshipType.Contains));

            var character = new GameCharacter("Player");
            objectRepo.Add(character);

            relationshipRepo.Add(new GameObjectRelationship(room.Id, character.Id, RelationshipType.Contains));

            var sword = new GameObject("Sword") {Description = "A blunt, battered-looking longsword."};
            objectRepo.Add(sword);

            relationshipRepo.Add(new GameObjectRelationship(character.Id, sword.Id, RelationshipType.IsHeldBy));

            var shield = new GameObject("Shield") {Description = "A dented wooden circular shield."};
            objectRepo.Add(shield);

            relationshipRepo.Add(new GameObjectRelationship(room.Id, shield.Id, RelationshipType.Contains));

            var parser = new Parser(new CommandCoordinator(new CommandExecutor(), objectRepo, relationshipRepo));

            while (true)
            {
                var result = parser.ParseInput(character.Id, System.Console.ReadLine()).Message;
                System.Console.WriteLine(result);
            }
        }
    }
}
