using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Domain
{
    public class Phone : GameObject
    {
        public Phone(string name, string phoneNumber)
        {
            Name = name;
            PhoneNumber = phoneNumber;
        }
        public string PhoneNumber { get; set; }
    }
}
