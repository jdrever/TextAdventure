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
                    return new CommandOperationStatus {Message = $"{gameCharacter.Name} already has that!", Status = false};
                }

                // Remove the relationship where the object is the child - there is no other way for the object to be within something
                // Todo - Catch object having no relationships?
                relationshipRepository.Remove(relationshipRepository.GetObjectRelationships(gameObject.Id).First(r => r.ChildObjectId == gameObject.Id));

                // Add heldby relationship between character and object
                relationshipRepository.Add(new GameObjectRelationship(gameCharacter.Id, gameObject.Id, RelationshipType.IsHeldBy));
                
                return new CommandOperationStatus { Message = $"{gameCharacter.Name} took {gameObject.Name}", Status = true};
            }
            catch (Exception e)
            {
                return new CommandOperationStatus { Message = e.Message, Status = false};
            }
        }

        public CommandOperationStatus Drop(GameObject gameObject, GameCharacter gameCharacter, GameLocation location, IRelationshipRepository relationshipRepository)
        {
            try
            {
                // Remove object from character

                // Check that the character has this object
                // Can only drop objects the character is directly holding
                if (relationshipRepository.GetObjectRelationships(gameCharacter.Id).Any(rel =>
                    rel.ParentObjectId == gameCharacter.Id &&
                    rel.RelationshipType == RelationshipType.IsHeldBy &&
                    rel.ChildObjectId == gameObject.Id))
                {
                    relationshipRepository.Remove(
                        relationshipRepository.GetObjectRelationships(gameCharacter.Id).First(rel =>
                            rel.ParentObjectId == gameCharacter.Id &&
                            rel.RelationshipType == RelationshipType.IsHeldBy &&
                            rel.ChildObjectId == gameObject.Id));
                }
                else
                {
                    return new CommandOperationStatus {Status = false, Message = $"{gameCharacter.Name} is not holding that object" };
                }

                // Insert into location
                relationshipRepository.Add(new GameObjectRelationship(location.Id, gameObject.Id,
                    RelationshipType.Contains));

                return new CommandOperationStatus
                {
                    Message = $"{gameCharacter.Name} dropped {gameObject.Name}",
                    Status = true
                };
            }
            catch (Exception e)
            {
                return new CommandOperationStatus { Message = e.Message, Status = false };
            }
        }

        public CommandOperationStatus Describe(GameBaseObject gameObject, GameCharacter gameCharacter, IRelationshipRepository relationshipRepository, IObjectRepository objectRepository)
        {
            // print description of gameobject; goto next object
            var message = gameObject.Description != null ? gameObject.Description : "";

            if (gameObject is GameLocation)
            {
                message = $"You are in {message}";

                if (relationshipRepository.GetObjectRelationships(gameObject.Id).Any(rel =>
                    rel.ParentObjectId == gameObject.Id &&
                    rel.RelationshipType == RelationshipType.Contains &&
                    rel.ChildObjectId != gameCharacter.Id))
                {
                    message += "\nYou can see ";

                    foreach (var relationship in relationshipRepository.GetObjectRelationships(gameObject.Id).Where(rel =>
                        rel.ParentObjectId == gameObject.Id &&
                        rel.RelationshipType == RelationshipType.Contains &&
                        rel.ChildObjectId != gameCharacter.Id))
                    {
                        var relationshipChild = objectRepository.Get<GameBaseObject>(relationship.ChildObjectId);
                        message += (relationshipChild.Description != null ? relationshipChild.Description : relationshipChild.Name) + " ";
                    }
                }
            }

            return new CommandOperationStatus() { Message = message != "" ? message : "There is nothing worthwhile here", Status = true };
        }
    }
}