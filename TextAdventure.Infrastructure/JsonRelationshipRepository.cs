using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using TextAdventure.Domain;
using TextAdventure.Interface;

namespace TextAdventure.Infrastructure
{
    public class JsonRelationshipRepository : IRelationshipRepository
    {
        public JsonRelationshipRepository()
        {
            #region Create empty relationship.txt, just in case

            var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            //File.Create($@"{appdata}\.textadventure\Logs\relationships.txt");

            GameObjectRelationship[] array = {};

            // Create empty array
            var json = JsonConvert.SerializeObject(array, Formatting.Indented);

            
            File.WriteAllText($@"{appdata}\.textadventure\Logs\relationships.txt", json);
            #endregion
        }

        public GameObjectRelationship[] GetObjectRelationships(Guid id)
        {
            return (from relationship in GetRelationships()
                where relationship.ChildObjectId == id || relationship.ParentObjectId == id
                select relationship).ToArray();
        }

        public void Add(GameObjectRelationship relationship)
        {
            var relationships = new List<GameObjectRelationship>(GetRelationships()) {relationship};

            SaveRelationships(relationships.ToArray());
        }

        public void Remove(GameObjectRelationship relationship)
        {
            var relationships = new List<GameObjectRelationship>(GetRelationships());
            relationships.Remove(relationship);

            SaveRelationships(relationships.ToArray());
        }

        private GameObjectRelationship[] GetRelationships()
        {
            var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var json = File.ReadAllText($@"{appdata}\.textadventure\Logs\relationships.txt");

            return JsonConvert.DeserializeObject<GameObjectRelationship[]>(json);
        }

        private void SaveRelationships(GameObjectRelationship[] relationships)
        {
            var json = JsonConvert.SerializeObject(relationships, Formatting.Indented);

            var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            File.WriteAllText($@"{appdata}\.textadventure\Logs\relationships.txt", json);
        }
    }
}
