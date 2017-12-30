using System;
using TextAdventure.Domain;
using TextAdventure.Interface;

namespace TextAdventure.Application
{
    public class Parser : IParser
    {
        private readonly ICommandCoordinator _actionCoordinator;
        private readonly ITextSimplifier _textSimplifier;
        //TODO: reinstate ActionCoordinator dependency

        public Parser(ICommandCoordinator actionCoordinator, ITextSimplifier textSimplifier)
        {
            if (actionCoordinator == null) throw new ArgumentNullException("actionCoordinator");
            _actionCoordinator = actionCoordinator;
            if (textSimplifier == null) throw new ArgumentNullException("textSimplifier");
            _textSimplifier = textSimplifier;
        }

        public string ParseInput(CharacterLocationDetails details, string input)
        {
            var sentences = _textSimplifier.SimplifyText(input);
            foreach (string sentence in sentences)
            {
                var words = sentence.Split(' ');
                var firstWord = words[0];
                var furtherWords = words[1];


                switch (firstWord)
                {
                    case "TAKE":
                        return _actionCoordinator.Take(furtherWords, details).Message;
                    case "DROP":
                        return _actionCoordinator.Drop(furtherWords, details).Message;
                }
                return "I didn't understand!";
            }
            return "Action completed";

            
        }

        
    }
}