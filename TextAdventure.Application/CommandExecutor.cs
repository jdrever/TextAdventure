using System;
using System.Linq;
using TextAdventure.Domain;
using TextAdventure.Interface;

namespace TextAdventure.Application
{
    public class CommandExecutor : ICommandExecutor
    {
        public CommandOperationStatus Take(GameObject gameObject, GameCharacter gameCharacter)
        {
            var status = new CommandOperationStatus();

            try
            {

                if (gameCharacter.HasIndirectRelationshipWith(gameObject, RelationshipType.IsHeldBy, RelationshipDirection.ParentToChild))
                {
                    status.Message = gameCharacter.Name + " already has " + gameObject.Name;
                    status.Status = false;
                    return status;
                }

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

        public CommandOperationStatus Drop(GameObject gameObject, GameCharacter gameCharacter, GameLocation location)
        {
            var status = new CommandOperationStatus();

            try
            {
                RemoveDirectPossessionRelationships(gameObject);

                if (!gameCharacter.HasIndirectRelationshipWith(gameObject, RelationshipType.IsHeldBy, RelationshipDirection.ParentToChild))
                {
                    status.Message = gameCharacter.Name + " doesn't have " + gameObject.Name;
                    status.Status = false;
                    return status;
                }

                location.AddRelationship(RelationshipType.Contains, RelationshipDirection.ParentToChild, gameObject);

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
            var directPossessionRelationships = gameobject.Relationships.Where(relationship => relationship.RelationshipDirection == RelationshipDirection.ChildToParent && (relationship.RelationshipType == RelationshipType.Contains || relationship.RelationshipType == RelationshipType.IsUnder || relationship.RelationshipType == RelationshipType.LeadsTo || relationship.RelationshipType == RelationshipType.IsHeldBy)).ToList();

            foreach (var relationship in directPossessionRelationships)
            {
                gameobject.RemoveRelationship(relationship);
            }
        }
    }
}