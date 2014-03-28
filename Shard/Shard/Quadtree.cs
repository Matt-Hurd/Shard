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

    /* Quadtree Code Implementation is taken and modified from an article titled "Quick Tip: Use Quadtrees to Detect Likely Collisions in 2D Space" by Steven Lambert
     * Published: 3 Sep 2012
     * URL: http://gamedevelopment.tutsplus.com/tutorials/quick-tip-use-quadtrees-to-detect-likely-collisions-in-2d-space--gamedev-374 
     */

    class Quadtree
    {

        private int MAX_OBJECTS = 10;
        private int MAX_LEVELS = 5;

        private int level;
        private List<ShardObject> objects;
        private Rectangle bounds;
        private Quadtree[] nodes;

        /*
         * Constructor
         */
        public Quadtree(int pLevel, Rectangle pBounds)
        {
            level = pLevel;
            objects = new List<ShardObject>();
            bounds = pBounds;
            nodes = new Quadtree[4];
        }

        /*
         * Clears the quadtree
         */
        public void Clear()
        {
            objects.Clear();

            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i] != null)
                {
                    nodes[i].Clear();
                    nodes[i] = null;
                }
            }
        }

        /*
         * Splits the node into 4 subnodes
         */
        private void Split()
        {
            int subWidth = (int)(bounds.Width / 2);
            int subHeight = (int)(bounds.Height / 2);
            int x = (int)bounds.X;
            int y = (int)bounds.Y;

            nodes[0] = new Quadtree(level + 1, new Rectangle(x + subWidth, y, subWidth, subHeight));
            nodes[1] = new Quadtree(level + 1, new Rectangle(x, y, subWidth, subHeight));
            nodes[2] = new Quadtree(level + 1, new Rectangle(x, y + subHeight, subWidth, subHeight));
            nodes[3] = new Quadtree(level + 1, new Rectangle(x + subWidth, y + subHeight, subWidth, subHeight));
        }

        /*
         * Determine which node the object belongs to. -1 means
         * object cannot completely fit within a child node and is part
         * of the parent node
         */
        private int GetIndex(ShardObject shardObject)
        {
            int index = -1;
            double verticalMidpoint = bounds.X + (bounds.Width / 2);
            double horizontalMidpoint = bounds.Y + (bounds.Height / 2);

            // Object can completely fit within the top quadrants
            bool topQuadrant = (shardObject.Y < horizontalMidpoint && shardObject.Y + shardObject.Height < horizontalMidpoint);
            // Object can completely fit within the bottom quadrants
            bool bottomQuadrant = (shardObject.Y > horizontalMidpoint);

            // Object can completely fit within the left quadrants
            if (shardObject.X < verticalMidpoint && shardObject.X + shardObject.Width < verticalMidpoint)
            {
                if (topQuadrant)
                {
                    index = 1;
                }
                else if (bottomQuadrant)
                {
                    index = 2;
                }
            }
            // Object can completely fit within the right quadrants
            else if (shardObject.X > verticalMidpoint)
            {
                if (topQuadrant)
                {
                    index = 0;
                }
                else if (bottomQuadrant)
                {
                    index = 3;
                }
            }

            return index;
        }

        /*
         * Insert the object into the quadtree. If the node
         * exceeds the capacity, it will split and add all
         * objects to their corresponding nodes.
         */
        public void Insert(ShardObject shardObject)
        {
            if (nodes[0] != null)
            {
                int index = GetIndex(shardObject);

                if (index != -1)
                {
                    nodes[index].Insert(shardObject);

                    return;
                }
            }

            objects.Add(shardObject);

            if (objects.Count > MAX_OBJECTS && level < MAX_LEVELS)
            {
                if (nodes[0] == null)
                {
                    Split();
                }

                int i = 0;
                while (i < objects.Count)
                {
                    int index = GetIndex(objects[i]);
                    if (index != -1)
                    {
                        nodes[index].Insert(objects[i]);
                        objects.RemoveAt(i);
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }

        /*
         * Return all objects that could collide with the given object
         */
        public List<ShardObject> Retrieve(List<ShardObject> returnObjects, ShardObject shardObject)
        {
            int index = GetIndex(shardObject);
            if (index != -1 && nodes[0] != null)
            {
                nodes[index].Retrieve(returnObjects, shardObject);
            }

            foreach (ShardObject so in objects)
            {
                if(!so.Equals(shardObject))
                    returnObjects.Add(so);
            }

            return returnObjects;
        }
    }
}
