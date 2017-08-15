using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TerrariaStyleWorld
{
    class Camera
    {
        private Vector2 mPosition;
        private Point mViewportSize;
        private float mZoom;

        private Camera() { }

        public Camera(Vector2 position, Point viewportBounds, float zoom)
        {
            mPosition = position;
            mViewportSize = viewportBounds;
            mZoom = zoom;
        }

        public Matrix getView()
        {
            Matrix view = Matrix.Identity;
            view *= Matrix.CreateTranslation(new Vector3(-mPosition.X, mPosition.Y, 0.0f));
            view *= Matrix.CreateScale(mZoom);
            view *= Matrix.CreateTranslation(mViewportSize.X / 2, mViewportSize.Y / 2, 0.0f);
            return view;
        }

        public void OnResize(Point viewportSize)
        {
            mViewportSize = viewportSize;
        }

        public void Move(Vector2 direction, float speed)
        {
            mPosition += direction * speed;
        }

        public Rectangle getBounds(Rectangle viewportBounds)// returns the bounds of the camera in world space
        {
            Rectangle bounds;
            
            int width = viewportBounds.Width;
            int height = -viewportBounds.Height;

            Point centre = mPosition.ToPoint();

            Point size = new Point((int)(width / mZoom), (int)(height / mZoom));
            Point location = centre - size / new Point(2);

            bounds = new Rectangle(location, size);

            return bounds;
        }
    }
}
