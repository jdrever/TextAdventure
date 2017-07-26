using System;
using TextAdventure.Domain;

namespace TextAdventure.Interface
{
    public interface ICommandCoordinator
    {
        CommandOperationStatus Take(string objectName, Guid characterID);
        CommandOperationStatus Drop(string objectName, Guid characterID);
        CommandOperationStatus Look(string whereToLook, Guid characterID);
    }
}
