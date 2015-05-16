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
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string json = System.IO.File.ReadAllText(String.Format(@"{0}\.textadventure\Logs\{1}.txt", appdata, locationId));


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
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            System.IO.File.WriteAllText(String.Format(@"{0}\.textadventure\Logs\{1}.txt", appdata, location.ID), json);
        }

        public GameLocation GetCharactersLocation(GameCharacter gameCharacter)
        {
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string fileDir = System.IO.File.ReadAllText(String.Format(@"{0}\.textadventure\Logs\{1}.txt", appdata, gameCharacter.ID));
            return GetLocation(fileDir);
        }
        
        public void SaveCurrentLocation(GameCharacter gameCharacter, GameLocation gameLocation)
        {
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            System.IO.File.WriteAllText(String.Format(@"{0}\.textadventure\Logs\{1}.txt", appdata, gameCharacter.ID), gameLocation.ID.ToString());

            // save the location so it can be accessed - might not be necessary later on?
            SaveLocation(gameLocation);
        }
    }
}

