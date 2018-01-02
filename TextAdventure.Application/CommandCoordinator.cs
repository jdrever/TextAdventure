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
        
        public CommandOperationStatus Take(string objectName, CharacterLocationDetails details)
        {

            var selectedCharacter = (GameCharacter)_objectRepository.GetGameObject(details.gameCharacterId, details);

            var location = selectedCharacter.GetCurrentLocation();

            var selectedObject = (GameObject)_objectRepository.GetGameObject(objectName, details);

            return _commandActioner.Take(selectedCharacter,selectedObject);
        }

        public CommandOperationStatus Drop(string objectName, CharacterLocationDetails details)
        {
            var selectedCharacter = (GameCharacter)_objectRepository.GetGameObject(details.gameCharacterId,details);

            var location = selectedCharacter.GetCurrentLocation();

            var selectedObject = (GameObject)_objectRepository.GetGameObject(objectName, details);

            return _commandActioner.Drop(selectedCharacter,selectedObject);
        }
    }
}