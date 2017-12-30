using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Domain
{
    public class OperationStatus
    {
        public bool Status { get; set; }
        public string Message { get; set; }
    }

    public class CommandOperationStatus : OperationStatus
    {
        public CharacterLocationDetails details { get; set; }
    }
    public class CharacterLocationDetails
    {
        public Guid gameCharacterId { get; set; }
        public Guid gameObjectId { get; set; }
    }

}
