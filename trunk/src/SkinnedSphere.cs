using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace GameXna
{
    /// <summary>
    /// Stores a bounding sphere for checking collision against a skinned
    /// model, and records which bone this sphere is attached to.
    /// </summary>
    public class SkinnedSphere
    {
        public string BoneName;
        public float Radius;

        [ContentSerializer(Optional = true)]
        public float Offset;
    }
}
