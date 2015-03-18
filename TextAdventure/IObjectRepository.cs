using TextAdventure.Domain;

namespace TextAdventure.Interface
{
    public interface IObjectRepository
    {
        GameObject GetObject(string objectName, GameLocation location);
        GameCharacter GetCharacter(string characterName, GameLocation location);
    }
}
