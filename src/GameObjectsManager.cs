using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameXna
{
    public class GameObjectsManager
    {

        List<GameObject> gameObjects = new List<GameObject>();

        public GameObject activeObject = null;

        public GameObject ActiveObject
        {
            get
            {
                return this.activeObject;
            }
            set
            {
                this.activeObject = value;
            }
        }

        public List<GameObject> GameObjects
        {
            get
            {
                return this.gameObjects;
            }
        }

        public GameObject GetObject(string _name)
        {
            return this.gameObjects.Find(a => a.Name.Equals(_name));
        }
        
        public void AddGameObject(GameObject _obj)
        {
            if (!this.gameObjects.Contains(_obj))
            {
                this.gameObjects.Add(_obj);
            }
        }

    }
}
