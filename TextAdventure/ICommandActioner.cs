using TextAdventure.Domain;

namespace TextAdventure.Interface
{
    public interface ICommandActioner
    {
        CommandOperationStatus Take(GameObject gameObject, GameCharacter gameCharacter);
        CommandOperationStatus Drop(GameObject gameObject, GameCharacter gameCharacter);
    }
}
