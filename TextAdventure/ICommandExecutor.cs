using TextAdventure.Domain;

namespace TextAdventure.Interface
{
    public interface ICommandExecutor
    {
        CommandOperationStatus Take(GameObject gameObject, GameCharacter gameCharacter, IRelationshipRepository relationshipRepository);
        CommandOperationStatus Describe(GameBaseObject gameObject, GameCharacter gameCharacter, IRelationshipRepository relationshipRepository, IObjectRepository objectRepository);
        CommandOperationStatus Drop(GameObject gameObject, GameCharacter gameCharacter, GameLocation location, IRelationshipRepository relationshipRepository);
    }
}
