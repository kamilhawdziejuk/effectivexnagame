//07-08-2010
//Kamil Hawdziejuk

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using XELibrary;
using Microsoft.Xna.Framework.Graphics;

namespace GameXna
{
    public class GameObjectsManager : DrawableGameComponent
    {
        FirstPersonCamera camera;
        #region --- Private fields ---

        private SphereCollisionDetector sphereCollisionDetector = new SphereCollisionDetector();
        private List<GameObject> gameObjects = new List<GameObject>();
        private List<GameObject> collidedObjects = new List<GameObject>();
        private GameObject activeObject = null;

        #endregion

        #region --- Creator & destroyers ---

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"></param>
        public GameObjectsManager(Game game) : base(game)
        {
            camera = (game.Components[2] as FirstPersonCamera);
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

        public override void Draw(GameTime gameTime)
        {
            foreach (GameObject obj in this.gameObjects)
            {
                //DrawModel(ref obj.Model, ref obj.World);
                obj.Draw();
            }

            base.Draw(gameTime);
        }

        /// <summary>
        /// Draw model
        /// </summary>
        /// <param name="m"></param>
        /// <param name="world"></param>
        public void DrawModel(ref Model m, ref Matrix world)
        {
            if (m == null)
                return;
            Matrix[] transforms = new Matrix[m.Bones.Count];
            m.CopyAbsoluteBoneTransformsTo(transforms);
            camera = (this.Game.Components[2] as FirstPersonCamera);

            foreach (ModelMesh mesh in m.Meshes)
            {
                foreach (BasicEffect be in mesh.Effects)
                {
                    be.EnableDefaultLighting();
                    be.Projection = camera.Projection;
                    be.View = camera.View;
                    be.World = world * mesh.ParentBone.Transform;
                }
                mesh.Draw();
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (GameObject obj in this.gameObjects)
            {
                if (obj != this.activeObject)
                {
                    if (sphereCollisionDetector.DetectCollision(obj, this.activeObject, 0))
                    {
                    }
                }
            }
            base.Update(gameTime);
        }
    }
}
