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

namespace Player
{
    class Player : GameObject
    {
        Rectangle playerRect;
        double health;
        public Player()
        {
            health = getHealth();
            setX(0);
            setY(0);
            setVelocity(0);
            playerRect = new Rectangle(getX(), getY(), 50, 50);
        }

        public override void move()
        {
            double dir = getDirection();
            double vel = getVelocity();
            base.move();



        }

        public override void update()
        {
            move();
        }

        public override void draw(SpriteBatch spritebatch, Texture2D spritesheet)
        {

        }


        public override Rectangle getBounds()
        {
            Rectangle b;

            return b;
        }

        public void attack(int damage, GameObject obj) //GameObject should have a takeDamage method
        {
            obj.takeDamage(damage);
        }

        public override void takeDamage(int value) //GameObject should have a takeDamage method
        {
            hp -= value;
        }
    }
}
