using TextAdventure.Domain;

namespace TextAdventure.Interface
{
    public interface ICommandExecutor
    {
        CommandOperationStatus Take(GameObject gameObject, GameCharacter gameCharacter);
        CommandOperationStatus Drop(GameObject gameObject, GameCharacter gameCharacter, GameLocation location);
    }
}
