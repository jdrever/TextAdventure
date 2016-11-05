using System;
using TextAdventure.Domain;

namespace TextAdventure.Interface
{
    public interface IObjectRepository
    {
        T GetObject<T>(Guid id) where T : GameBaseObject;

        void Add(GameBaseObject gameObject);
        void Remove(Guid id);
    }
}
