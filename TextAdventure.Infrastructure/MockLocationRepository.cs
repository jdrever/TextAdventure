using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Domain;
using TextAdventure.Interface;

namespace TextAdventure.Infrastructure
{
    public class MockLocationRepository : ILocationRepository
    {
        private 
        private ObjectRepository objectRepository = new ObjectRepository();
        public MockLocationRepository()
        {

        }


        public GameObject GetGameObject(Guid id)
        {
            return objectRepository.GetObjectFromID<GameObject>(id, container);
        }


        public void SaveGameObject(GameObject gameObject)
        {
            throw new NotImplementedException();
        }
    }
}
