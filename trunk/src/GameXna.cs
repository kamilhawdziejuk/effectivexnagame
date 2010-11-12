//18.07.2010
//gamexna
//author: Kamil Hawdziejuk

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XELibrary;
using Primitives3D;
using GameXna.GameObjects;
using GameXna.GameStates;

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
        /// Component ---> Sound
        /// </summary>
        private SoundManager sound;

        /// <summary>
        /// Component ---> objects
        /// </summary>
        private GameObjectsManager objects;
        
        ModelBone choopBone; //smiglo cz.1
        ModelBone choopBone2; //smiglo cz.2

        #endregion

        #region --- Private fields ---

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Texture2D texture;
       // private Texture2D textureCenter;
        public Texture2D TextureIntro;
        private Matrix heliWorld;
        private float rot = 200f;
        private Matrix choopRotation;
        float total = 0f;
        private Skybox skybox;
        Model skyboxModel;
        Texture2D[] skyboxTextures;
        public SpriteFont Font;


        //private InputHandler input;
        //private Camera camera;
        public GameStateManager GameStateManager;
        public SpriteBatch SpriteBatch
        {
            get
            {
                return this.spriteBatch;
            }
        }

        public GameObjectsManager ObjectsManager
        {
            get
            {
                return this.objects;
            }
        }

       
        public ITitleIntroState TitleIntroState;
        public IStartMenuState StartMenuState;
        public IOptionsMenuState OptionsMenuState;
        public IPlayingState PlayingState;
        public IStartLevelState StartLevelState;
        public ILostGameState LostGameState;
        public IWonGameState WonGameState;
        public IFadingState FadingState;
        public IPausedState PausedState;
        public IYesNoDialogState YesNoDialogState;



       /* SkinnedSphere[] skinnedSpheres;
        BoundingSphere[] boundingSpheres;

        bool showSpheres;
        SpherePrimitive spherePrimitive;

        Model currentModel;
        AnimationPlayer animationPlayer;
        SkinningData skinningData;
        Matrix[] boneTransforms;*/

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
           // this.graphics.GraphicsDevice.

            Content.RootDirectory = "Content";

            input = new InputHandler(this);
            Components.Add(input);

            camera = new FirstPersonCamera(this);
            Components.Add(camera);

            sound = new SoundManager(this, "SoundManager");
            Components.Add(sound);

            objects = new GameObjectsManager(this);
            Components.Add(objects);
//#if DEBUG
            fps = new FPS(this);
            Components.Add(fps);
