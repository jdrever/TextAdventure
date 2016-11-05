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

        public string Name { get; set; }
        public string Description { get; set; }

        public IList<GameObjectRelationship> Relationships { get; set; }

        public void AddRelationship(RelationshipType relationshipType, RelationshipDirection relationshipDirection, GameBaseObject relationshipTo)
        {
            // if null instanciate Relationships
            if (Relationships==null)
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

        public void RemoveRelationship(GameObjectRelationship relationship)
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

        public void RemoveRelationship(RelationshipType relationshipType, RelationshipDirection relationshipDirection, GameBaseObject relationshipTo)
        {
            // if null instanciate Relationships
            if (Relationships == null)
                Relationships = new List<GameObjectRelationship>();

            // if relationshipTo's Relationships is null instanciate Relationships
            if (relationshipTo.Relationships == null)
                relationshipTo.Relationships = new List<GameObjectRelationship>();

            // remove new relationship
            Relationships.Remove(new GameObjectRelationship(relationshipType, relationshipDirection, relationshipTo));

            // remove other type of relationship
            if (relationshipDirection == RelationshipDirection.ParentToChild)
                relationshipTo.Relationships.Remove(new GameObjectRelationship(relationshipType, RelationshipDirection.ChildToParent, this));
            else if (relationshipDirection == RelationshipDirection.ChildToParent)
                relationshipTo.Relationships.Remove(new GameObjectRelationship(relationshipType, RelationshipDirection.ParentToChild, this));

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

        // shouldn't be here
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
    }

    public class GameCharacter : GameBaseObject
    {
        public GameCharacter(string name)
        {
            Name = name;
        }
	
        public string Gender { get; set; }

        public string FirstName => Name.Split(' ')[0];

        public string Surname => string.Format(Name.Replace(FirstName + " ", string.Empty));
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

    [JsonObject(ItemReferenceLoopHandling = ReferenceLoopHandling.Serialize)]
    public class GameObjectRelationship
    {
        public GameObjectRelationship(RelationshipType relationshipType, RelationshipDirection relationshipDirection, GameBaseObject relationshipTo)
        {
            RelationshipType = relationshipType;
            RelationshipDirection = relationshipDirection;
            RelationshipTo = relationshipTo;
        }

        public RelationshipType RelationshipType { get; set; }
        public RelationshipDirection RelationshipDirection { get; set; }
        public GameBaseObject RelationshipTo { get; set; } 
    }


    public enum RelationshipType
    {
        Contains = 0,
        LeadsTo = 1,
        IsHeldBy = 2,
        IsUnder = 3,
        
    }

    public enum RelationshipDirection
    {
        ParentToChild = 0,
        ChildToParent = 1
    }
}
