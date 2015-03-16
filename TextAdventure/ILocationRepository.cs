
using TextAdventure.Domain;

namespace TextAdventure.Interface
{
    public interface ILocationRepository
    {
        GameLocation GetLocation(string locationId);
    }
}
