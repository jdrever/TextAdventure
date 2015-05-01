using System;
using TextAdventure.Domain;
using TextAdventure.Interface;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace TextAdventure.Infrastructure
{
    public class LocationRepository : ILocationRepository
    {
        public GameLocation GetLocation(string locationId)
        {
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string json = System.IO.File.ReadAllText(String.Format(@"{0}\.textadventure\Log\{1}.txt", appdata, locationId));

            return JsonConvert.DeserializeObject<GameLocation>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects
            });
        }

        public void SaveLocation(GameLocation location)
        {
            string json = JsonConvert.SerializeObject(location, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });

            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string fileDir = System.IO.File.ReadAllText(String.Format(@"{0}\.textadventure\Log\{1}.txt", appdata, location.ID));

            System.IO.File.WriteAllText(fileDir, json);
        }

        // Getting current location???
        //public GameLocation GetCharactersLocation(GameCharacter gameCharacter)
        //{
        //    
        //}
        //
        //public void SaveCurrentLocation()
        //{
        //
        //}
    }
}
