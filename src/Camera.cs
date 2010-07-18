//18.07.2010
//gamexna
//author: Kamil Hawdziejuk

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameXna
{
    public class Camera
    {
        public Vector3 cameraPosition = new Vector3(0.0f, 0.0f, 3.0f);
        public Vector3 cameraTarget = Vector3.Zero;
        public Vector3 cameraUpVector = Vector3.Up;
    }
}
