using System;
using TextAdventure.Domain;
using TextAdventure.Interface;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TextAdventure.Infrastructure
{
    public class LocationRepository : ILocationRepository
    {
        public GameLocation GetLocation(string locationId)
        {
            string json = System.IO.File.ReadAllText(String.Format(@"C:\Users\Public\Log\{0}.txt", locationId));

            return JsonConvert.DeserializeObject<GameLocation>(json);
        }

        public void SaveLocation(GameLocation location)
        {
            string json = JsonConvert.SerializeObject(location);
            string fileDir = String.Format(@"C:\Users\Public\Log\{0}.txt", location.ID.ToString());

            System.IO.File.WriteAllText(fileDir, json);
        }
    }
}
