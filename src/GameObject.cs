using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameXna
{
    /// <summary>
    /// Main class that represents game's object
    /// </summary>
    public class GameObject
    {
        private Texture2D texture;
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
        public GameObject(Model _model, Matrix _world, string _name)
        {
            this.Model = _model;
            this.World = _world;
            this.position = new Vector3(0, 0, 0);
            this.name = _name;
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
                Vector3 movement = value - this.position;
                this.position = value;
                this.World *= Matrix.CreateTranslation(movement);
            }
        }

        #endregion
    }
}
