using System.Linq;
using System.Security.Cryptography.X509Certificates;
using TextAdventure.Domain;
using TextAdventure.Interface;

namespace TextAdventure.Application
{
    class CommandActioner : ICommandActioner
    {
        public CommandOperationStatus Take(GameObject gameobject, GameObject character)
        {
            var status = new CommandOperationStatus();
            //RemoveLocationRelationships(gameobject);
            gameobject.AddRelationship(RelationshipType.IsHeldBy, RelationshipDirection.ChildToParent, character);
            return status;
        }

        private void RemoveLocationRelationships(GameObject gameobject)
        {
            //gameobject.Relationships.ToList().RemoveAll
            //    (GameObjectRelationship => 
            //    GameObjectRelationship.RelationshipType==RelationshipType.Contains
            //    ||GameObjectRelationship.RelationshipType==RelationshipType.IsUnder).
            //
        }
    }
}
