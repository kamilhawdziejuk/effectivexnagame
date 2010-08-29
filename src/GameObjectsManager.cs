//07-08-2010
//Kamil Hawdziejuk

using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameXna
{
    public class GameObjectsManager : GameComponent
    {
        #region --- Private fields ---

        private SphereCollisionDetector sphereCollisionDetector = new SphereCollisionDetector();
        private List<GameObject> gameObjects = new List<GameObject>();
        private GameObject activeObject = null;

        #endregion

        #region --- Creator & destroyers ---

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"></param>
        public GameObjectsManager(Game game) : base(game)
        {
        }

        #endregion

        #region --- Public properties --- 

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

        #endregion

        #region --- Public methods ---

        public GameObject Get(string _name)
        {
            return this.gameObjects.Find(a => a.Name.Equals(_name));
        }
        
        public void Add(GameObject _obj)
        {
            if (!this.gameObjects.Contains(_obj))
            {
                this.gameObjects.Add(_obj);
            }
        }

        #endregion

        public override void Update(GameTime gameTime)
        {
            foreach (GameObject obj in this.gameObjects)
            {
                if (obj != this.activeObject)
                {
                    if (sphereCollisionDetector.DetectCollision(obj, this.activeObject, 0))
                    {
                        //  break;
                    }
                }
            }
            base.Update(gameTime);
        }
    }
}
