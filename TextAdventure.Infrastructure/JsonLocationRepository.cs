using System;
using TextAdventure.Domain;
using TextAdventure.Interface;
using Newtonsoft.Json;

namespace TextAdventure.Infrastructure
{
    public class JsonLocationRepository : ILocationRepository
    {
 **
        public GameObject GetContainingObjectForCharacter(Guid characterID)
        {
            //TODO: what happens if there isn't a file yet?
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string fileDir = System.IO.File.ReadAllText($@"{appdata}\.textadventure\Logs\{characterID}.txt");
            return GetGameObject(fileDir);
        }
        
        public void SaveCurrentLocation(GameCharacter gameCharacter, GameObject gameObject)
        {
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            System.IO.File.WriteAllText($@"{appdata}\.textadventure\Logs\{gameCharacter.ID}.txt", gameLocation.ID.ToString());

            // save the location so it can be accessed - might not be necessary later on?
            SaveGameObject(gameObject);
        }
**/
    }
}

