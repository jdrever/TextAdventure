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
            var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var json = System.IO.File.ReadAllText($@"{appdata}\.textadventure\Logs\{locationId}.txt");


            return JsonConvert.DeserializeObject<GameLocation>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                PreserveReferencesHandling = PreserveReferencesHandling.All
            });
        }

        public void SaveLocation(GameLocation location)
        {
            string json = JsonConvert.SerializeObject(location, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple,
                ReferenceLoopHandling  = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.All
            });


            //TODO: put the file in a project directory
            var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            System.IO.File.WriteAllText($@"{appdata}\.textadventure\Logs\{location.ID}.txt", json);
        }

        public GameLocation GetCharactersLocation(Guid characterID)
        {
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string fileDir = System.IO.File.ReadAllText($@"{appdata}\.textadventure\Logs\{characterID}.txt");
            return GetLocation(fileDir);
        }
        
        public void SaveCurrentLocation(GameCharacter gameCharacter, GameLocation gameLocation)
        {
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            System.IO.File.WriteAllText($@"{appdata}\.textadventure\Logs\{gameCharacter.ID}.txt", gameLocation.ID.ToString());

            // save the location so it can be accessed - might not be necessary later on?
            SaveLocation(gameLocation);
        }
    }
}

