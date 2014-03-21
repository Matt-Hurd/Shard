using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Shard
{
    public class Camera
    {
        private Vector2 offset;
        private Vector3 position;

        private int screenWidth, screenHeight;

        private Matrix viewMatrix;

        private float zoom;

        private bool isZooming;
        private float zoomStart, zoomA, zoomB;
        private int zoomValue;

        public Camera(float OffsetX, float OffsetY)
        {
            offset = new Vector2(OffsetX, OffsetY);
            screenWidth = 1280;
            screenHeight = 720;
            zoom = 1;
            zoomA = zoom;
            zoomB = zoom;
            BuildViewMatrix();
        }

        #region Modifying Fields

        public virtual int ScreenWidth
        {
            get
            {
                return this.screenWidth;
            }
            set
            {
                if (value >= 0)
                    this.screenWidth = value;
                else
                    this.screenWidth = 0;
            }
        }

        public virtual int ScreenHeight
        {
            get
            {
                return this.screenHeight;
            }
            set
            {
                if (value >= 0)
                    this.screenHeight = value;
                else
                    this.screenHeight = 0;
            }
        }

        #endregion

        public void AddPosition(float X, float Y, float Z)
        {
            position += new Vector3(X, Y, Z);
            BuildViewMatrix();
        }

        public void SetPosition(float X, float Y, float Z)
        {
            position = new Vector3(X + offset.X, Y + offset.X, Z);
            BuildViewMatrix();
        }

        public bool ScreenContains(Rectangle bounds)
        {
            return bounds.Intersects(Screen);
        }

        public virtual Rectangle Screen
        {
            get
            {
                return new Rectangle((int)(Position.X - ScreenWidth / 2), (int)(Position.Y - ScreenHeight / 2), ScreenWidth, ScreenHeight);
            }
            set
            {
                if (value != null)
                {
                    position.X = value.X;
                    position.Y = value.Y;
                    screenWidth = value.Width;
                    ScreenHeight = value.Height;
                }
            }
        }

        public virtual Vector3 Position
        {
            get
            {
                return position;
            }
            set
            {
                if (value != null)
                {
                    position = value;
                    BuildViewMatrix();
                }
            }
        }

        public virtual float Zoom
        {
            get
            {
                return zoom;
            }
            set
            {
                zoom = value;
                BuildViewMatrix();
            }
        }

        public Matrix GetViewMatrix()
        {
            return viewMatrix;
        }

        public void PreformZoom(float Delta)
        {
            isZooming = true;
            zoomStart = (float)DateTime.Now.TimeOfDay.TotalSeconds;
            zoomA = zoom;
            zoomValue += (int)Delta;
            zoomValue = (int)MathHelper.Clamp(zoomValue, 5, 10);
            zoomB = (float)Math.Pow(2, zoomValue);
        }

        public void Think(float Delta)
        {
            if (isZooming)
            {
                // Calculate progress
                float t = (((float)DateTime.Now.TimeOfDay.TotalSeconds) - zoomStart) / 0.25f;

                // Are we done?
                if (t >= 1.0f)
                {
                    isZooming = false;
                    t = 1.0f;
                }
                else
                {
                    // Update zoom value
                    Zoom = (MathHelper.SmoothStep(zoomA, zoomB, t));
                }
            }
        }

        private void BuildViewMatrix()
        {
            // Identity transform
            Matrix tmp = Matrix.Identity;

            // Center camera about camera position
            float uX = screenWidth / Zoom;
            float uY = screenHeight / Zoom;

            tmp = Matrix.Multiply(Matrix.CreateScale(zoom), tmp);

            tmp = Matrix.Multiply(Matrix.CreateTranslation((uX * 0.5f) - position.X, (uY * 0.5f) - position.Y, 0), tmp);

            // Done
            viewMatrix = tmp;
        }
    }
}
