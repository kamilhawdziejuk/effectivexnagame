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
using XELibrary;

namespace GameXna
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameXna : Game
    {
        /// <summary>
        /// Component --> Camera
        /// </summary>
        private Camera camera;

        /// <summary>
        /// Component ---> FPS
        /// </summary>
        private FPS fps;

        /// <summary>
        /// Component ---> Input
        /// </summary>
        private InputHandler input;

        private float sum = 0.05f;
        bool changed = true;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        global::GameXna.Figures.Triangle triangle;
        global::GameXna.Figures.Rectangle rectangleRight;
        global::GameXna.Figures.Rectangle rectangleLeft;
        global::GameXna.Figures.Rectangle rectangleCenter;

        Dictionary<BasicEffect, Figures.VerticesIndicesFigure> effects = new Dictionary<BasicEffect, Figures.VerticesIndicesFigure>();

        private Texture2D texture;
        private Texture2D textureCenter;
    
        private float rotationRate = 0;
        

        public GameXna()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
#if DEBUG
            fps = new FPS(this);
            Components.Add(fps);
#endif
            input = new InputHandler(this);
            Components.Add(input);

            camera = new Camera(this);
            Components.Add(camera);

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            this.triangle = new global::GameXna.Figures.Triangle(new Vector3(-1, 0.8f, 0), new Vector3(1, -0.8f, 0), new Vector3(-1, -0.8f, 0));
            this.rectangleRight = new global::GameXna.Figures.Rectangle(new Vector3(1.7f, -0.8f, -1), new Vector3(2.0f, 0.8f, 1));
            this.rectangleLeft = new global::GameXna.Figures.Rectangle(new Vector3(-2.0f, -0.8f, 1), new Vector3(-1.7f, 0.8f, -1));
            this.rectangleCenter = new global::GameXna.Figures.Rectangle(new Vector3(-1.7f, -0.8f, -1), new Vector3(1.7f, 0.8f, -1));
            base.Initialize();
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
            this.textureCenter = Content.Load<Texture2D>("Textures\\animeGirls");
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
            {
                this.Exit();
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        private void InitializeEffect(BasicEffect effect)
        {
            effect.World = Matrix.Identity; 
            effect.Projection = this.camera.Projection;
            effect.View = this.camera.View;
            effect.EnableDefaultLighting();
            effect.TextureEnabled = true;
            effect.Texture = this.texture;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //if (changed)
            {
                graphics.GraphicsDevice.RenderState.CullMode = CullMode.None;

                GraphicsDevice.Clear(Color.LightYellow);

                // TODO: Add your drawing code here
                graphics.GraphicsDevice.VertexDeclaration = new
                        VertexDeclaration(graphics.GraphicsDevice,
                        VertexPositionNormalTexture.VertexElements);

                //BasicEffect effectTriangle = new BasicEffect(graphics.GraphicsDevice, null);
                //this.InitializeEffect(effectTriangle);

                BasicEffect effectRectangleRight = new BasicEffect(graphics.GraphicsDevice, null);
                this.InitializeEffect(effectRectangleRight);

                BasicEffect effectRectangleLeft = new BasicEffect(graphics.GraphicsDevice, null);
                this.InitializeEffect(effectRectangleLeft);


                BasicEffect effectRectangleCenter = new BasicEffect(graphics.GraphicsDevice, null);
                this.InitializeEffect(effectRectangleCenter);
                effectRectangleCenter.Texture = this.textureCenter;
                //effectRectangleCenter.World = Matrix.CreateTranslation(0, this.rotationRate, 0);
                this.effects.Clear();
                this.effects.Add(effectRectangleRight, this.rectangleRight);
                this.effects.Add(effectRectangleLeft, this.rectangleLeft);
                this.effects.Add(effectRectangleCenter, this.rectangleCenter);

                //world = Matrix.CreateRotationY(this.rotationRate);
                // world = Matrix.CreateRotationX(this.rotationRate);
                // worldData.World = Matrix.CreateTranslation(this.rotationRate, 0, 0);
                foreach (KeyValuePair<BasicEffect, Figures.VerticesIndicesFigure> kvp in this.effects)
                {
                    BasicEffect effect = kvp.Key;
                    effect.Begin();
                    foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                    {
                        pass.Begin();
                        kvp.Value.Draw(graphics.GraphicsDevice);
                        pass.End();
                    }
                    effect.End();
                }

                base.Draw(gameTime);
                changed = false;
            }
        }
    }
}
