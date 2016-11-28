using System;
using TextAdventure.Domain;
using TextAdventure.Interface;

namespace TextAdventure.Application
{
    public class CommandCoordinator : ICommandCoordinator
    {
        private readonly ICommandExecutor _commandExecutor;
        private readonly IObjectRepository _objectRepository;
        private readonly IRelationshipRepository _relationshipRepository;
        
        public CommandCoordinator(ICommandExecutor commandExecutor, IObjectRepository objectRepository, IRelationshipRepository relationshipRepository)
        {
            if (commandExecutor == null) throw new ArgumentNullException(nameof(commandExecutor));
            if (objectRepository == null) throw new ArgumentNullException(nameof(objectRepository));
            _commandExecutor = commandExecutor;
            _objectRepository = objectRepository;
            _relationshipRepository = relationshipRepository;
        }
                
        public CommandOperationStatus Take(string objectName, Guid characterId)
        {
            // Get object and character
            // Todo - if either doesn't exist, return false with a message.
            var selectedCharacter = _objectRepository.Get<GameCharacter>(characterId);
            var selectedObject = _objectRepository.Get<GameObject>(_objectRepository.GetIdFromName(objectName));

            return selectedObject != null ? _commandExecutor.Take(selectedObject, selectedCharacter, _relationshipRepository) :
                new CommandOperationStatus {Message = "Object does not exist", Status = false};
        }

        public CommandOperationStatus Drop(string objectName, Guid characterId)
        {
            throw new NotImplementedException();

            /*
            var location = new LocationRepository().GetCharactersLocation(characterID);

            var selectedCharacter = _objectRepository.GetObjectFromID<GameCharacter>(characterID, location);

            var selectedObject = _objectRepository.GetObjectFromName<GameObject>(objectName, location);

            return _commandExecutor.Drop(selectedObject, selectedCharacter, location);
            */
        }
        
    }
}