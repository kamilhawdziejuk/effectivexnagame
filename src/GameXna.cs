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

        /// <summary>
        /// Component ---> Sount
        /// </summary>
        private SoundManager sound;

        #endregion

        #region --- Private fields ---

        public GameObjectsManager GameObjectManager;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Texture2D texture;
        private Texture2D textureCenter;
        private Matrix heliWorld;

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

            this.sound = new SoundManager(this, "SoundManager");
            Components.Add(sound);
            //itd.
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
            this.triangle = new global::GameXna.Figures.Triangle(new Vector3(-2, 0.8f, 0), new Vector3(2, -0.8f, 0), new Vector3(-2, -0.8f, 0));
            this.rectangleRight = new global::GameXna.Figures.Rectangle(new Vector3(2.7f, -0.8f, -2), new Vector3(3.0f, 0.8f, 2));
            this.rectangleLeft = new global::GameXna.Figures.Rectangle(new Vector3(-3.0f, -0.8f, 2), new Vector3(-2.7f, 0.8f, -2));
            this.rectangleCenter = new global::GameXna.Figures.Rectangle(new Vector3(-2.7f, -0.8f, -2), new Vector3(2.7f, 0.8f, -2));

            Matrix carWorld = Matrix.CreateScale(0.0015f) * 
                Matrix.CreateRotationX(MathHelper.ToRadians(90.0f)) *
                Matrix.CreateRotationY(MathHelper.ToRadians(-90.0f)) *
                Matrix.CreateTranslation(new Vector3(0.5f, -0.5f, -0.5f));

            heliWorld = Matrix.CreateScale(0.0025f) *
                Matrix.CreateRotationY(MathHelper.ToRadians(90.0f)) *
                Matrix.CreateRotationX(MathHelper.ToRadians(90.0f)) *
                Matrix.CreateRotationZ(MathHelper.ToRadians(-90.0f)) *
                Matrix.CreateTranslation(new Vector3(0.5f, 0.2f, 0));

            this.GameObjectManager.AddGameObject(new GameObject(null, carWorld, "car"));
            this.GameObjectManager.AddGameObject(new GameObject(null, heliWorld, "heli"));
            this.GameObjectManager.ActiveObject = this.GameObjectManager.GetObject("heli");

            base.Initialize();
        }

        private void DrawGameObject(GameObject gameObject)
        {
            DrawModel(ref gameObject.Model, ref gameObject.World);
        }

        protected override void BeginRun()
        {
            base.BeginRun();

        }

        protected override bool BeginDraw()
        {
            return base.BeginDraw();
        }

        protected override void EndDraw()
        {
            base.EndDraw();
        }
        protected override void EndRun()
        {
            base.EndRun();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void UnloadGraphicsContent(bool unloadAllContent)
        {
            base.UnloadGraphicsContent(unloadAllContent);
        }

        private void DrawModel(ref Model m, ref Matrix world)
        {
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
            texture = Content.Load<Texture2D>("Textures\\zachod_slonca");
            this.textureCenter = Content.Load<Texture2D>("Textures\\zachod_slonca");
            if (loadAllContent)
            {
                // TODO: Load any ResourceManagementMode.Automatic content
                GameObject ford = this.GameObjectManager.GetObject("car");
                if (ford != null)
                {
                    ford.Model = Content.Load<Model>("Models\\Ford\\ford");                   
                }

                GameObject heli = this.GameObjectManager.GetObject("heli");
                if (heli != null)
                {
                    heli.Model = Content.Load<Model>("Models\\Helikopter\\helnwsm1");
                }
            }
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            base.OnExiting(sender, args);
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

            Keys[] keys = input.KeyboardState.GetPressedKeys();
            if (keys.Length > 0)
            {
                if (keys[0] == Keys.C)
                {
                    if (this.GameObjectManager.ActiveObject != this.GameObjectManager.GetObject("car"))
                    {
                        this.GameObjectManager.ActiveObject = this.GameObjectManager.GetObject("car");
                        this.sound.StopAll();
                    }
                }
                else if (keys[0] == Keys.H)
                {
                    if (this.GameObjectManager.ActiveObject != this.GameObjectManager.GetObject("heli"))
                    {
                        this.GameObjectManager.ActiveObject = this.GameObjectManager.GetObject("heli");
                        this.sound.StopAll();
                    }
                }
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
            GraphicsDevice.Clear(Color.DimGray);

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

            //ISROT = Identity, Scale, Rotation, Orbit, Translation

            GameObject obj = this.GameObjectManager.ActiveObject;
            
            obj.Position = this.camera.cameraPosition + new Vector3(-0.5f,-0.1f,-1);
            if (this.camera.lastCameraYaw != this.camera.cameraYaw)
            {
                obj.Rotation = this.camera.cameraYaw - this.camera.lastCameraYaw;
                obj.World *=
                    Matrix.CreateTranslation(-this.camera.cameraPosition) *
                    Matrix.CreateRotationY(MathHelper.ToRadians(1f * obj.Rotation)) *
                    Matrix.CreateTranslation(this.camera.cameraPosition);


                
            }
            if (this.camera.lastCameraPosition != this.camera.cameraPosition)
            {
                if (this.GameObjectManager.GetObject("heli") == this.GameObjectManager.ActiveObject)
                {
                    sound.Play("helis");
                }
                else
                {
                    sound.Play("car");
                }
            }

            foreach (GameObject obj0 in this.GameObjectManager.GameObjects)
            {
                DrawGameObject(obj0);
            }

            base.Draw(gameTime);
        }
    }
}
