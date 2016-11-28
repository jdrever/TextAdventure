using TextAdventure.Domain;

namespace TextAdventure.Interface
{
    public interface ICommandExecutor
    {
        CommandOperationStatus Take(GameObject gameObject, GameCharacter gameCharacter, IRelationshipRepository relationshipRepository);
        CommandOperationStatus Drop(GameObject gameObject, GameCharacter gameCharacter, GameLocation location, IRelationshipRepository relationshipRepository);
    }
}
