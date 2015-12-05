using System;
using TextAdventure.Domain;
using TextAdventure.Interface;
using TextAdventure.Infrastructure;

namespace TextAdventure.Application
{
    public class ActionCoordinator : IActionCoordinator
    {
        private CommandActioner _commandActioner;
        private ObjectRepository _objectRepository;

        
        public ActionCoordinator(CommandActioner commandActioner, ObjectRepository objectRepository)
        {
           
            if (commandActioner == null) throw new ArgumentNullException("commandActioner");
            if (objectRepository == null) throw new ArgumentNullException("objectRepository");
            _commandActioner = commandActioner;
            _objectRepository = objectRepository;
        }

        public CommandOperationStatus Take(string objectName, string characterName, GameLocation location)
        {
            var selectedCharacter = _objectRepository.GetCharacter(characterName, location);

            var selectedObject = _objectRepository.GetObject(objectName, location);

            return _commandActioner.Take(selectedObject, selectedCharacter);
        }
    }
}
