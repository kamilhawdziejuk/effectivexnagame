using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XELibrary;

namespace GameXna
{
    /// <summary>
    /// Main class that represents game's object
    /// </summary>
    public class GameObject : DrawableGameComponent
    {
        public FirstPersonCamera camera;
        public Model Model;
        public Matrix World;
        public float Scale;
        private string name = string.Empty;
        public List<Texture2D> Textures = new List<Texture2D>();

        #region --- Constructing & destroying objects ---

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_model"></param>
        /// <param name="_world"></param>
        /// <param name="_scale"></param>
        public GameObject(Game _game, Model _model, Matrix _world, string _name) : base(_game)
        {
            this.Model = _model;
            this.World = _world;
            this.position = new Vector3(0, 0, 0);
            this.name = _name;
            camera = (_game.Components[2] as FirstPersonCamera);

        }

        #endregion

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        #region --- Rotation ---

        float rotation;

        public float Rotation
        {
            get
            {
                return this.rotation;
            }
            set
            {
                this.rotation = value;
            }
        }

        #endregion


        #region --- Position ---

        private Vector3 position;

        public Vector3 Position
        {
            get
            {
                //return this.position;
                return this.World.Translation;
            }
            set
            {
                Vector3 movement = value - this.Position;
                this.position = value;
                this.World *= Matrix.CreateTranslation(movement);
            }
        }

        #endregion

        #region IDrawable Members

        public virtual void Draw()
        {
            Model m = this.Model;
            Matrix world = this.World;
            camera = (this.Game.Components[2] as FirstPersonCamera);

            if (m == null)
                return;
            Matrix[] transforms = new Matrix[m.Bones.Count];
            m.CopyAbsoluteBoneTransformsTo(transforms);

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

        public int DrawOrder
        {
            get { throw new System.NotImplementedException(); }
        }

        public event System.EventHandler DrawOrderChanged;

        public bool Visible
        {
            get { return true; }
        }

        public event System.EventHandler VisibleChanged;

        #endregion
    }
}
