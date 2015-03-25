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
        public GameObject GetObject(Guid objectID, GameLocation location)
        {
            // Needs to get all objects with which the location has a "contains" relationship with.

            // Find something that doesn't return whether the object exists, but the actual object.
            return location.Relationships.Any(GameObjectRelationship => GameObjectRelationship.RelationshipTo.ID ==  objectID);
        }

        public GameCharacter GetCharacter(Guid characterID, GameLocation location)
        {
            throw new System.NotImplementedException();
        }
    }
}
