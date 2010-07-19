//Kamil Hawdziejuk
//19-07-2010

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameXna.Figures
{
    public class Triangle : VerticesIndicesFigure
    {
        public Vector3 Position1
        {
            get
            {
                return this.vertices[0].Position;
            }
        }
        public Vector3 Position2
        {
            get
            {
                return this.vertices[1].Position;
            }
        }
        public Vector3 Position3
        {
            get
            {
                return this.vertices[2].Position;
            }
        }

        #region --- Creating & destroying objects ---

        /// <summary>
        /// Creates a triangle
        /// </summary>
        /// <param name="position1"></param>
        /// <param name="position2"></param>
        /// <param name="position3"></param>
        public Triangle(Vector3 position1, Vector3 position2, Vector3 position3)
        {
            this.vertices = new Microsoft.Xna.Framework.Graphics.VertexPositionNormalTexture[3];
            Vector2 textureCoordinates;
            //top left
            textureCoordinates = new Vector2(0, 0);
            vertices[0] = new VertexPositionNormalTexture(position1, Vector3.Forward, textureCoordinates);
            //bottom right
            textureCoordinates = new Vector2(1, 1);
            vertices[1] = new VertexPositionNormalTexture(position2, Vector3.Forward, textureCoordinates);
            //bottom left
            textureCoordinates = new Vector2(0, 1);
            vertices[2] = new VertexPositionNormalTexture(position3, Vector3.Forward, textureCoordinates);
        }

        #endregion

        #region --- Draw ---

        /// <summary>
        /// Draws the triangle
        /// </summary>
        /// <param name="_device"></param>
        public override void Draw(Microsoft.Xna.Framework.Graphics.GraphicsDevice _device)
        {
            _device.DrawUserPrimitives(PrimitiveType.TriangleList, vertices, 0, vertices.Length / 3);
        }

        #endregion
    }
}
