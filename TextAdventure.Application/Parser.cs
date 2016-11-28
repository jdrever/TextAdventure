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

        public CommandOperationStatus ParseInput(Guid characterId, string input)
        {
            var words = input.Split(' ');
            var firstWord = words[0].ToUpper();
            var secondWords = words[1];

            switch (firstWord)
            {
                case "TAKE":
                    return _commandCoordinator.Take(secondWords, characterId);
                case "DROP":
                    return _commandCoordinator.Drop(secondWords, characterId);
                default:
                    break;
            }
            return new CommandOperationStatus{Message = "I didn't understand!", Status = false};
        }
    }
}