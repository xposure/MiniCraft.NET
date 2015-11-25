using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniCraft.Gfx;

namespace MiniCraft.Entities
{
    public class Lantern : Furniture
    {
        public Lantern()
            : base("Lantern")
        {
            col = ColorHelper.get(-1, 000, 111, 555);
            sprite = 5;
            xr = 3;
            yr = 2;
        }

        public override int getLightRadius()
        {
            return 8;
        }
    }
}
