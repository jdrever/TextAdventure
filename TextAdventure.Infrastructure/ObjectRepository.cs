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

        public GameObject GetObject(string objectName, GameLocation location)
        {
            var tryFirstLevel=GetObject(objectName,location);
            if (tryFirstLevel != null)
                return tryFirstLevel;

            // This might work?
            foreach (GameObject gameObject in 
                from ObjectRelationship in location.Relationships 
                where ObjectRelationship.RelationshipType == RelationshipType.Contains
                || ObjectRelationship.RelationshipType == RelationshipType.IsHeldBy
                || ObjectRelationship.RelationshipType == RelationshipType.IsUnder
                select ObjectRelationship.RelationshipTo)
            {
                var tryFindObject = GetObject(objectName, gameObject);

            }
        }


        private GameObject FindObject(string objectName, GameBaseObject baseObject)
        {
            // If object is there...
            if (baseObject.Relationships.Any(GameObjectRelationship => GameObjectRelationship.RelationshipTo.Name == objectName && (GameObjectRelationship.RelationshipType == RelationshipType.Contains || GameObjectRelationship.RelationshipType == RelationshipType.IsHeldBy || GameObjectRelationship.RelationshipType == RelationshipType.IsUnder)))
            {
                // Return object
                return (GameObject)baseObject.Relationships.First
                (GameObjectRelationship => GameObjectRelationship.RelationshipTo.Name == objectName
                    && (GameObjectRelationship.RelationshipType == RelationshipType.Contains
                    || GameObjectRelationship.RelationshipType == RelationshipType.IsHeldBy
                    || GameObjectRelationship.RelationshipType == RelationshipType.IsUnder)).RelationshipTo;
            }
            // Otherwise...
            else
            {
                return null;
            }

            if (baseObject.Relationships == null)
            {
                throw new Exception("Null relationship list.");
            }

            // If object is there...
            if (baseObject.Relationships.Any(GameObjectRelationship => GameObjectRelationship.RelationshipTo.Name == objectName && (GameObjectRelationship.RelationshipType == RelationshipType.Contains|| GameObjectRelationship.RelationshipType == RelationshipType.IsHeldBy|| GameObjectRelationship.RelationshipType == RelationshipType.IsUnder)))
            {
                // Return object
                return (GameObject)baseObject.Relationships.First
                (GameObjectRelationship => GameObjectRelationship.RelationshipTo.Name == objectName
                    && (GameObjectRelationship.RelationshipType == RelationshipType.Contains
                    || GameObjectRelationship.RelationshipType == RelationshipType.IsHeldBy
                    || GameObjectRelationship.RelationshipType == RelationshipType.IsUnder)).RelationshipTo;
            }
            // Otherwise...
            else
            {

            }
        }

        // TODO: do the same for GetCharacter() what will have been done to GetObject()
        public GameCharacter GetCharacter(string characterName, GameBaseObject baseObject)
        {
            return (GameCharacter)baseObject.Relationships.First(GameObjectRelationship => GameObjectRelationship.RelationshipTo.Name == characterName).RelationshipTo;
        }
    }
}
