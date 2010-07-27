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
using StillDesign;

namespace GameXna
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameXna : Game
    {
        #region --- Components ---

        /// <summary>
        /// Component --> FirstPersonCamera
        /// </summary>
        private FirstPersonCamera camera;

        /// <summary>
        /// Component ---> FPS
        /// </summary>
        private FPS fps;

        /// <summary>
        /// Component ---> Input
        /// </summary>
        private InputHandler input;

        #endregion

        #region --- Private fields ---

        private GameObjectsManager GameObjectManager;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Texture2D texture;
        private Texture2D textureCenter;

        #endregion

        #region --- Basic objects / effects ---

        global::GameXna.Figures.Triangle triangle;
        global::GameXna.Figures.Rectangle rectangleRight;
        global::GameXna.Figures.Rectangle rectangleLeft;
        global::GameXna.Figures.Rectangle rectangleCenter;

        Dictionary<BasicEffect, Figures.VerticesIndicesFigure> effects = new Dictionary<BasicEffect, Figures.VerticesIndicesFigure>();

        #endregion

        #region --- Creating & destroying objects ---

        /// <summary>
        /// Constructor
        /// </summary>
        public GameXna()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.Content = new ContentManager(Services);
            this.GameObjectManager = new GameObjectsManager();

            Content.RootDirectory = "Content";
#if DEBUG
            fps = new FPS(this);
            Components.Add(fps);
#endif
            input = new InputHandler(this);
            Components.Add(input);

            camera = new FirstPersonCamera(this);
            Components.Add(camera);

        }

        #endregion

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

            Matrix carWorld = Matrix.CreateScale(0.0015f) * 
                Matrix.CreateRotationX(MathHelper.ToRadians(90.0f)) *
                Matrix.CreateRotationY(MathHelper.ToRadians(90.0f)) *
                Matrix.CreateTranslation(new Vector3(0.5f, -0.5f, 0));

            this.GameObjectManager.AddGameObject(new GameObject(null, carWorld, "car"));

            base.Initialize();
        }

        private void DrawGameObject(GameObject gameObject)
        {
            DrawModel(ref gameObject.Model, ref gameObject.World);
        }

        private void DrawModel(ref Model m, ref Matrix world)
        {

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
            if (loadAllContent)
            {
                // TODO: Load any ResourceManagementMode.Automatic content
                //bikeModel = Content.Load<Model>("Models\\ClassicBike");
                //bikeModel = Content.Load<Model>("Models\\BesballBat");
                this.GameObjectManager.GetObject("car").Model = Content.Load<Model>("Models\\ford");
            }
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

        /// <summary>
        /// Initializates one basic effect
        /// </summary>
        /// <param name="effect"></param>
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
            graphics.GraphicsDevice.RenderState.CullMode = CullMode.None;
            GraphicsDevice.Clear(Color.LightYellow);

            graphics.GraphicsDevice.VertexDeclaration = new
                    VertexDeclaration(graphics.GraphicsDevice,
                    VertexPositionNormalTexture.VertexElements);

            //wall1
            BasicEffect effectRectangleRight = new BasicEffect(graphics.GraphicsDevice, null);
            this.InitializeEffect(effectRectangleRight);
            //wall2
            BasicEffect effectRectangleLeft = new BasicEffect(graphics.GraphicsDevice, null);
            this.InitializeEffect(effectRectangleLeft);
            //wall_center
            BasicEffect effectRectangleCenter = new BasicEffect(graphics.GraphicsDevice, null);
            this.InitializeEffect(effectRectangleCenter);
            effectRectangleCenter.Texture = this.textureCenter;

            this.effects.Clear();
            this.effects.Add(effectRectangleRight, this.rectangleRight);
            this.effects.Add(effectRectangleLeft, this.rectangleLeft);
            this.effects.Add(effectRectangleCenter, this.rectangleCenter);

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

            foreach (GameObject obj in this.GameObjectManager.GameObjects)
            {
                obj.Position = new Vector3(obj.Position.X, obj.Position.Y, obj.Position.Z + 0.001f);
                DrawGameObject(obj);
            }

            base.Draw(gameTime);
        }
    }
}
