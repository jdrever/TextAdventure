using System;
using TextAdventure.Domain;

namespace TextAdventure.Interface
{
    public interface IObjectRepository
    {
        T GetObjectFromName<T>(string objectName, GameBaseObject baseObject) where T : GameBaseObject;
        T GetObjectFromID<T>(Guid ID, GameBaseObject baseObject) where T : GameBaseObject;
    }
}
