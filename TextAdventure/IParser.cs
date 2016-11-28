using System;
using TextAdventure.Domain;

namespace TextAdventure.Interface
{
    public interface IParser
    {
        CommandOperationStatus ParseInput(Guid characterID, string input);
    }
}
