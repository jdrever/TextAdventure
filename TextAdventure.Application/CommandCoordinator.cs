using System;
using TextAdventure.Domain;
using TextAdventure.Infrastructure;
using TextAdventure.Interface;

namespace TextAdventure.Application
{
    public class CommandCoordinator : ICommandCoordinator
    {
        private readonly ICommandExecutor _commandExecutor;
        private readonly IObjectRepository _objectRepository;
        
        public CommandCoordinator(ICommandExecutor commandExecutor, IObjectRepository objectRepository)
        {
            if (commandExecutor == null) throw new ArgumentNullException("commandExecutor");
            if (objectRepository == null) throw new ArgumentNullException("objectRepository");
            _commandExecutor = commandExecutor;
            _objectRepository = objectRepository;
        }
        
        public CommandOperationStatus Take(string objectName, Guid characterID)
        {
            var location = new LocationRepository().GetCharactersLocation(characterID);

            var selectedCharacter = _objectRepository.GetObjectFromID<GameCharacter>(characterID, location);

            var selectedObject = _objectRepository.GetObjectFromName<GameObject>(objectName, location);

            return _commandExecutor.Take(selectedObject, selectedCharacter);
        }

        public CommandOperationStatus Drop(string objectName, Guid characterID)
        {
            var location = new LocationRepository().GetCharactersLocation(characterID);

            var selectedCharacter = _objectRepository.GetObjectFromID<GameCharacter>(characterID, location);

            var selectedObject = _objectRepository.GetObjectFromName<GameObject>(objectName, location);

            return _commandExecutor.Drop(selectedObject, selectedCharacter, location);
        }
    }
}