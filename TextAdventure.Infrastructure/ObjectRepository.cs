using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Domain;
using TextAdventure.Interface;

namespace TextAdventure.Infrastructure
{
    public class ObjectRepository : IObjectRepository
    {

        public GameObject GetObject(string objectName, GameLocation location)
        {
            throw new NotImplementedException();
        }

        public GameCharacter GetCharacter(string characterName, GameLocation location)
        {
            throw new NotImplementedException();
        }
    }
}
