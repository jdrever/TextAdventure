using System;
using System.Linq;
using TextAdventure.Domain;
using TextAdventure.Interface;

namespace TextAdventure.Application
{
    public class CommandExecutor : ICommandExecutor
    {
        public CommandOperationStatus Take(GameCharacter gameCharacter, GameObject gameObject)
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

                //RemoveDirectPossessionRelationships(gameObject);
                var objectsHeldbyDefaultHandlingObject =gameCharacter.GetDefaultHandlingObject().Holds();
                if (objectsHeldbyDefaultHandlingObject != null)
                {
                    // need to check for other contained objects that can hold first
                    // before returing an error
                    status.Message = gameCharacter.Name + " is already carrying the " + objectsHeldbyDefaultHandlingObject.First().Name;
                    if (gameCharacter.HasDefaultHandlingObject())
                    {
                        status.Message += " with their " + gameCharacter.GetDefaultHandlingObject().Name;
                    } 
                    status.Status = false;
                    return status;
                }
                //
                gameObject.AddRelationship(RelationshipType.IsHeldBy, RelationshipDirection.ChildToParent, gameCharacter.GetDefaultHandlingObject());

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

        public CommandOperationStatus Drop(GameCharacter gameCharacter, GameObject gameObject)
        {
            var status = new CommandOperationStatus();

            try
            {
                //RemoveDirectPossessionRelationships(gameObject);

                if (!gameCharacter.HasIndirectRelationshipWith(gameObject, RelationshipType.IsHeldBy, RelationshipDirection.ParentToChild))
                {
                    status.Message = gameCharacter.Name + " doesn't have " + gameObject.Name;
                    status.Status = false;
                    return status;
                }

                gameCharacter.GetCurrentLocation().AddRelationship(RelationshipType.Contains, RelationshipDirection.ParentToChild, gameObject);

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

        /**private void _RemoveDirectPossessionRelationships(GameBaseObject gameobject)
        {
            var directPossessionRelationships = gameobject.Relationships.Where(relationship => relationship.RelationshipDirection == RelationshipDirection.ChildToParent && (relationship.RelationshipType == RelationshipType.Contains || relationship.RelationshipType == RelationshipType.IsUnder || relationship.RelationshipType == RelationshipType.LeadsTo || relationship.RelationshipType == RelationshipType.IsHeldBy)).ToList();

            foreach (var relationship in directPossessionRelationships)
            {
                gameobject.RemoveRelationship(relationship);
            }
        }
        **/

        public CommandOperationStatus Open(GameCharacter gameCharacter,GameObject gameObject)
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

        public CommandOperationStatus GoThrough(GameCharacter gameCharacter,GameObject gameObject)
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

        public CommandOperationStatus GoUp(GameCharacter gameCharacter, GameObject gameObject)
        {
            var status = new CommandOperationStatus();
            var goesUpTo = gameObject.GoesUpTo();
            if (goesUpTo == null)
            {
                status.Status = false;
                status.Message = "The " + gameObject.Name + " doesn't go anywhere";
                return status;
            }
            gameCharacter.Move(gameCharacter.GetCurrentLocation(), RelationshipType.Contains, goesUpTo);
            status.Status = true;
            status.Message = gameCharacter.Name + "  goes up the " + gameObject.Name;
            return status;
        }

        public CommandOperationStatus GoDown(GameCharacter gameCharacter, GameObject gameObject)
        {
            var status = new CommandOperationStatus();
            var goesDownTo = gameObject.GoesDownTo();
            if (goesDownTo == null)
            {
                status.Status = false;
                status.Message = "The " + gameObject.Name + " doesn't go anywhere";
                return status;
            }
            gameCharacter.Move(gameCharacter.GetCurrentLocation(), RelationshipType.Contains, goesDownTo);
            status.Status = true;
            status.Message = gameCharacter.Name + "  goes up the " + gameObject.Name;
            return status;
        }

        public CommandOperationStatus Examine(GameCharacter gameCharacter, GameObject gameObject)
        {
            var status = new CommandOperationStatus();
            status.Message = gameCharacter.Name + "  examines the " + gameObject.Name + ". " + gameObject.ToString();
            return status;
        }
        public CommandOperationStatus Wear(GameCharacter gameCharacter, GameObject gameObject)
        {
            var status = new CommandOperationStatus();

            if (!gameObject.IsWearable)
            {
                status.Message = "You can't wear the " + gameObject.Name + ".";
                status.Status = false;
                return status;
            }

            gameObject.RemoveRelationship(RelationshipType.Contains, RelationshipDirection.ChildToParent, gameObject.GetContainingParent());
            gameCharacter.Wears(gameObject);
            status.Message = gameCharacter.Name + "  wears the " + gameObject.Name + ". " + gameObject.ToString();

            return status;
        }
    }
}