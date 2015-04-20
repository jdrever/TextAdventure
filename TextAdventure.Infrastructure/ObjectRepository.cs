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
        public GameObject GetObject(string objectName, GameBaseObject baseObject)
        {
            if (baseObject.Relationships == null)
                baseObject.Relationships = new List<GameObjectRelationship>();

            // get child nodes
            var childObjects = GetChildObjects(baseObject);

            // check if they are the searched for node
            foreach (GameObject gameObject in childObjects)
                // if yes return node
                if (CheckObject(objectName, gameObject))
                    return gameObject;
                // if not, repeat
                else
                    return GetObject(objectName, gameObject);
                
            return null;
        }

        public GameCharacter GetCharacter(string characterName, GameBaseObject baseObject)
        {
            if (baseObject.Relationships == null)
                baseObject.Relationships = new List<GameObjectRelationship>();

            // get child nodes
            var childObjects = GetChildObjects(baseObject);

            // check if they are the searched for node
            foreach (GameCharacter gameCharacter in childObjects)
                // if yes return node
                if (CheckObject(characterName, gameCharacter))
                    return gameCharacter;
                // if not, repeat
                else
                    return GetCharacter(characterName, gameCharacter);
            
            return null;
        }

        private List<GameBaseObject> GetChildObjects(GameBaseObject gameObject)
        {
            // Get all child objects
            List<GameBaseObject> allChildObjects = (from ObjectRelationship in gameObject.Relationships
                                                    where (ObjectRelationship.RelationshipTo is GameObject)
                                                    && (ObjectRelationship.RelationshipDirection == RelationshipDirection.ParentToChild)
                                                    && (ObjectRelationship.RelationshipType == RelationshipType.Contains
                                                    || ObjectRelationship.RelationshipType == RelationshipType.IsHeldBy
                                                    || ObjectRelationship.RelationshipType == RelationshipType.IsUnder)
                                                    select ObjectRelationship.RelationshipTo).ToList<GameBaseObject>();

            // Get those that are of type GameObject and
            // return those
            return allChildObjects;
        }

        private bool CheckObject(string objectName, GameBaseObject gameObject)
        {
            if (gameObject.Name == objectName)
                return true;
            else
                return false;
        }
    }
}
