using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Project03
{
    public class Camera
    {
        Vector2 position;
        Matrix viewMatrix;
        public Vector2 Position
        {
            get { return position; }
        }
        public Matrix ViewMatrix
        {
            get { return viewMatrix; }
        }

        public float ScreenWidth
        {
            get { return ScreenManager.Instance.Dimensions.X; }
        }

        public float ScreenHeight
        {
            get { return ScreenManager.Instance.Dimensions.Y; }
        }

        public void Update(Vector2 playerPostion)
        {
            position.X = playerPostion.X - (ScreenWidth / 2);
            position.Y = playerPostion.Y - (ScreenHeight / 2);

            if (position.X < 0)
                position.X = 0;
            if (position.Y < 0)
                position.Y = 0;
            if (position.X > 480)
                position.X = 480;
            if (position.Y > 800)
                position.Y = 800;

            viewMatrix = Matrix.CreateTranslation(new Vector3(-position, 0));
        }
    }
}
