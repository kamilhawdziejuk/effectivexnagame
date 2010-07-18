//18.07.2010
//gamexna
//author: Kamil Hawdziejuk

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace GameXna
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameXna : Microsoft.Xna.Framework.Game
    {
        private Camera camera = new Camera();

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private VertexPositionNormalTexture[] vertices;
        private Texture2D texture;

        private Matrix projection;
        private Matrix view;
        private Matrix world = Matrix.Identity;
        private float rotationRate = 0;

        public GameXna()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            this.InitializeWorld();
            this.InitializeVertices();
            base.Initialize();
        }

        private void InitializeWorld()
        {
            Matrix.CreateLookAt(ref this.camera.cameraPosition, ref this.camera.cameraTarget, ref this.camera.cameraUpVector, out view);

            float aspectRatio = (float)graphics.GraphicsDevice.Viewport.Width /
            (float)graphics.GraphicsDevice.Viewport.Height;
            Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio,
            0.0001f, 1000.0f, out projection);
        }

        private void InitializeVertices()
        {
            Vector3 position;
            Vector2 textureCoordinates;
            vertices = new VertexPositionNormalTexture[3];
            //top left
            position = new Vector3(-1, 1, 0);
            textureCoordinates = new Vector2(0, 0);
            vertices[0] = new VertexPositionNormalTexture(position, Vector3.Forward,
            textureCoordinates);
            //bottom right
            position = new Vector3(1, -1, 0);
            textureCoordinates = new Vector2(1, 1);
            vertices[1] = new VertexPositionNormalTexture(position, Vector3.Forward,
            textureCoordinates);
            //bottom left
            position = new Vector3(-1, -1, 0);
            textureCoordinates = new Vector2(0, 1);
            vertices[2] = new VertexPositionNormalTexture(position, Vector3.Forward,
            textureCoordinates);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void LoadGraphicsContent(bool loadAllContent)
        {
            texture = Content.Load<Texture2D>("Textures\\obraz");
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            graphics.GraphicsDevice.VertexDeclaration = new
                    VertexDeclaration(graphics.GraphicsDevice,
                    VertexPositionNormalTexture.VertexElements);
            BasicEffect effect = new BasicEffect(graphics.GraphicsDevice, null);
            //world = Matrix.Identity;
            effect.World = world;
            effect.Projection = projection;
            effect.View = view;
            effect.EnableDefaultLighting();
            effect.TextureEnabled = true;
            effect.Texture = this.texture;
            this.rotationRate += 0.02f;
            world = Matrix.CreateRotationY(this.rotationRate);
            world = Matrix.CreateRotationX(this.rotationRate);
            effect.Begin();
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Begin();
                graphics.GraphicsDevice.DrawUserPrimitives(
                PrimitiveType.TriangleList, vertices, 0,
                vertices.Length / 3);
                pass.End();
            }
            effect.End();



            base.Draw(gameTime);
        }
    }
}
