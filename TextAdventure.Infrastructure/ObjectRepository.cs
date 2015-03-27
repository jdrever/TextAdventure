using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Domain;
using TextAdventure.Interface;

namespace TextAdventure.Infrastructure
{
    public class ObjectRepository : IObjectRepository
    {
        public GameObject GetObject(string objectName, GameLocation location)
        {
            var gameObject = (GameObject)location.Relationships.First
                (GameObjectRelationship => GameObjectRelationship.RelationshipTo.Name == objectName 
                    && GameObjectRelationship.RelationshipType == RelationshipType.Contains)
                    .RelationshipTo;
            //if (gameObject != null)
                return gameObject;
            //else:
            //  find any object within location (specifically those that are within other objects)
            //  whose name is equal to objectName
        }

        // Grr... dunno
        public GameBaseObject foo(GameLocation location)
        {
            List<GameBaseObject> objects = new List<GameBaseObject>();

            foreach (GameObjectRelationship relationship in location.Relationships)
                if (relationship.RelationshipType == RelationshipType.Contains)

            
            return foo();
        }

        // TODO: do the same for GetCharacter() what will have been done to GetObject()
        public GameCharacter GetCharacter(string characterName, GameLocation location)
        {
            return (GameCharacter)location.Relationships.First(GameObjectRelationship => GameObjectRelationship.RelationshipTo.Name == characterName).RelationshipTo;
        }
    }
}
