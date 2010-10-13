using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameXna
{
    public class SphereCollisionDetector : ICollisionDetector
    {
        #region ICollisionDetector Members

        public bool DetectCollision(GameObject obj1, GameObject obj2, double tolerance)
        {
            if (obj1.Model == null || obj2.Model == null)
            {
                return false;
            }

            foreach (ModelMesh mesh1 in obj1.Model.Meshes)
            {
                foreach (ModelMesh mesh2 in obj2.Model.Meshes)
                {
                    BoundingSphere bs1 = mesh1.BoundingSphere;
                    bs1.Center = obj1.Position2;
                    bs1.Radius *= 0.8f * obj1.Scale;
                   
                    BoundingSphere bs2 = mesh2.BoundingSphere;
                    bs2.Center = obj2.Position2;
                    bs2.Radius *= 0.8f * obj2.Scale;

                    if (bs1.Intersects(bs2))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public string Name
        {
            get { return "SphereCollisionDetector"; }
        }

        #endregion
    }
}