//#endif

            GameStateManager = new GameStateManager(this);
            Components.Add(GameStateManager);

            TitleIntroState = new TitleIntroState(this);
            StartMenuState = new StartMenuState(this);
            //OptionsMenuState = new OptionsMenuState(this);
            PlayingState = new PlayingState(this);
            StartLevelState = new StartLevelState(this);
            FadingState = new FadingState(this);
            //LostGameState = new LostGameState(this);
            WonGameState = new WonGameState(this);
            //PausedState = new PausedState(this);
            //YesNoDialogState = new YesNoDialogState(this);
            GameStateManager.ChangeState(TitleIntroState.Value);
        }

        #endregion

        public void ReStart()
        {
            this.objects.GameObjects.Clear();
            this.Initialize();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
           // this.triangle = new global::GameXna.Figures.Triangle(new Vector3(-2, 2.8f, 0), new Vector3(2, -0.8f, 0), new Vector3(-2, -0.8f, 0));
           // this.rectangleRight = new global::GameXna.Figures.Rectangle(new Vector3(2.7f, -0.8f, -2), new Vector3(3.0f, 2.8f, 2));
          //  this.rectangleLeft = new global::GameXna.Figures.Rectangle(new Vector3(-3.0f, -0.8f, 2), new Vector3(-2.7f, 2.8f, -2));
           // this.rectangleCenter = new global::GameXna.Figures.Rectangle(new Vector3(-2.7f, -0.8f, -2), new Vector3(2.7f, 2.8f, -2));
 
            Matrix carWorld = Matrix.CreateScale(0.0015f) * 
                Matrix.CreateRotationX(MathHelper.ToRadians(90.0f)) *
                Matrix.CreateRotationY(MathHelper.ToRadians(-90.0f)) *
                Matrix.CreateTranslation(new Vector3(0.5f, -0.5f, -0.5f));

            float height = 0.2f;
            heliWorld = Matrix.CreateScale(0.0025f) *
                Matrix.CreateRotationY(MathHelper.ToRadians(90.0f)) *
                Matrix.CreateRotationX(MathHelper.ToRadians(90.0f)) *
                Matrix.CreateRotationZ(MathHelper.ToRadians(-90.0f)) *
                Matrix.CreateTranslation(new Vector3(0.5f, height, 0));

            var car = new AIGameVehicule(this,null, carWorld, "car");

            var heli = new AIGameVehicule(this, null, heliWorld, "heli");
            heli.AIActivated = false;

            car.Scale = 0.0015f;
            heli.Scale = 0.0025f;

            objects.Add(car);
            objects.Add(heli);
            objects.ActiveObject = objects.Get("heli");

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
            
            //texture = Content.Load<Texture2D>("Textures\\wydmy");
           // this.textureCenter = Content.Load<Texture2D>("Textures\\wydmy");
            this.TextureIntro = Content.Load<Texture2D>("Textures\\intro3");

            this.Font = Content.Load<SpriteFont>("Fonts\\IntroFont");
            // TODO: Load any ResourceManagementMode.Automatic content
            var ford = objects.Get("car") as GameVehicle;
            if (ford != null)
            {
                ford.Model = Content.Load<Model>("Models\\Ford\\ford");
            }

            var heli = objects.Get("heli") as GameVehicle;
            if (heli != null)
            {
                heli.Model = Content.Load<Model>("Models\\Helikopter\\helnwsm1");
                this.choopBone = heli.Model.Bones[16]; //smiglo cz.1
                this.choopBone2 = heli.Model.Bones[18]; //smiglo cz.2
                this.choopRotation = choopBone.Transform;   
            }

            Model fireModel = Content.Load<Model>("Models\\Pociski\\Zepplin");

            heli.Bullets.ForEach(a => a.Model = fireModel);
            ford.Bullets.ForEach(a => a.Model = fireModel);

            this.skyboxModel = LoadModel("Skyboxes\\skybox", out this.skyboxTextures);

            base.LoadContent();
        }

        

        private void DrawSkybox()
        {
            GraphicsDevice device = graphics.GraphicsDevice;
            device.SamplerStates[0].AddressU = TextureAddressMode.Clamp;
            device.SamplerStates[0].AddressV = TextureAddressMode.Clamp;

            device.RenderState.DepthBufferWriteEnable = false;
            Matrix[] skyboxTransforms = new Matrix[skyboxModel.Bones.Count];
            skyboxModel.CopyAbsoluteBoneTransformsTo(skyboxTransforms);

            foreach (ModelMesh mesh in skyboxModel.Meshes)
            {
                foreach (Effect currentEffect in mesh.Effects)
                {
                    Matrix worldMatrix = skyboxTransforms[mesh.ParentBone.Index] *
                          Matrix.CreateTranslation(29 * Vector3.UnitY);
                    currentEffect.Parameters["World"].SetValue(worldMatrix);
                    currentEffect.Parameters["View"].SetValue(this.camera.View);
                    currentEffect.Parameters["Projection"].SetValue(this.camera.Projection);
                }
                mesh.Draw();
            }
            device.RenderState.DepthBufferWriteEnable = true;
        }

        private Model LoadModel(string assetName, out Texture2D[] textures)
        {

            Model newModel = Content.Load<Model>(assetName);
            textures = new Texture2D[newModel.Meshes.Count];
            int i = 0;
            foreach (ModelMesh mesh in newModel.Meshes)
                foreach (BasicEffect currentEffect in mesh.Effects)
                    textures[i++] = currentEffect.Texture;
            return newModel;
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
                    if (objects.ActiveObject != objects.Get("car"))
                    {
                        objects.ActiveObject = objects.Get("car");
                        this.sound.StopAll();
                    }
                }
                else if (keys[0] == Keys.H)
                {
                    if (objects.ActiveObject != objects.Get("heli"))
                    {
                        objects.ActiveObject = objects.Get("heli");
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
            //this.SpriteBatch.Begin();

            graphics.GraphicsDevice.RenderState.CullMode = CullMode.None;
            GraphicsDevice.Clear(Color.DimGray);

            if (this.GameStateManager.State is StartLevelState)
            {
                graphics.GraphicsDevice.VertexDeclaration = new
                VertexDeclaration(graphics.GraphicsDevice,
                VertexPositionNormalTexture.VertexElements);
                graphics.GraphicsDevice.RenderState.DepthBufferEnable = true;
                graphics.GraphicsDevice.RenderState.AlphaBlendEnable = false;
                graphics.GraphicsDevice.RenderState.AlphaTestEnable = false;

               // GraphicsDevice.SamplerStates[0].AddressU = TextureAddressMode.Wrap;
               // GraphicsDevice.SamplerStates[0].AddressV = TextureAddressMode.Wrap;


                ////ISROT = Identity, Scale, Rotation, Orbit, Translation
                GameObject obj = objects.ActiveObject;

                rot += 5.1f;

                obj.Position = this.camera.cameraPosition + new Vector3(-0.5f, -0.1f, -1); //ustawiam aktywny wehiku³ w danym miejscu (tak, aby by³ widoczny z widoku kamery)
                Vector3 lastPosition = obj.Position;
                Matrix yaw = Matrix.Identity;
                Matrix pitch = Matrix.Identity;
                Vector3 forward = obj.World0.Forward;
                if (this.camera.lastCameraYaw != this.camera.cameraYaw)
                {
                    float rotation = this.camera.cameraYaw - this.camera.lastCameraYaw;
                    yaw = Matrix.CreateTranslation(-this.camera.cameraPosition) *
                        Matrix.CreateRotationY(MathHelper.ToRadians(rotation)) *
                        Matrix.CreateTranslation(this.camera.cameraPosition);
                    total += obj.Rotation;
                    obj.Rotation += rotation;
                    obj.World *= yaw;
                    obj.World0 *= yaw;
                }
                if (objects.Get("heli") == objects.ActiveObject)
                {
                    //rotating the choop, with a correction include
                    float alfa = 360 - objects.ActiveObject.Rotation;
                    Vector3 poprawka = 0.11f * new Vector3((float)Math.Sin(MathHelper.ToRadians(alfa)), 0, -(float)Math.Cos(MathHelper.ToRadians(alfa)));
                    Vector3 move = objects.ActiveObject.Position2 + poprawka;

                    this.choopBone.Transform =
                        Matrix.CreateTranslation(-move) *
                         Matrix.CreateRotationY(rot) *
                        Matrix.CreateTranslation(move);

                }
                this.choopBone2.Transform = this.choopBone.Transform;
                //sound
                this.sound.Update(gameTime);
                if (this.camera.lastCameraPosition != this.camera.cameraPosition)
                {
                    if (objects.Get("heli") == objects.ActiveObject)
                    {
                        sound.Play("helis");
                    }
                    else
                    {
                        sound.Play("car");
                    }
                }

                //this.Window.Title = "Position= " + objects.ActiveObject.Position + "  Position2=" + objects.ActiveObject.Position2 + " X=" + X + "  Z=" + Z + "  Total=" + (360-objects.ActiveObject.Rotation);// this.choopBone.Transform.Translation;
                //this.Window.Title = "Arms counter=" + (objects.ActiveObject as GameVehicle).Bullets.FindAll(a => a.State == BulletState.Prepared).Count;
                this.Window.Title = "Gra \"WEHIKULOWE STARCIE\"          FPS:" + this.fps.ToString();

                objects.Draw(gameTime);
                this.DrawSkybox();
            }
            base.Draw(gameTime);

           // this.SpriteBatch.End();
        }
    }
}
