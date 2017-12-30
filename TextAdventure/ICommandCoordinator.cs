using System;
using TextAdventure.Domain;

namespace TextAdventure.Interface
{
    public interface ICommandCoordinator
    {
        CommandOperationStatus Take(string objectName, CharacterLocationDetails details);
        CommandOperationStatus Drop(string objectName, CharacterLocationDetails details);
    }
}
