using TextAdventure.Domain;

namespace TextAdventure.Interface
{
    public interface IParser
    {
        string ParseInput(GameLocation location, string characterName, string input);
    }
}
