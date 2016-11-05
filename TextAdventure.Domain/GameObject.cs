using System;

namespace TextAdventure.Domain
{
    public class GameBaseObject 
    {
        public Guid Id;

        public GameBaseObject()
        {
            Id = Guid.NewGuid();
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public override bool Equals(object obj)
        {
            var o = obj as GameBaseObject;
            if (o != null)
            {
                return o.Id == Id && o.Name == Name && o.Description == Description;
            }
            return base.Equals(obj);
        }
    }
    
    public class GameContainer : GameBaseObject
    {
        public string CurrencyType { get; set; }

        public override bool Equals(object obj)
        {
            var o = obj as GameContainer;
            if (o != null)
            {
                return base.Equals(o) && o.CurrencyType == CurrencyType;
            }
            return base.Equals(obj);
        }
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
        public GameObject() { }

        public GameObject(string name)
        {
            Name = name;
        }

        public float WidthInMetres { get; set; }
        public float HeightInMetres { get; set; }
        public bool IsOpenable { get; set; }
        public bool IsTakeable { get; set; }

        public override bool Equals(object obj)
        {
            var o = obj as GameObject;
            if (o != null)
            {
                return base.Equals(o) && o.IsOpenable == IsOpenable && o.IsTakeable == IsTakeable
                    && o.HeightInMetres == HeightInMetres && o.WidthInMetres == WidthInMetres;
            }
            return base.Equals(obj);
        }
    }

    public class GameCharacter : GameBaseObject
    {
        public GameCharacter(string name)
        {
            Name = name;
        }
	
        public string Gender { get; set; }

        // Todo - replace with in-game DoB?
        public int Age { get; set; }

        public override bool Equals(object obj)
        {
            var o = obj as GameCharacter;
            if (o != null)
            {
                return base.Equals(o) && o.Age == Age && o.Gender == Gender;
            }
            return base.Equals(obj);
        }
    }

    public class GameCurrencyObject : GameObject
    {
        public GameCurrencyObject(string name, decimal financialValue)
        {
            Name = name;
            FinancialValue = financialValue;
        }

        public decimal FinancialValue { get; set; }

        public override bool Equals(object obj)
        {
            var o = obj as GameCurrencyObject;
            if (o != null)
            {
                return base.Equals(o) && o.FinancialValue == FinancialValue;
            }
            return base.Equals(obj);
        }
    }

    public class GameObjectRelationship
    {
        public GameObjectRelationship(Guid parentObjectId, Guid childObjectId, RelationshipType relationshipType)
        {
            ParentObjectId = parentObjectId;
            ChildObjectId = childObjectId;
            RelationshipType = relationshipType;
        }

        public RelationshipType RelationshipType { get; set; }

        public Guid ParentObjectId { get; set; }
        public Guid ChildObjectId { get; set; }
    }

    public enum RelationshipType
    {
        Contains = 0,
        LeadsTo = 1,
        IsHeldBy = 2,
        IsUnder = 3
    }
}
