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
    public class ObjectRepository : IObjectRepository
    {
        private GameWorld _world;

        public ObjectRepository()
        { }

        public ObjectRepository(GameWorld world)
        {
            _world = world;
        }
        public GameBaseObject GetGameObject(string objectName, CharacterLocationDetails details)
        {
            if (_world!=null)
            {
                return SearchGameObjects(_world, objectName);
            }
            var location = GetGameObjectFromJSON(details.gameObjectId);
            return SearchGameObjects(location,objectName);
        }

        private GameBaseObject SearchGameObjects(GameBaseObject gameObject,string objectName)
        {
            if (CheckObjectName(objectName, gameObject))
                return gameObject;
            var childGameObjects = GetChildObjects(gameObject);
            foreach (var childGameObject in childGameObjects)
            {
                if (CheckObjectName(objectName, childGameObject))
                    return childGameObject;
                else
                    return SearchGameObjects(childGameObject, objectName);
            }
            return null;
        }

        public GameBaseObject GetGameObject(Guid id, CharacterLocationDetails details)
        {
            if (_world != null)
            {
                return SearchGameObjects(_world, id);
            }
            var location = GetGameObjectFromJSON(details.gameObjectId);
            return SearchGameObjects(location, id);
        }

        private GameBaseObject SearchGameObjects(GameBaseObject gameObject, Guid id)
        {
            if (id==gameObject.ID)
                return gameObject;
            var childGameObjects = GetChildObjects(gameObject);
            foreach (var childGameObject in childGameObjects)
            {
                if (id== childGameObject.ID)
                    return childGameObject;
                else
                    return SearchGameObjects(childGameObject, id);
            }
            return null;
        }


        public static List<GameBaseObject> GetChildObjects(GameBaseObject baseObject)
        {
            //TODO: check the list of "child" relationship types - are all relationship types effectively child types? 
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
