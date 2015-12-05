using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public string ParseInput(string input)
        {
            string[] words = input.Split(' ');
            string firstWords = words[0].ToUpper();

            if (firstWords == "TAKE")
            {
                // need location/character
                return _actionCoordinator.Take(words[0], null, null).Message;
            }
            return "I didn't understand!";
        }
    }
}
