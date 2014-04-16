using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Shard
{
    class MuteButton : Button
    {
        public MuteButton(ShardGame gameReference) : this(gameReference, null) { }

        public MuteButton(ShardGame gameReference, MenuImage image) : base(gameReference, image) { }

        public override void PreformMouseClickAction()
        {
            GameReference.Muted = !GameReference.Muted;
            //base.PreformMouseClickAction();
        }
    }
}