using System;
using TextAdventure.Domain;

namespace TextAdventure.Interface
{
    public interface IObjectRepository
    {
        GameBaseObject GetGameObject(string objectName, CharacterLocationDetails details);
        GameBaseObject GetGameObject(Guid ID, CharacterLocationDetails details);

        void SaveGameObject(GameObject gameObject);
    }
}
