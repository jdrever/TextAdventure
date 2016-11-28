using System;
using TextAdventure.Domain;

namespace TextAdventure.Interface
{
    public interface IObjectRepository
    {
        T Get<T>(Guid id) where T : GameBaseObject;

        void Add(GameBaseObject gameObject);
        void Remove(Guid id);

        Guid GetIdFromName(string name);
    }
}
