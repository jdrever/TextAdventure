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
        /// <summary>
        /// Will return null if no such object exists
        /// </summary>
        /// <param name="objectName"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public GameObject GetObject(string objectName, GameLocation location)
        {
            if (location.Relationships.Any(GameObjectRelationship => GameObjectRelationship.RelationshipTo.Name == objectName && GameObjectRelationship.RelationshipType == RelationshipType.Contains))
            {
                var gameObject = (GameObject)location.Relationships.First
                (GameObjectRelationship => GameObjectRelationship.RelationshipTo.Name == objectName
                    && GameObjectRelationship.RelationshipType == RelationshipType.Contains)
                    .RelationshipTo;
                return gameObject;
            }
            else
            {
                return null; 
            }
        }

        // Grr... dunno
        //public GameBaseObject foo(GameLocation location)
        //{
        //    List<GameBaseObject> objects = new List<GameBaseObject>();
        //
        //    foreach (GameObjectRelationship relationship in location.Relationships)
        //        if (relationship.RelationshipType == RelationshipType.Contains)
        //
        //    
        //    return foo(location);
        //}

        // TODO: do the same for GetCharacter() what will have been done to GetObject()
        public GameCharacter GetCharacter(string characterName, GameLocation location)
        {
            return (GameCharacter)location.Relationships.First(GameObjectRelationship => GameObjectRelationship.RelationshipTo.Name == characterName).RelationshipTo;
        }
    }
}
