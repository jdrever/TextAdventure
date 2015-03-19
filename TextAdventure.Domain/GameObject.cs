using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TextAdventure.Domain
{
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

        public void AddRelationship(RelationshipType relationshipType, GameBaseObject relationshipTo)
        {
            if (Relationships==null)
                Relationships=new List<GameObjectRelationship>();
            Relationships.Add(new GameObjectRelationship(relationshipType, relationshipTo));
        }

        public bool HasRelationshipWith(GameBaseObject baseObject, RelationshipType type)
        {
            if (Relationships == null)
                return false;

            var objectRelationship = new GameObjectRelationship(type, baseObject);

            if (Relationships.Contains(objectRelationship))
                return true;
            else
                return false;
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
        //TODO: override Name to return FirstName+" "+Surname
        public GameCharacter(string firstName, string surname)
        {
            FirstName = firstName;
            Surname = surname;
        }
	
        // Will this work?
	    public new string Name
	    {
		get
		    {
			    return this.FirstName + " " + this.Surname;
		    }
	    }
	
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }

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

    public class GameObjectRelationship
    {
        public GameObjectRelationship(RelationshipType relationshipType, GameBaseObject relationshipTo)
        {
            RelationshipType = relationshipType;
            RelationshipTo = relationshipTo;
        }

        public RelationshipType RelationshipType { get; set; }
        public GameBaseObject RelationshipTo { get; set; } 
    }

    public enum RelationshipType
    {
        Contains = 0,
        LeadsTo = 1,
        IsHeldBy = 2,
        IsUnder = 3,

    }

    public enum ObjectType
    {
        GameContainer=0,
        Location=1,
        Object=2,
        Character=3
    }
}
