using TextAdventure.Domain;

namespace TextAdventure.Interface
{
    public interface IObjectRepository
    {
        GameObject GetObject(string objectName, GameBaseObject baseObject);
        GameCharacter GetCharacter(string characterName, GameBaseObject baseObject);
    }
}
