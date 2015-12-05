using System;
using TextAdventure.Domain;
using TextAdventure.Interface;

namespace TextAdventure.Application
{
    public class Parser : IParser
    {
        private readonly ActionCoordinator _actionCoordinator;

        public Parser(ActionCoordinator actionCoordinator)
        {
            if (actionCoordinator == null) throw new ArgumentNullException("actionCoordinator");
            _actionCoordinator = actionCoordinator;
        }

        public string ParseInput(Guid characterID, string input)
        {
            var words = input.Split(' ');
            var firstWord = words[0].ToUpper();
            var secondWords = words[1];

            switch (firstWord)
            {
                case "TAKE":
                    return _actionCoordinator.Take(secondWords, characterID).Message;
                case "DROP":
                    return _actionCoordinator.Drop(secondWords, characterID).Message;
            }
            return "I didn't understand!";
        }
    }
}