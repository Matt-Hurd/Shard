using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shard
{
    public class Camera
    {
        private Vector2 _Off;
        private Vector3 _Pos;

        private Matrix _ViewMatrix;

        private float _Zoom;

        private bool Zooming;
        private float ZoomStart, ZoomA, ZoomB;
        private int ZoomVal;

        public Camera(float OffsetX, float OffsetY)
        {
            _Off = new Vector2(OffsetX, OffsetY);
            _Zoom = 1;
            ZoomA = _Zoom;
            ZoomB = _Zoom;
            BuildViewMatrix();
        }

        public void AddPos(float X, float Y, float Z)
        {
            _Pos += new Vector3(X, Y, Z);
            BuildViewMatrix();
        }

        public void SetPos(float X, float Y, float Z)
        {
            _Pos = new Vector3(X, Y, Z);
            BuildViewMatrix();
        }

        public void SetZoom(float Zoom)
        {
            _Zoom = Zoom;
            BuildViewMatrix();
        }

        public Matrix GetViewMatrix()
        {
            return _ViewMatrix;
        }

        public float GetZoom()
        {
            return _Zoom;
        }

        public Vector3 GetPos()
        {
            return _Pos;
        }

        public void PreformZoom(float Delta)
        {
            Zooming = true;
            ZoomStart = (float)DateTime.Now.TimeOfDay.TotalSeconds;
            ZoomA = _Zoom;
            ZoomVal += (int)Delta;
            ZoomVal = (int)MathHelper.Clamp(ZoomVal, 5, 10);
            ZoomB = (float)Math.Pow(2, ZoomVal);
        }

        public void Think(float Delta)
        {
            if (Zooming)
            {
                // Calculate progress
                float t = (((float)DateTime.Now.TimeOfDay.TotalSeconds) - ZoomStart) / 0.25f;

                // Are we done?
                if (t >= 1.0f)
                {
                    Zooming = false;
                    t = 1.0f;
                }
                else
                {
                    // Update zoom value
                    SetZoom(MathHelper.SmoothStep(ZoomA, ZoomB, t));
                }
            }
        }

        private void BuildViewMatrix()
        {
            // Identity transform
            Matrix tmp = Matrix.Identity;

            // Center camera about camera position
            float uX = 1280 / _Zoom;
            float uY = 720 / _Zoom;

            tmp = Matrix.Multiply(Matrix.CreateScale(_Zoom), tmp);

            tmp = Matrix.Multiply(Matrix.CreateTranslation((uX * 0.5f) - _Pos.X, (uY * 0.5f) - _Pos.Y, 0), tmp);

            // Done
            _ViewMatrix = tmp;
        }
    }
}
