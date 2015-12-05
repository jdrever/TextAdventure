using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Domain;
using TextAdventure.Infrastructure;
using TextAdventure.Interface;

namespace TextAdventure.Application
{
    public class Parser : IParser
    {
        private ActionCoordinator _actionCoordinator;

        public Parser(ActionCoordinator actionCoordinator)
        {
            
            if (actionCoordinator == null) throw new ArgumentNullException("actionCoordinator");
            _actionCoordinator = actionCoordinator;
        }

        public string ParseInput(GameLocation location, string characterName, string input)
        {
            var words = input.Split(' ');
            var firstWord = words[0].ToUpper();
            var secondWords = words[1];

            if (firstWord == "TAKE")
            {
                return _actionCoordinator.Take(secondWords, characterName, location).Message;
            }
            return "I didn't understand!";
        }
    }
}
