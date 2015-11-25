﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniCraft.Entities;
using MiniCraft.Gfx;

namespace MiniCraft.Levels.Tiles
{

    public class LavaTile : Tile
    {
        public LavaTile(int id)
            : base(id)
        {
            connectsToSand = true;
            connectsToLava = true;
        }

        private Random wRandom = new Random();

        public override void render(Screen screen, Level level, int x, int y)
        {
            wRandom = new Random((int)((tickCount + (x / 2 - y) * 4311) / 10 * 54687121L + x * 3271612L + y * 3412987161L));
            //TODO: wRandom.setSeed((tickCount + (x / 2 - y) * 4311) / 10 * 54687121L + x * 3271612L + y * 3412987161L);
            int col = ColorHelper.get(500, 500, 520, 550);
            int transitionColor1 = ColorHelper.get(3, 500, level.dirtColor - 111, level.dirtColor);
            int transitionColor2 = ColorHelper.get(3, 500, level.sandColor - 110, level.sandColor);

            bool u = !level.getTile(x, y - 1).connectsToLava;
            bool d = !level.getTile(x, y + 1).connectsToLava;
            bool l = !level.getTile(x - 1, y).connectsToLava;
            bool r = !level.getTile(x + 1, y).connectsToLava;

            bool su = u && level.getTile(x, y - 1).connectsToSand;
            bool sd = d && level.getTile(x, y + 1).connectsToSand;
            bool sl = l && level.getTile(x - 1, y).connectsToSand;
            bool sr = r && level.getTile(x + 1, y).connectsToSand;

            if (!u && !l)
            {
                screen.render(x * 16 + 0, y * 16 + 0, wRandom.nextInt(4), col, wRandom.nextInt(4));
            }
            else
                screen.render(x * 16 + 0, y * 16 + 0, (l ? 14 : 15) + (u ? 0 : 1) * 32, (su || sl) ? transitionColor2 : transitionColor1, 0);

            if (!u && !r)
            {
                screen.render(x * 16 + 8, y * 16 + 0, wRandom.nextInt(4), col, wRandom.nextInt(4));
            }
            else
                screen.render(x * 16 + 8, y * 16 + 0, (r ? 16 : 15) + (u ? 0 : 1) * 32, (su || sr) ? transitionColor2 : transitionColor1, 0);

            if (!d && !l)
            {
                screen.render(x * 16 + 0, y * 16 + 8, wRandom.nextInt(4), col, wRandom.nextInt(4));
            }
            else
                screen.render(x * 16 + 0, y * 16 + 8, (l ? 14 : 15) + (d ? 2 : 1) * 32, (sd || sl) ? transitionColor2 : transitionColor1, 0);
            if (!d && !r)
            {
                screen.render(x * 16 + 8, y * 16 + 8, wRandom.nextInt(4), col, wRandom.nextInt(4));
            }
            else
                screen.render(x * 16 + 8, y * 16 + 8, (r ? 16 : 15) + (d ? 2 : 1) * 32, (sd || sr) ? transitionColor2 : transitionColor1, 0);
        }

        public override bool mayPass(Level level, int x, int y, Entity e)
        {
            return e.canSwim();
        }

        public override void tick(Level level, int xt, int yt)
        {
            int xn = xt;
            int yn = yt;

            if (random.nextbool())
                xn += random.nextInt(2) * 2 - 1;
            else
                yn += random.nextInt(2) * 2 - 1;

            if (level.getTile(xn, yn) == Tile.hole)
            {
                level.setTile(xn, yn, this, 0);
            }
        }

        public override int getLightRadius(Level level, int x, int y)
        {
            return 6;
        }
    }
}
