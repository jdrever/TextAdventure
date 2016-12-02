using System;
using System.Collections.Generic;
using System.Linq;
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
            var selectedCharacter = _objectRepository.Get<GameCharacter>(characterId);

            var location = GetLocation(selectedCharacter);

            var selectedObject = _objectRepository.Get<GameObject>(_objectRepository.GetIdFromName(objectName));

            return _commandExecutor.Drop(selectedObject, selectedCharacter, location, _relationshipRepository);
        }

        // This gets the first gamelocation an object (e.g. a character) has a child-to-parent relationsship with
        // In case the object is not directly within a location (e.g. the character is in a wardrobe in a room)
        private GameLocation GetLocation(GameBaseObject gameObject)
        {
            // check if char has any rels where char is child and loc in parent
            // - if yes, return first
            // - if no, go through each rel char has where char is child and check each 

            // Get relationships where the object is the child
            var gameObjectChildRelationships =
                _relationshipRepository.GetObjectRelationships(gameObject.Id).Where(rel => rel.ChildObjectId == gameObject.Id).ToList();

            // If the object is in a relationship where a location is a parent, return that
            if (gameObjectChildRelationships.Any(rel => _objectRepository.Get<GameLocation>(rel.ParentObjectId) != null))
                return
                    _objectRepository.Get<GameLocation>(
                        gameObjectChildRelationships.First(rel => _objectRepository.Get<GameLocation>(rel.ParentObjectId) != null)
                            .ParentObjectId);
            // Else, go through all the non-location objects the object is a child in a realtionship with and check them for parents
            else
            {
                var gameObjectChildNonLocationParentRelationships =
                    gameObjectChildRelationships.Where(
                        rel => _objectRepository.Get<GameLocation>(rel.ParentObjectId) == null);

                foreach (var relationship in gameObjectChildNonLocationParentRelationships)
                {
                    return GetLocation(_objectRepository.Get<GameBaseObject>(relationship.ParentObjectId));
                }
            }

            return null;
        }
    }
}