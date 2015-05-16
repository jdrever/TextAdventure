using System;
using TextAdventure.Domain;
using TextAdventure.Interface;
using Newtonsoft.Json;

namespace TextAdventure.Infrastructure
{
    public class LocationRepository : ILocationRepository
    {
        public GameLocation GetLocation(string locationId)
        {
            //TODO: put the file in a project directory
            string json = System.IO.File.ReadAllText(String.Format(@"E:\Programming resources\Logs and Tests\{0}.txt", locationId));

            return JsonConvert.DeserializeObject<GameLocation>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
            });
        }

        public void SaveLocation(GameLocation location)
        {
            string json = JsonConvert.SerializeObject(location, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple,
                ReferenceLoopHandling  = ReferenceLoopHandling.Ignore
            });

            //TODO: put the file in a project directory
            string fileDir = String.Format(@"E:\Programming resources\Logs and Tests\{0}.txt", location.ID.ToString());

            System.IO.File.WriteAllText(fileDir, json);
        }
        public GameLocation GetCharactersLocation(GameCharacter gameCharacter)
        {
            return GetLocation(System.IO.File.ReadAllText(String.Format(@"E:\Programming resources\Logs and Tests\{0}.txt", gameCharacter.ID)));
        }
        
        public void SaveCurrentLocation(GameCharacter gameCharacter, GameLocation gameLocation)
        {
            System.IO.File.WriteAllText(String.Format(@"E:\Programming resources\Logs and Tests\{0}.txt", gameCharacter.ID), gameLocation.ID.ToString());

            // save the location so it can be accessed - might not be necessary later on?
            SaveLocation(gameLocation);
        }
    }
}

