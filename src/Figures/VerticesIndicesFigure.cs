//Kamil Hawdziejuk
//19-07-2010

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace GameXna.Figures
{
    public abstract class VerticesIndicesFigure
    {
        public VertexPositionNormalTexture[] vertices;
        public short[] indices;

        public abstract void Draw(GraphicsDevice _device);
    }
}
