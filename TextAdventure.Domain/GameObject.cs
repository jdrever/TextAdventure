using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Domain
{
    public class GameObject : GameBaseObject
    {
        public GameObject()
        {

        }
        public GameObject(string name)
        {
            Name = name;
        }




        public float WidthInMetres { get; set; }
        public float HeightInMetres { get; set; }
        public bool IsOpenable { get; set; }
        public bool IsTakeable { get; set; }
        public bool IsClimbable { get; set; }

        public bool IsOpen { get; set; }

        public bool IsWearable { get; set; }
        public bool CanHold { get; set; }

    }

}
