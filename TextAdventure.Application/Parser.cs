using System;
using TextAdventure.Domain;
using TextAdventure.Interface;

namespace TextAdventure.Application
{
    public class Parser : IParser
    {
        private readonly ICommandCoordinator _actionCoordinator;

        //TODO: reinstate ActionCoordinator dependency

        public Parser(ICommandCoordinator actionCoordinator)
        {
            if (actionCoordinator == null) throw new ArgumentNullException("actionCoordinator");
            _actionCoordinator = actionCoordinator;
        }

        public string ParseInput(Guid characterID, string input)
        {
            var sentences = input.Split('.');
            foreach (string sentence in sentences)
            {
                var words = sentence.Split(' ');
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
}