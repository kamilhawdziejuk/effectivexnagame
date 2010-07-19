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
    public class Rectangle : VerticesIndicesFigure
    {
        public Vector3 DownLeft
        {
            get
            {
                return this.vertices[2].Position;
            }
        }
        public Vector3 UpRight
        {
            get
            {
                return this.vertices[3].Position;
            }
        }

        #region --- Creating & destroying objects ---

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="downLeft"></param>
        /// <param name="upRight"></param>
        public Rectangle(Vector3 downLeft, Vector3 upRight)
        {
            this.vertices = new VertexPositionNormalTexture[4];
            Vector2 textureCoordinates;
            //top left
            textureCoordinates = new Vector2(0, 0);
            vertices[0] = new VertexPositionNormalTexture(new Vector3(downLeft.X, upRight.Y, downLeft.Z), Vector3.Forward, textureCoordinates);
            //bottom right
            textureCoordinates = new Vector2(1, 1);
            vertices[1] = new VertexPositionNormalTexture(new Vector3(upRight.X, downLeft.Y, upRight.Z), Vector3.Forward, textureCoordinates);
            //bottom left
            textureCoordinates = new Vector2(0, 1);
            vertices[2] = new VertexPositionNormalTexture(downLeft, Vector3.Forward, textureCoordinates);
            //top right
            textureCoordinates = new Vector2(1, 0);
            vertices[3] = new VertexPositionNormalTexture(upRight, Vector3.Forward, textureCoordinates);


            indices = new short[6];
            //triangle 1 (bottom portion)
            indices[0] = 0; // top left
            indices[1] = 1; // bottom right
            indices[2] = 2; // bottom left
            //triangle 2 (top portion)
            indices[3] = 0; // top left
            indices[4] = 3; // top right
            indices[5] = 1; // bottom right
        }

        #endregion

        #region --- Draw ---

        /// <summary>
        /// Draws the triangle
        /// </summary>
        /// <param name="_device"></param>
        public override void Draw(Microsoft.Xna.Framework.Graphics.GraphicsDevice _device)
        {
            _device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, vertices, 0, vertices.Length, indices, 0, indices.Length / 3);
        }

        #endregion
    }
}
