using System;
using System.Collections.Generic;
using System.Linq;
using TextAdventure.Domain;
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
                RemoveLocationRelationships(gameObject);
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

        private void RemoveLocationRelationships(GameBaseObject gameobject)
        {
            var list = gameobject.Relationships.ToList();
            list.RemoveAll
                (gameObjectRelationship =>
                    gameObjectRelationship.RelationshipDirection == RelationshipDirection.ChildToParent
                    && (gameObjectRelationship.RelationshipType == RelationshipType.Contains
                    || gameObjectRelationship.RelationshipType == RelationshipType.IsUnder
                    || gameObjectRelationship.RelationshipType == RelationshipType.LeadsTo));
            gameobject.Relationships = list;
        }
    }
}
