using System;
using System.Linq;
using TextAdventure.Domain;
using TextAdventure.Interface;

namespace TextAdventure.Application
{
    public class CommandExecutor : ICommandExecutor
    {
        public CommandOperationStatus Take(GameObject gameObject, GameCharacter gameCharacter, IRelationshipRepository relationshipRepository)
        {
            try
            {
                // If the character already has the object, return false with message.

                if (relationshipRepository.GetObjectRelationships(gameObject.Id).Any(r =>
                    r.ParentObjectId == gameCharacter.Id &&
                    r.ChildObjectId == gameObject.Id &&
                    r.RelationshipType == RelationshipType.IsHeldBy))
                {
                    return new CommandOperationStatus {Message = "Character already has that!", Status = false};
                }

                // Remove the relationship where the object is the child
                // Todo - Catch object having no relationships
                relationshipRepository.Remove(relationshipRepository.GetObjectRelationships(gameObject.Id).First(r => r.ChildObjectId == gameObject.Id));

                // Add heldby relationship between character and object
                relationshipRepository.Add(new GameObjectRelationship(gameCharacter.Id, gameObject.Id, RelationshipType.IsHeldBy));
                
                return new CommandOperationStatus { Message = gameCharacter.Name + " took " + gameObject.Name, Status = true};
            }
            catch (Exception e)
            {
                return new CommandOperationStatus { Message = e.Message, Status = false};
            }
        }

        public CommandOperationStatus Drop(GameObject gameObject, GameCharacter gameCharacter, GameLocation location, IRelationshipRepository relationshipRepository)
        {
            var status = new CommandOperationStatus();

            try
            {
                /*
                RemoveDirectPossessionRelationships(gameObject);

                if (!gameCharacter.HasIndirectRelationshipWith(gameObject, RelationshipType.IsHeldBy, RelationshipDirection.ParentToChild))
                {
                    status.Message = gameCharacter.Name + " doesn't have " + gameObject.Name;
                    status.Status = false;
                    return status;
                }

                location.AddRelationship(RelationshipType.Contains, RelationshipDirection.ParentToChild, gameObject);
                */

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
    }
}