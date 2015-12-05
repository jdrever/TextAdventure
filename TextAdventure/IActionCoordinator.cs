using System;
using TextAdventure.Domain;

namespace TextAdventure.Interface
{
    public interface IActionCoordinator
    {
        CommandOperationStatus Take(string objectName, Guid characterID);
        CommandOperationStatus Drop(string objectName, Guid characterID);
    }
}
