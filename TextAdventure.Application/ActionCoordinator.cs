using TextAdventure.Domain;
using TextAdventure.Interface;
using TextAdventure.Infrastructure;

namespace TextAdventure.Application
{
    public class ActionCoordinator : IActionCoordinator
    {
        public CommandOperationStatus Take(string objectName, string characterName, GameLocation location)
        {
            var objectRepository = new ObjectRepository();

            var character = objectRepository.GetCharacter(characterName, location);

            var selectedObject = objectRepository.GetObject(objectName, location);

            //

            // move object to be held by character
            // update location

            return null;
        }
    }
}
