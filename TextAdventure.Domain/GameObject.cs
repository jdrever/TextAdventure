using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace TextAdventure.Domain
{
    [JsonObject(IsReference = false)]
    public class GameBaseObject
    {
        public Guid ID;

        public GameBaseObject()
        {
            ID = Guid.NewGuid();
        }


        public override string ToString()
        {
            string text = Name + ". ";
            if (!String.IsNullOrWhiteSpace(Description))
            {
                text += Description + ". ";
            }
            var containedObjects =
                Relationships.Where(p => p.RelationshipType == RelationshipType.Contains && p.RelationshipDirection == RelationshipDirection.ParentToChild).Select(a => a.RelationshipTo);
            if (containedObjects.Count() > 0)
            {
                text += "You can see: ";
                foreach (var containedObject in containedObjects)
                {
                    text += containedObject.Name + ". ";
                }
            }
            return text;
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public IList<GameObjectRelationship> Relationships { get; set; }

        public GameBaseObject GetContainingParent()
        {
            return Relationships.Single(r=>r.RelationshipType == RelationshipType.Contains && r.RelationshipDirection == RelationshipDirection.ChildToParent).RelationshipTo;

        }

        public void AddRelationship(RelationshipType relationshipType, RelationshipDirection relationshipDirection, GameBaseObject relationshipTo)
        {
            // if null instanciate Relationships
            if (Relationships == null)
                Relationships = new List<GameObjectRelationship>();

            // if relationshipTo's Relationships is null instanciate Relationships
            if (relationshipTo.Relationships == null)
                relationshipTo.Relationships = new List<GameObjectRelationship>();

            // add new relationship
            Relationships.Add(new GameObjectRelationship(relationshipType, relationshipDirection, relationshipTo));

            // add other type of relationship
            if (relationshipDirection == RelationshipDirection.ParentToChild)
                relationshipTo.Relationships.Add(new GameObjectRelationship(relationshipType, RelationshipDirection.ChildToParent, this));
            else if (relationshipDirection == RelationshipDirection.ChildToParent)
                relationshipTo.Relationships.Add(new GameObjectRelationship(relationshipType, RelationshipDirection.ParentToChild, this));

        }

        public void AddRelationship(RelationshipType relationshipType, GameBaseObject relationshipTo)
        {
            AddRelationship(relationshipType, RelationshipDirection.ParentToChild, relationshipTo);
        }

        public IList<GameBaseObject> GetParentChildRelationshipObjects(RelationshipType relationshipType)
        {
            if (Relationships == null) { return null; }
                
            var containingRelationships = Relationships.Where(p => p.RelationshipType == relationshipType && p.RelationshipDirection == RelationshipDirection.ParentToChild);
            if (containingRelationships.Count() > 0)
                return containingRelationships.Select(a => a.RelationshipTo).ToList();
            else
                return null;
        }

        public void Contains(GameBaseObject relationshipTo)
        {
            AddRelationship(RelationshipType.Contains, RelationshipDirection.ParentToChild, relationshipTo);
        }

        public IList<GameBaseObject> Contains()
        {
            return GetParentChildRelationshipObjects(RelationshipType.Contains);
        }

        public IList<GameBaseObject> Holds()
        {
            return GetParentChildRelationshipObjects(RelationshipType.IsHeldBy);
        }

        public void LeadsTo(GameBaseObject relationshipTo)
        {
            AddRelationship(RelationshipType.LeadsTo, RelationshipDirection.ParentToChild, relationshipTo);
        }
        //TODO: may need to revsit the LeadsTo etc methods returning a single GameBaseObject?
        //they may throw a null exception
        //and will they always return a single record?
        public GameBaseObject LeadsTo()
        {
            return Relationships.Where(p => p.RelationshipType == RelationshipType.LeadsTo && p.RelationshipDirection == RelationshipDirection.ParentToChild).Select(a => a.RelationshipTo).First();
        }

        public void GoesUpTo(GameBaseObject relationshipTo)
        {
            AddRelationship(RelationshipType.GoesUpTo, RelationshipDirection.ParentToChild, relationshipTo);
        }

        public GameBaseObject GoesUpTo()
        {
            return Relationships.Where(p => p.RelationshipType == RelationshipType.GoesUpTo && p.RelationshipDirection == RelationshipDirection.ParentToChild).Select(a => a.RelationshipTo).First();
        }

        public void GoesDownTo(GameBaseObject relationshipTo)
        {
            AddRelationship(RelationshipType.GoesDownTo, RelationshipDirection.ParentToChild, relationshipTo);
        }

        public GameBaseObject GoesDownTo()
        {
            return Relationships.Where(p => p.RelationshipType == RelationshipType.GoesDownTo && p.RelationshipDirection == RelationshipDirection.ParentToChild).Select(a => a.RelationshipTo).First();
        }

        public void Wears(GameBaseObject relationshipTo)
        {
            AddRelationship(RelationshipType.Wears, RelationshipDirection.ParentToChild, relationshipTo);
        }

        public GameBaseObject Wears()
        {
            return Relationships.Where(p => p.RelationshipType == RelationshipType.Wears && p.RelationshipDirection == RelationshipDirection.ParentToChild).Select(a => a.RelationshipTo).First();
        }

        /**public void RemoveRelationship_(GameObjectRelationship relationship)
        {
            // if null instanciate Relationships
            if (Relationships == null)
                Relationships = new List<GameObjectRelationship>();

            // if relationshipTo's Relationships is null instanciate Relationships
            if (relationship.RelationshipTo.Relationships == null)
                relationship.RelationshipTo.Relationships = new List<GameObjectRelationship>();

            // remove new relationship
            Relationships.Remove(relationship);

            // remove other type of relationship
            if (relationship.RelationshipDirection == RelationshipDirection.ParentToChild)
                relationship.RelationshipTo.Relationships.Remove(new GameObjectRelationship(relationship.RelationshipType, RelationshipDirection.ChildToParent, this));
            else if (relationship.RelationshipDirection == RelationshipDirection.ChildToParent)
                relationship.RelationshipTo.Relationships.Remove(new GameObjectRelationship(relationship.RelationshipType, RelationshipDirection.ParentToChild, this));
        }
        **/

        public void RemoveRelationship(RelationshipType relationshipType, RelationshipDirection relationshipDirection, GameBaseObject relationshipTo)
        {
            // if null instanciate Relationships
            if (Relationships == null)
                Relationships = new List<GameObjectRelationship>();

            // if relationshipTo's Relationships is null instanciate Relationships
            if (relationshipTo.Relationships == null)
                relationshipTo.Relationships = new List<GameObjectRelationship>();

            // remove new relationship
            var existingRelationship = Relationships.Single(r => r.RelationshipTo == relationshipTo && r.RelationshipType == relationshipType && r.RelationshipDirection == relationshipDirection);
            Relationships.Remove(existingRelationship);


            // remove other type of relationship
            if (relationshipDirection == RelationshipDirection.ParentToChild)
            {
                var existingOtherRelationship = relationshipTo.Relationships.Single(r => r.RelationshipTo.ID == this.ID && r.RelationshipDirection == RelationshipDirection.ChildToParent && r.RelationshipType == relationshipType);
                relationshipTo.Relationships.Remove(existingOtherRelationship);
            }

            else if (relationshipDirection == RelationshipDirection.ChildToParent)
            {
                var existingOtherRelationship = relationshipTo.Relationships.Single(r => r.RelationshipTo.ID == this.ID && r.RelationshipDirection == RelationshipDirection.ParentToChild && r.RelationshipType == relationshipType);
                relationshipTo.Relationships.Remove(existingOtherRelationship);
            }
        }

        public bool HasDirectRelationshipWith(GameBaseObject baseObject, RelationshipType type, RelationshipDirection direction)
        {
            if (Relationships == null)
                return false;

            var objectRelationship = new GameObjectRelationship(type, direction, baseObject);

            // Identifies object equality by ID. 
            // Returns whether the object's Relationships contains a relationship with the other object in the specified direction.
            return Relationships.Any(gameObjectRelationship => gameObjectRelationship.RelationshipTo.ID == objectRelationship.RelationshipTo.ID);
        }

        public bool HasIndirectRelationshipWith(GameBaseObject baseObject, RelationshipType type, RelationshipDirection direction)
        {
            if (Relationships == null)
                return false;

            if (HasDirectRelationshipWith(baseObject, type, direction))
                return false;

            // check child objects for direct relationship
            // check each child's child's child's ... 
            // ...

            return false;
        }

        public void Move(GameBaseObject currentParent, RelationshipType relationshipType, GameBaseObject newParent)
        {
            // if a relationship exists between this and the soon-to-be old parent
            if (currentParent.HasDirectRelationshipWith(this, relationshipType, RelationshipDirection.ParentToChild))
            {
                //remove that relationship
                currentParent.RemoveRelationship(relationshipType, RelationshipDirection.ParentToChild, this);

                //add a new one
                newParent.AddRelationship(relationshipType, RelationshipDirection.ParentToChild, this);
            }
        }
    }

    public class GameContainer : GameBaseObject
    {
        public GameContainer(string name)
        {
            Name = name;
        }
        public string CurrencyType { get; set; }
    }

    public class GameLocation : GameBaseObject
    {
        public GameLocation(string name)
        {
            Name = name;
        }
    }

    public class GameObject : GameBaseObject
    {
        public GameObject()
        {

        }
        public GameObject(string name)
        {
            Name = name;
        }




        public float WidthInMetres { get; set; }
        public float HeightInMetres { get; set; }
        public bool IsOpenable { get; set; }
        public bool IsTakeable { get; set; }
        public bool IsClimbable { get; set; }

        public bool IsOpen { get; set; }

        public bool IsWearable { get; set; }
        public bool CanHold { get; set; }

    }



    public class GameCurrencyObject : GameObject
    {
        public GameCurrencyObject(string name, decimal financialValue)
        {
            Name = name;
            FinancialValue = financialValue;
        }

        public decimal FinancialValue { get; set; }
    }

    public class Phone : GameObject
    {
        public Phone(string name, string phoneNumber)
        {
            Name = name;
            PhoneNumber = phoneNumber;
        }
        public string PhoneNumber { get; set; }
    }
}
    