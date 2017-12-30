using System.Linq;

namespace TextAdventure.Domain
{
    public class GameCharacter : GameObject
    {
        public GameCharacter(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            string toString = base.ToString();
            toString += Name + " is wearing: " + Wears();
            return toString;
        }

        public string Gender { get; set; }

        public string FirstName => Name.Split(' ')[0];

        public string Surname => string.Format(Name.Replace(FirstName + " ", string.Empty));

        public GameBaseObject GetCurrentLocation()
        {
            return Relationships.Where(p => p.RelationshipType == RelationshipType.Contains && p.RelationshipDirection == RelationshipDirection.ChildToParent).Select(a => a.RelationshipTo).First();
        }

        public GameBaseObject DefaultHandlingObject { get; set; }
    }


}
