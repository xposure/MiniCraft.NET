using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniCraft.Entities;
using MiniCraft.Gfx;

namespace MiniCraft.Levels.Tiles
{
    public class InfiniteFallTile : Tile
    {
        public InfiniteFallTile(TileID id)
            : base(id)
        {
        }

        public override void render(Screen screen, Level level, int x, int y)
        {
        }

        public override void tick(Level level, int xt, int yt)
        {
        }

        public override bool mayPass(Level level, int x, int y, Entity e)
        {
            if (e is AirWizard) return true;
            return false;
        }
    }
}
