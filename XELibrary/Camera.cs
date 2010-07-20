using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace XELibrary
{
    public class Camera : GameComponent
    {
        private IInputHandler input;
        private GraphicsDeviceManager graphics;

        public Vector3 cameraPosition = new Vector3(0.0f, 0.0f, 3.0f);
        public Vector3 cameraTarget = Vector3.Zero;
        public Vector3 cameraUpVector = Vector3.Up;
        private Matrix projection;
        private Matrix view;

        private float cameraYaw = 0.0f;
        private const float spinRate = 2.0f;

        private Vector3 cameraReferance = new Vector3(0.0f, 0.0f, -1.0f);

        public Camera(Game game) : base(game)
        {
            graphics = (GraphicsDeviceManager)Game.Services.GetService(typeof(IGraphicsDeviceManager));
            input = (IInputHandler)Game.Services.GetService(typeof(IInputHandler));
        }

        public Matrix View
        {
            get { return this.view; }
        }

        public Matrix Projection
        {
            get { return this.projection; }
        }

        public override void Initialize()
        {
            base.Initialize();
            this.InitializeCamera();
        }

        private void InitializeCamera()
        {
            float aspectRatio = (float)graphics.GraphicsDevice.Viewport.Width / (float)graphics.GraphicsDevice.Viewport.Height;
            Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 0.0001f, 1000.0f, out this.projection);
            Matrix.CreateLookAt(ref this.cameraPosition, ref this.cameraTarget, ref this.cameraUpVector, out this.view);
        }

        public override void  Update(GameTime gameTime)
        {
            if (input.KeyboardState.IsKeyDown(Keys.Left))
            {
                cameraYaw += spinRate;
            }
            if (input.KeyboardState.IsKeyDown(Keys.Right))
            {
                cameraYaw -= spinRate;
            }

            if (cameraYaw > 360)
            {
                cameraYaw -= 360;
            }
            else if (cameraYaw < 0)
            {
                cameraYaw += 360;
            }
 	        base.Update(gameTime);

            Matrix rotationMatrix;
            Matrix.CreateRotationY(MathHelper.ToRadians(cameraYaw), out rotationMatrix);
            //create a vector poinint the direction the camera is facing
            Vector3 transformedReference;
            Vector3.Transform(ref cameraReferance, ref rotationMatrix, out transformedReference);

            //Calculate the position the camera is looking at
            Vector3.Add(ref cameraPosition, ref transformedReference, out cameraTarget);

            float aspectRatio = (float)graphics.GraphicsDevice.Viewport.Width / (float)graphics.GraphicsDevice.Viewport.Height;
            Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 0.0001f, 1000.0f, out this.projection);
            Matrix.CreateLookAt(ref this.cameraPosition, ref this.cameraTarget, ref this.cameraUpVector, out this.view);
        }
    }
}
