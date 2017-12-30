
using System;
using TextAdventure.Domain;

namespace TextAdventure.Interface
{
    public interface ILocationRepository
    {

        GameObject GetGameObject(Guid id);
        void SaveGameObject(GameObject gameObject);
        //        GameObject GetContainingObjectForCharacter(Guid characterId);
    }
}
