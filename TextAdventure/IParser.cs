using System;
using TextAdventure.Domain;

namespace TextAdventure.Interface
{
    public interface IParser
    {
        string ParseInput(Guid characterID, string input);
    }
}
