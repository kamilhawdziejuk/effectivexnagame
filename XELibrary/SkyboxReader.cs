using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace XELibrary
{
    public class SkyboxReader : ContentTypeReader<Skybox>
    {
        protected override Skybox Read(ContentReader input, Skybox existingInstance)
        {
            return new Skybox(input);
        }
    }

    public class Skybox
    {
        private Model skyboxModel;
        private Texture2D skyboxTexture;
        internal Skybox(ContentReader input)
        {
            skyboxModel = input.ReadObject<Model>();
            skyboxTexture = input.ReadObject<Texture2D>();
        }
        public void Draw(Matrix view, Matrix projection, Matrix world)
        {

            foreach (ModelMesh mesh in skyboxModel.Meshes)
            {
                foreach (BasicEffect be in mesh.Effects)
                {
                    be.Projection = projection;
                    be.View = view;
                    be.World = world;
                    be.Texture = skyboxTexture;
                    be.TextureEnabled = true;
                }
                mesh.Draw(SaveStateMode.SaveState);
            }
        }
    }
}
