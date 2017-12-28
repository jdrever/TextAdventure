using TextAdventure.Domain;

namespace TextAdventure.Interface
{
    public interface ICommandExecutor
    {
        CommandOperationStatus Take(GameCharacter gameCharacter, GameObject gameObject);
        CommandOperationStatus Drop(GameCharacter gameCharacter, GameObject gameObject, GameLocation location);
    }
}
