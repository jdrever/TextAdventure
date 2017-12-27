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

                status.Message = gameCharacter.Name + " takes the " + gameObject.Name;
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

        public CommandOperationStatus Open(GameObject gameObject, GameCharacter gameCharacter)
        {
            var status = new CommandOperationStatus();
            if (!gameObject.IsOpenable)
            {
                status.Status = false;
                status.Message = "The " + gameObject.Name + " can't be opened";
                return status;
            }
            if (gameObject.IsOpen)
            {
                status.Status = false;
                status.Message = "The " + gameObject.Name + " is already open";
                return status;
            }
            gameObject.IsOpen = true;
            status.Status = true;
            status.Message = gameCharacter.Name + " has opened the " + gameObject.Name;
            return status;
        }

        public CommandOperationStatus GoThrough(GameObject gameObject, GameCharacter gameCharacter)
        {
            var status = new CommandOperationStatus();
            if (gameObject.IsOpenable && !gameObject.IsOpen)
            {
                status.Status = false;
                status.Message = "The " + gameObject.Name + " isn't opened";
                return status;
            }
            var leadsTo = gameObject.LeadsTo();
            if (leadsTo == null)
            {
                status.Status = false;
                status.Message = "The " + gameObject.Name + " doesn't go anywhere";
                return status;
            }
            gameCharacter.Move(gameCharacter.GetCurrentLocation(), RelationshipType.Contains, leadsTo);
            status.Status = true;
            status.Message = gameCharacter.Name + "  goes through the " + gameObject.Name;
            return status;
        }
    }
}