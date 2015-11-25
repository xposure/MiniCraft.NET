using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniCraft.Entities;
using MiniCraft.Entities.Particles;
using MiniCraft.Gfx;
using MiniCraft.Items;
using MiniCraft.Items.Resources;

namespace MiniCraft.Levels.Tiles
{

    public class HardRockTile : Tile
    {
        public HardRockTile(int id)
            : base(id)
        {
        }

        public override void render(Screen screen, Level level, int x, int y)
        {
            int col = ColorHelper.get(334, 334, 223, 223);
            int transitionColor = ColorHelper.get(001, 334, 445, level.dirtColor);

            bool u = level.getTile(x, y - 1) != this;
            bool d = level.getTile(x, y + 1) != this;
            bool l = level.getTile(x - 1, y) != this;
            bool r = level.getTile(x + 1, y) != this;

            bool ul = level.getTile(x - 1, y - 1) != this;
            bool dl = level.getTile(x - 1, y + 1) != this;
            bool ur = level.getTile(x + 1, y - 1) != this;
            bool dr = level.getTile(x + 1, y + 1) != this;

            if (!u && !l)
            {
                if (!ul)
                    screen.render(x * 16 + 0, y * 16 + 0, 0, col, 0);
                else
                    screen.render(x * 16 + 0, y * 16 + 0, 7 + 0 * 32, transitionColor, 3);
            }
            else
                screen.render(x * 16 + 0, y * 16 + 0, (l ? 6 : 5) + (u ? 2 : 1) * 32, transitionColor, 3);

            if (!u && !r)
            {
                if (!ur)
                    screen.render(x * 16 + 8, y * 16 + 0, 1, col, 0);
                else
                    screen.render(x * 16 + 8, y * 16 + 0, 8 + 0 * 32, transitionColor, 3);
            }
            else
                screen.render(x * 16 + 8, y * 16 + 0, (r ? 4 : 5) + (u ? 2 : 1) * 32, transitionColor, 3);

            if (!d && !l)
            {
                if (!dl)
                    screen.render(x * 16 + 0, y * 16 + 8, 2, col, 0);
                else
                    screen.render(x * 16 + 0, y * 16 + 8, 7 + 1 * 32, transitionColor, 3);
            }
            else
                screen.render(x * 16 + 0, y * 16 + 8, (l ? 6 : 5) + (d ? 0 : 1) * 32, transitionColor, 3);
            if (!d && !r)
            {
                if (!dr)
                    screen.render(x * 16 + 8, y * 16 + 8, 3, col, 0);
                else
                    screen.render(x * 16 + 8, y * 16 + 8, 8 + 1 * 32, transitionColor, 3);
            }
            else
                screen.render(x * 16 + 8, y * 16 + 8, (r ? 4 : 5) + (d ? 0 : 1) * 32, transitionColor, 3);
        }

        public override bool mayPass(Level level, int x, int y, Entity e)
        {
            return false;
        }

        public override void hurt(Level level, int x, int y, Mob source, int dmg, int attackDir)
        {
            hurt(level, x, y, 0);
        }

        public override bool interact(Level level, int xt, int yt, Player player, Item item, int attackDir) {
		if (item is ToolItem) {
			ToolItem tool = (ToolItem) item;
			if (tool.type == ToolType.pickaxe && tool.level == 4) {
				if (player.payStamina(4 - tool.level)) {
					hurt(level, xt, yt, random.nextInt(10) + (tool.level) * 5 + 10);
					return true;
				}
			}
		}
		return false;
	}

        public void hurt(Level level, int x, int y, int dmg)
        {
            int damage = level.getData(x, y) + dmg;
            level.add(new SmashParticle(x * 16 + 8, y * 16 + 8));
            level.add(new TextParticle("" + dmg, x * 16 + 8, y * 16 + 8, ColorHelper.get(-1, 500, 500, 500)));
            if (damage >= 200)
            {
                int count = random.nextInt(4) + 1;
                for (int i = 0; i < count; i++)
                {
                    level.add(new ItemEntity(new ResourceItem(Resource.stone), x * 16 + random.nextInt(10) + 3, y * 16 + random.nextInt(10) + 3));
                }
                count = random.nextInt(2);
                for (int i = 0; i < count; i++)
                {
                    level.add(new ItemEntity(new ResourceItem(Resource.coal), x * 16 + random.nextInt(10) + 3, y * 16 + random.nextInt(10) + 3));
                }
                level.setTile(x, y, Tile.dirt, 0);
            }
            else
            {
                level.setData(x, y, damage);
            }
        }

        public override void tick(Level level, int xt, int yt)
        {
            int damage = level.getData(xt, yt);
            if (damage > 0) level.setData(xt, yt, damage - 1);
        }
    }
}
