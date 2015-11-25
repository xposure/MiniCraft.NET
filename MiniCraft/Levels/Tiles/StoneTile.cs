using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniCraft.Entities;
using MiniCraft.Gfx;

namespace MiniCraft.Levels.Tiles
{

    public class StoneTile : Tile
    {
        public StoneTile(int id)
            : base(id)
        {
        }

        public override void render(Screen screen, Level level, int x, int y)
        {
            int rc1 = 111;
            int rc2 = 333;
            int rc3 = 555;
            screen.render(x * 16 + 0, y * 16 + 0, 32, ColorHelper.get(rc1, level.dirtColor, rc2, rc3), 0);
            screen.render(x * 16 + 8, y * 16 + 0, 32, ColorHelper.get(rc1, level.dirtColor, rc2, rc3), 0);
            screen.render(x * 16 + 0, y * 16 + 8, 32, ColorHelper.get(rc1, level.dirtColor, rc2, rc3), 0);
            screen.render(x * 16 + 8, y * 16 + 8, 32, ColorHelper.get(rc1, level.dirtColor, rc2, rc3), 0);
        }

        public override bool mayPass(Level level, int x, int y, Entity e)
        {
            return false;
        }

    }
}
