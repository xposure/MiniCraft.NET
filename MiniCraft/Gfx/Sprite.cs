using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniCraft.Gfx
{
    public class Sprite
    {
        public int x, y;
        public int img;
        public int col;
        public int bits;

        public Sprite(int x, int y, int img, int col, int bits)
        {
            this.x = x;
            this.y = y;
            this.img = img;
            this.col = col;
            this.bits = bits;
        }
    }
}
