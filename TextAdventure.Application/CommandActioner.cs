using System.Linq;
using System.Security.Cryptography.X509Certificates;
using TextAdventure.Domain;
using TextAdventure.Interface;

namespace TextAdventure.Application
{
    class CommandActioner : ICommandActioner
    {
        public CommandOperationStatus Take(GameObject gameObject, GameCharacter gameCharacter)
        {
            var status = new CommandOperationStatus();
            RemoveLocationRelationships(gameObject);
            gameObject.AddRelationship(RelationshipType.IsHeldBy, RelationshipDirection.ChildToParent, gameCharacter);
            return status;
        }

        private void RemoveLocationRelationships(GameObject gameobject)
        {
            gameobject.Relationships.ToList().RemoveAll
                (GameObjectRelationship =>
                    GameObjectRelationship.RelationshipDirection == RelationshipDirection.ChildToParent
                    && (GameObjectRelationship.RelationshipType == RelationshipType.Contains
                    || GameObjectRelationship.RelationshipType == RelationshipType.IsUnder
                    || GameObjectRelationship.RelationshipType == RelationshipType.LeadsTo));
        }
    }
}
