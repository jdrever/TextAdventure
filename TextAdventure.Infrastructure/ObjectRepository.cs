﻿using System;
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
            var childObjects = GetChildObjects<GameObject>(baseObject);

            // check if they are the searched for node
            foreach (GameObject gameObject in childObjects)
                // if yes return node
                if (CheckObject(objectName, gameObject))
                    return gameObject;
                // if not, repeat
                else
                    return GetObject(objectName, gameObject);

            // when there are no more child objects, return null - the desired object obviously doesn't 
            // exist
            return null;
        }

        public GameCharacter GetCharacter(string characterName, GameBaseObject baseObject)
        {
            if (baseObject.Relationships == null)
                baseObject.Relationships = new List<GameObjectRelationship>();

            // get child nodes
            var childObjects = GetChildObjects<GameCharacter>(baseObject);

            // check if they are the searched for node
            foreach (GameCharacter gameCharacter in childObjects)
                // if yes return node
                if (CheckObject(characterName, gameCharacter))
                    return gameCharacter;
                // if not, repeat
                else
                    return GetCharacter(characterName, gameCharacter);
            
            // when there are no more child objects, return null - the desired character obviously doesn't 
            // exist
            return null;
        }


        public static List<GameBaseObject> GetChildObjects<T>(GameBaseObject baseObject)
        {
            return (from objectRelationship in baseObject.Relationships where objectRelationship.RelationshipTo is T && (objectRelationship.RelationshipDirection == RelationshipDirection.ParentToChild) && (objectRelationship.RelationshipType == RelationshipType.Contains || objectRelationship.RelationshipType == RelationshipType.IsHeldBy || objectRelationship.RelationshipType == RelationshipType.IsUnder) select objectRelationship.RelationshipTo).ToList();
        }

        private static bool CheckObject(string objectName, GameBaseObject baseObject)
        {
            return baseObject.Name == objectName;
        }
    }
}
