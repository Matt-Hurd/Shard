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
    class RealisticSpaceMovementToggleButton : Button
    {
        public RealisticSpaceMovementToggleButton(ShardGame gameReference) : this(gameReference, null) { }

        public RealisticSpaceMovementToggleButton(ShardGame gameReference, MenuImage image) : base(gameReference, image) { }

        public override void PreformMouseClickAction()
        {
            GameReference.RealisticSpaceMovement = !GameReference.RealisticSpaceMovement;
            //base.PreformMouseClickAction();
        }
    }
}