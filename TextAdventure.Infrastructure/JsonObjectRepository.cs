﻿using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TextAdventure.Domain;
using TextAdventure.Interface;

namespace TextAdventure.Infrastructure
{
    public class JsonObjectRepository : IObjectRepository
    {
        public T Get<T>(Guid id) where T : GameBaseObject
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
        // Todo - currently, everything is reset and created each time the test runs. This will need to change.
        // e.g the idMap needs to be loaded from a file instead of being dynamically created each time.
        public void Add(GameBaseObject gameObject)
        {
            var json = JsonConvert.SerializeObject(gameObject, Formatting.Indented);

            var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            File.WriteAllText($@"{appdata}\.textadventure\Logs\{gameObject.Id}.txt", json);

            _idMap.Add(gameObject.Name, gameObject.Id);
        }

        public void Remove(Guid id)
        {
            var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            File.Delete($@"{appdata}\.textadventure\Logs\{id}.txt");
        }

        private readonly Dictionary<string, Guid> _idMap = new Dictionary<string, Guid>();
        
        public Guid GetIdFromName(string name)
        {
            try
            {
                return _idMap[name];
            }
            catch (KeyNotFoundException)
            {
                return Guid.Empty; 
            }
        }
    }
}
