using System;
using TextAdventure.Domain;

namespace TextAdventure.Interface
{
    public interface IObjectRepository
    {
        T GetGameObject<T>(string objectName, CharacterLocationDetails details) where T : GameBaseObject;
        T GetGameObject<T>(Guid ID, CharacterLocationDetails details) where T : GameBaseObject;

        void SaveGameObject(GameObject gameObject);
    }
}
