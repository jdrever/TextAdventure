using System;
using TextAdventure.Domain;

namespace TextAdventure.Interface
{
    public interface IRelationshipRepository
    {
        GameObjectRelationship[] GetObjectRelationships(Guid id);

        void Add(GameObjectRelationship relationship);
        void Remove(GameObjectRelationship relationship);
    }
}
