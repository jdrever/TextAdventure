using System;
using TextAdventure.Domain;
using TextAdventure.Infrastructure;
using TextAdventure.Interface;

namespace TextAdventure.Application
{
    public class CommandCoordinator : ICommandCoordinator
    {
        private readonly ICommandExecutor _commandActioner;
        private readonly IObjectRepository _objectRepository;


        
        public CommandCoordinator(ICommandExecutor commandActioner, IObjectRepository objectRepository)
        {
            if (commandActioner == null) throw new ArgumentNullException("commandActioner");
            if (objectRepository == null) throw new ArgumentNullException("objectRepository");
            _commandActioner = commandActioner;
            _objectRepository = objectRepository;
        }
        
        public CommandOperationStatus Take(string objectName, Guid characterID)
        {
            var location = new LocationRepository().GetCharactersLocation(characterID);

            var selectedCharacter = _objectRepository.GetObjectFromID<GameCharacter>(characterID, location);

            var selectedObject = _objectRepository.GetObjectFromName<GameObject>(objectName, location);

            return _commandActioner.Take(selectedObject, selectedCharacter);
        }

        public CommandOperationStatus Drop(string objectName, Guid characterID)
        {
            var location = new LocationRepository().GetCharactersLocation(characterID);

            var selectedCharacter = _objectRepository.GetObjectFromID<GameCharacter>(characterID, location);

            var selectedObject = _objectRepository.GetObjectFromName<GameObject>(objectName, location);

            return _commandActioner.Drop(selectedObject, selectedCharacter, location);
        }
    }
}