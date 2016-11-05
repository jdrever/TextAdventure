using System;
using System.IO;
using Newtonsoft.Json;
using TextAdventure.Domain;
using TextAdventure.Interface;

namespace TextAdventure.Infrastructure
{
    public class JsonObjectRepository : IObjectRepository
    {
        // Todo - get by name? Would require map of id to name?

        public T GetObject<T>(Guid id) where T : GameBaseObject
        {
            // Todo - properly manage file not found
            try
            {
                var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                var json = File.ReadAllText($@"{appdata}\.textadventure\Logs\{id}.txt");
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }

        // Todo - update? (if given an object that is already saved, Add will update...)

        public void Add(GameBaseObject gameObject)
        {
            var json = JsonConvert.SerializeObject(gameObject, Formatting.Indented);

            var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            File.WriteAllText($@"{appdata}\.textadventure\Logs\{gameObject.Id}.txt", json);
        }

        public void Remove(Guid id)
        {
            var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            File.Delete($@"{appdata}\.textadventure\Logs\{id}.txt");
        }
    }
}
