//Kamil Hawdziejuk
//24-07-2010

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
        protected IInputHandler input;
        private GraphicsDeviceManager graphics;

        public Vector3 cameraPosition = new Vector3(0.0f, 0.0f, 3.0f);
        public Vector3 lastCameraPosition;
        public Vector3 cameraTarget = Vector3.Zero;
        public Vector3 cameraUpVector = Vector3.Up;
        private Matrix projection;
        private Matrix view;

        public float cameraYaw = 0.0f;
        public float lastCameraYaw = 0.0f;
        public float cameraPitch = 0.0f;
        protected Vector3 movement = Vector3.Zero;

        private const float spinRate = 90.0f;
        private const float moveRate = 3.0f;

        private Vector3 cameraReferance = new Vector3(0.0f, 0.0f, -1.0f);

        #region --- Constructing & destroying objects ---

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"></param>
        public Camera(Game game) : base(game)
        {
            graphics = (GraphicsDeviceManager)Game.Services.GetService(typeof(IGraphicsDeviceManager));
            input = (IInputHandler)Game.Services.GetService(typeof(IInputHandler));
        }

        #endregion

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
            float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.lastCameraYaw = cameraYaw;
#if !XBOX360
            if ((input.PreviousMouseState.X > input.MouseState.X) &&
            (input.MouseState.LeftButton == ButtonState.Pressed))
            {
                cameraYaw += (spinRate * timeDelta);
            }
            else if ((input.PreviousMouseState.X < input.MouseState.X) &&
            (input.MouseState.LeftButton == ButtonState.Pressed))
            {
                cameraYaw -= (spinRate * timeDelta);
            }

            if ((input.PreviousMouseState.Y > input.MouseState.Y) &&
                (input.MouseState.LeftButton == ButtonState.Pressed))
            {
                cameraPitch += (spinRate * timeDelta);
            }
            else if ((input.PreviousMouseState.Y < input.MouseState.Y) &&
                (input.MouseState.LeftButton == ButtonState.Pressed))
            {
                cameraPitch -= (spinRate * timeDelta);
            }
#endif

            if (input.KeyboardState.IsKeyDown(Keys.Left))
            {
                cameraYaw += (spinRate * timeDelta);
            }
            if (input.KeyboardState.IsKeyDown(Keys.Right))
            {
                cameraYaw -= (spinRate * timeDelta);
            }
            if (input.KeyboardState.IsKeyDown(Keys.Up))
            {
                cameraPitch += (spinRate * timeDelta);
            }
            if (input.KeyboardState.IsKeyDown(Keys.Down))
            {
                cameraPitch -= (spinRate * timeDelta);
            }

            if (cameraYaw > 360)
            {
                cameraYaw -= 360;
            }
            else if (cameraYaw < 0)
            {
                cameraYaw += 360;
            }

            if (cameraPitch > 360)
            {
                cameraPitch -= 360;
            }
            else if (cameraPitch < 0)
            {
                cameraPitch += 360;
            }


            //update movement (none for this base class)
            movement *= (moveRate * timeDelta);
            Matrix rotationMatrix;
            Vector3 transformedReference;
            Matrix.CreateRotationY(MathHelper.ToRadians(cameraYaw), out rotationMatrix);
            lastCameraPosition = cameraPosition;
            if (movement != Vector3.Zero)
            {
                Vector3.Transform(ref movement, ref rotationMatrix, out movement);
                
                cameraPosition += movement;
                movement = Vector3.Zero;
            }
            //add in pitch to the rotation
            rotationMatrix = Matrix.CreateRotationX(MathHelper.ToRadians(cameraPitch)) * rotationMatrix;
            Vector3.Transform(ref cameraReferance, ref rotationMatrix, out transformedReference);

            //Calculate the position the camera is looking at
            Vector3.Add(ref cameraPosition, ref transformedReference, out cameraTarget);

            float aspectRatio = (float)graphics.GraphicsDevice.Viewport.Width / (float)graphics.GraphicsDevice.Viewport.Height;
            Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 0.0001f, 1000.0f, out this.projection);
            Matrix.CreateLookAt(ref this.cameraPosition, ref this.cameraTarget, ref this.cameraUpVector, out this.view);

            base.Update(gameTime);
        }
    }
}
