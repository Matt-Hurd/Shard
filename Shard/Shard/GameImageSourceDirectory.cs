using System;
using System.IO;
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
    class GameImageSourceDirectory
    {
        private Dictionary<string, Rectangle> sourceRectangles;

        public GameImageSourceDirectory()
        {
            sourceRectangles = new Dictionary<string, Rectangle>();
        }

        public Rectangle GetSourceRectangle(string imageName)
        {
            try
            {
                return sourceRectangles[imageName];
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new Rectangle(0,0,1,1);
            }
        }

        #region Loading Images from Spritesheet

        public bool LoadSourcesFromFile(string directoryFileName)
        {

            //imagesheet = spritesheet;
            List<string> lines = new List<string>();

            try
            {
                using (StreamReader reader = new StreamReader(directoryFileName))
                {
                    while (!reader.EndOfStream)
                    {
                        lines.Add(reader.ReadLine());
                    }
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine("The Spritesheet Directory file could not be read.");
                System.Console.WriteLine(e.Message);
                return false;
            }

            foreach (String line in lines)
            {
                string[] parts = line.Split('=');
                string name = parts[0].Trim();
                string[] rectValues = parts[1].Trim().Split(' ');
                Rectangle sourceRect = new Rectangle(Convert.ToInt32(rectValues[0]), Convert.ToInt32(rectValues[1]), Convert.ToInt32(rectValues[2]), Convert.ToInt32(rectValues[3]));
                sourceRectangles.Add(name, sourceRect);
            }

            return true;
        }

        #endregion
    }
}
