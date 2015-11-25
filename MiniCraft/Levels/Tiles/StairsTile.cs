using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniCraft.Gfx;

namespace MiniCraft.Levels.Tiles
{
    public class StairsTile : Tile
    {
        private bool leadsUp;

        public StairsTile(int id, bool leadsUp)
            : base(id)
        {
            this.leadsUp = leadsUp;
        }

        public override void render(Screen screen, Level level, int x, int y)
        {
            int color = ColorHelper.get(level.dirtColor, 000, 333, 444);
            int xt = 0;
            if (leadsUp) xt = 2;
            screen.render(x * 16 + 0, y * 16 + 0, xt + 2 * 32, color, 0);
            screen.render(x * 16 + 8, y * 16 + 0, xt + 1 + 2 * 32, color, 0);
            screen.render(x * 16 + 0, y * 16 + 8, xt + 3 * 32, color, 0);
            screen.render(x * 16 + 8, y * 16 + 8, xt + 1 + 3 * 32, color, 0);
        }
    }
}
