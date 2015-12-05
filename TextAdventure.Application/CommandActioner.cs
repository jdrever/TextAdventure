using System;
using System.Linq;
using TextAdventure.Domain;
using TextAdventure.Infrastructure;
using TextAdventure.Interface;

namespace TextAdventure.Application
{
    public class CommandActioner : ICommandActioner
    {
        public CommandOperationStatus Take(GameObject gameObject, GameCharacter gameCharacter)
        {
            var status = new CommandOperationStatus();

            try
            {
                RemoveDirectPossessionRelationships(gameObject);
                gameObject.AddRelationship(RelationshipType.IsHeldBy, RelationshipDirection.ChildToParent, gameCharacter);

                status.Message = gameCharacter.Name + " took " + gameObject.Name;
                status.Status = true;
            }
            catch (Exception e)
            {
                status.Message = e.Message;
                status.Status = false;
            }

            return status;
        }

        public CommandOperationStatus Drop(GameObject gameObject, GameCharacter gameCharacter)
        {
            var status = new CommandOperationStatus();

            try
            {
                RemoveDirectPossessionRelationships(gameObject);

                var locationRepository = new LocationRepository();
                var gameLocation = locationRepository.GetCharactersLocation(gameCharacter);

                gameLocation.AddRelationship(RelationshipType.Contains, RelationshipDirection.ParentToChild, gameObject);

                status.Message = gameCharacter.Name + " dropped " + gameObject.Name;
                status.Status = true;
            }
            catch (Exception e)
            {
                status.Message = e.Message;
                status.Status = false;
            }

            return status;
        }

        private void RemoveDirectPossessionRelationships(GameBaseObject gameobject)
        {
            var list = gameobject.Relationships.ToList();
            list.RemoveAll
                (gameObjectRelationship =>
                    gameObjectRelationship.RelationshipDirection == RelationshipDirection.ChildToParent
                    && (gameObjectRelationship.RelationshipType == RelationshipType.Contains
                        || gameObjectRelationship.RelationshipType == RelationshipType.IsUnder
                        || gameObjectRelationship.RelationshipType == RelationshipType.LeadsTo
                        || gameObjectRelationship.RelationshipType == RelationshipType.IsHeldBy));
            gameobject.Relationships = list;
        }
    }
}