using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Domain
{
    public class GameWorld : GameBaseObject
    {
        public GameWorld(string name)
        {
            Name = name;
        }
        public string CurrencyType { get; set; }
    }
}
