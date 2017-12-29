using Newtonsoft.Json;
namespace TextAdventure.Domain
{

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
        GoesDownTo = 4,
        GoesUpTo = 5,
        Wears=6

    }

    public enum RelationshipDirection
    {
        ParentToChild = 0,
        ChildToParent = 1
    }

}
