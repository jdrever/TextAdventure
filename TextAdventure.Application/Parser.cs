using System;
using TextAdventure.Domain;
using TextAdventure.Interface;

namespace TextAdventure.Application
{
    public class Parser : IParser
    {
        private readonly ICommandCoordinator _commandCoordinator;

        //TODO: reinstate ActionCoordinator dependency

        public Parser(ICommandCoordinator commandCoordinator)
        {
            if (commandCoordinator == null) throw new ArgumentNullException("commandCoordinator");
            _commandCoordinator = commandCoordinator;
        }

        public string ParseInput(Guid characterID, string input)
        {
            var words = input.Split(' ');
            var firstWord = words[0].ToUpper();
            var secondWords = words[1];

            switch (firstWord)
            {
                case "TAKE":
                    return _commandCoordinator.Take(secondWords, characterID).Message;
                case "DROP":
                    return _commandCoordinator.Drop(secondWords, characterID).Message;
            }
            return "I didn't understand!";
        }
    }
}