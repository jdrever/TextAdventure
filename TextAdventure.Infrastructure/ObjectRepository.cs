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
        public T GetObjectFromName<T>(string objectName, GameBaseObject baseObject) where T:GameBaseObject
        {
            if (baseObject.Relationships == null)
                baseObject.Relationships = new List<GameObjectRelationship>();

            // get child objects
            var childObjects = GetChildObjects(baseObject);
            // check if they are the searched for node
            foreach (var gameObject in childObjects)
            {
                if (CheckObjectName(objectName, gameObject))
                    return gameObject as T;
                if (CheckObjectName(objectName, GetObjectFromName<T>(objectName, gameObject)))
                {
                    return GetObjectFromName<T>(objectName, gameObject);
                }
            }

            // when there are no more child objects, return null - the desired object obviously doesn't 
            // exist
            return null;
        }

        public T GetObjectFromID<T>(Guid ID, GameBaseObject baseObject) where T:GameBaseObject
        {
            if (baseObject.Relationships == null)
                baseObject.Relationships = new List<GameObjectRelationship>();

            // get child nodes
            var childObjects = GetChildObjects(baseObject);

            // check if they are the searched for node
            foreach (var gameObject in childObjects)
            {
                if (CheckObjectID(ID, gameObject))
                    return (T) gameObject;
                
                return GetObjectFromID<T>(ID, gameObject);
            }

            // when there are no more child objects, return null - the desired object obviously doesn't 
            // exist
            return null;
        }

        public static List<GameBaseObject> GetChildObjects(GameBaseObject baseObject)
        {
            return (from objectRelationship in baseObject.Relationships where objectRelationship.RelationshipDirection == RelationshipDirection.ParentToChild && (objectRelationship.RelationshipType == RelationshipType.Contains || objectRelationship.RelationshipType == RelationshipType.IsHeldBy || objectRelationship.RelationshipType == RelationshipType.IsUnder) select objectRelationship.RelationshipTo).ToList();
        }

        private static bool CheckObjectName(string objectName, GameBaseObject baseObject)
        {
            return baseObject != null && baseObject.Name == objectName;
        }

        private static bool CheckObjectID(Guid ID, GameBaseObject baseObject)
        {
            return baseObject.ID == ID;
        }
    }
}
