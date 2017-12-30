using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Domain;
using TextAdventure.Interface;

namespace TextAdventure.Infrastructure
{
    public class JSONObjectRepository : IObjectRepository
    {
        public T GetGameObject<T>(string objectName, CharacterLocationDetails details) where T:GameBaseObject
        {
            var containingObject = GetGameObjectFromJSON(details.gameObjectId);

            if (containingObject.Relationships == null)
                containingObject.Relationships = new List<GameObjectRelationship>();

            // get child objects
            var childObjects = GetChildObjects(containingObject);
            // check if they are the searched for node
            foreach (var gameObject in childObjects)
            {
                if (CheckObjectName(objectName, gameObject))
                    return gameObject as T;
                if (CheckObjectName(objectName, GetGameObject<T>(objectName, details)))
                {
                    return GetGameObject<T>(objectName, details);
                }
            }

            // when there are no more child objects, return null - the desired object obviously doesn't 
            // exist
            return null;
        }

        public T GetGameObject<T>(Guid ID, CharacterLocationDetails details) where T:GameBaseObject
        {
            var containingObject = GetGameObjectFromJSON(details.gameObjectId);
            if (containingObject.Relationships == null)
                containingObject.Relationships = new List<GameObjectRelationship>();

            // get child nodes
            var childObjects = GetChildObjects(containingObject);

            // check if they are the searched for node
            foreach (var gameObject in childObjects)
            {
                if (CheckObjectID(ID, gameObject))
                    return (T) gameObject;
                
                return GetGameObject<T>(ID, details);
            }

            // when there are no more child objects, return null - the desired object obviously doesn't 
            // exist
            return null;
        }

        private static List<GameBaseObject> GetChildObjects(GameBaseObject baseObject)
        {
            return (from objectRelationship in baseObject.Relationships where objectRelationship.RelationshipDirection == RelationshipDirection.ParentToChild && (objectRelationship.RelationshipType == RelationshipType.Contains || objectRelationship.RelationshipType == RelationshipType.IsHeldBy || objectRelationship.RelationshipType == RelationshipType.IsUnder) select objectRelationship.RelationshipTo).ToList();
        }

        private static bool CheckObjectName(string objectName, GameBaseObject baseObject)
        {
            return baseObject != null && baseObject.Name == objectName;
        }

        private static bool CheckObjectID(Guid ID, GameBaseObject baseObject)
        {
            return baseObject.ID == ID;
        }

        private GameObject GetGameObjectFromJSON(Guid id)
        {

            //TODO: put the file in a project directory
            var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var json = System.IO.File.ReadAllText($@"{appdata}\.textadventure\Logs\{id}.txt");

            return JsonConvert.DeserializeObject<GameObject>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                PreserveReferencesHandling = PreserveReferencesHandling.All
            });
        }

        public void SaveGameObject(GameObject gameObject)
        {
            string json = JsonConvert.SerializeObject(gameObject, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.All
            });


            //TODO: put the file in a project directory
            var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            System.IO.File.WriteAllText($@"{appdata}\.textadventure\Logs\{gameObject.ID}.txt", json);
        }
    }
}
