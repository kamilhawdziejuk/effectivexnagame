using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

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
    }
}
