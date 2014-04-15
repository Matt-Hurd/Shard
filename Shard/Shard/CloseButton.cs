using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shard
{
    class CloseButton : Button
    {
        public CloseButton(ShardGame gameReference) : this(gameReference, null) { }

        public CloseButton(ShardGame gameReference, MenuImage image) : base(gameReference, image) { }
        
        public override void PreformMouseClickAction()
        {
            MenuReference.Active = false;
            //base.PreformMouseClickAction();
        }
    }
}
