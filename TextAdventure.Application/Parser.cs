using System;
using System.Linq;
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
            // cuts of the first word as the command word and any other words
            string commandWord;
            string otherWords;

            try
            {
                commandWord = input.Substring(0, input.IndexOf(' ')).ToUpper();
                otherWords = input.Substring(input.IndexOf(' ') + 1);
            }
            catch (Exception e) when (e is IndexOutOfRangeException || e is ArgumentOutOfRangeException)
            {
                return new CommandOperationStatus { Message = "I didn't understand that input!", Status = false };
            }

            if (string.IsNullOrWhiteSpace(otherWords))
                return new CommandOperationStatus { Message = "Only command given!", Status = false };

            switch (commandWord)
            {
                case "TAKE":
                    return _commandCoordinator.Take(otherWords, characterId);
                case "DROP":
                    return _commandCoordinator.Drop(otherWords, characterId);
                case "LOOK":
                    return _commandCoordinator.Look(otherWords, characterId);
                default:
                    return new CommandOperationStatus { Message = "I didn't understand that command word!", Status = false };
            }
        }
    }           
}