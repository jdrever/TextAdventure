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
            gameobject.AddRelationship(RelationshipType.IsHeldBy, character);
            return status;
        }

        private void RemoveLocationRelationships(GameObject gameobject)
        {
            gameobject.Relationships.ToList().RemoveAll(x => x.RelationshipType==RelationshipType.IsWithin||x.RelationshipType==RelationshipType.IsUnder).
    
        }
    }
}
