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

    public class TreeTile : Tile
    {
        public TreeTile(int id)
            : base(id)
        {
            connectsToGrass = true;
        }

        public override void render(Screen screen, Level level, int x, int y)
        {
            int col = ColorHelper.get(10, 30, 151, level.grassColor);
            int barkCol1 = ColorHelper.get(10, 30, 430, level.grassColor);
            int barkCol2 = ColorHelper.get(10, 30, 320, level.grassColor);

            bool u = level.getTile(x, y - 1) == this;
            bool l = level.getTile(x - 1, y) == this;
            bool r = level.getTile(x + 1, y) == this;
            bool d = level.getTile(x, y + 1) == this;
            bool ul = level.getTile(x - 1, y - 1) == this;
            bool ur = level.getTile(x + 1, y - 1) == this;
            bool dl = level.getTile(x - 1, y + 1) == this;
            bool dr = level.getTile(x + 1, y + 1) == this;

            if (u && ul && l)
            {
                screen.render(x * 16 + 0, y * 16 + 0, 10 + 1 * 32, col, 0);
            }
            else
            {
                screen.render(x * 16 + 0, y * 16 + 0, 9 + 0 * 32, col, 0);
            }
            if (u && ur && r)
            {
                screen.render(x * 16 + 8, y * 16 + 0, 10 + 2 * 32, barkCol2, 0);
            }
            else
            {
                screen.render(x * 16 + 8, y * 16 + 0, 10 + 0 * 32, col, 0);
            }
            if (d && dl && l)
            {
                screen.render(x * 16 + 0, y * 16 + 8, 10 + 2 * 32, barkCol2, 0);
            }
            else
            {
                screen.render(x * 16 + 0, y * 16 + 8, 9 + 1 * 32, barkCol1, 0);
            }
            if (d && dr && r)
            {
                screen.render(x * 16 + 8, y * 16 + 8, 10 + 1 * 32, col, 0);
            }
            else
            {
                screen.render(x * 16 + 8, y * 16 + 8, 10 + 3 * 32, barkCol2, 0);
            }
        }

        public override void tick(Level level, int xt, int yt)
        {
            int damage = level.getData(xt, yt);
            if (damage > 0) level.setData(xt, yt, damage - 1);
        }

        public override bool mayPass(Level level, int x, int y, Entity e)
        {
            return false;
        }

        public override void hurt(Level level, int x, int y, Mob source, int dmg, int attackDir)
        {
            hurt(level, x, y, dmg);
        }

        public override bool interact(Level level, int xt, int yt, Player player, Item item, int attackDir)
        {
            if (item is ToolItem)
            {
                ToolItem tool = (ToolItem)item;
                if (tool.type == ToolType.axe)
                {
                    if (player.payStamina(4 - tool.level))
                    {
                        hurt(level, xt, yt, random.nextInt(10) + (tool.level) * 5 + 10);
                        return true;
                    }
                }
            }
            return false;
        }

        private void hurt(Level level, int x, int y, int dmg)
        {
            {
                int count = random.nextInt(10) == 0 ? 1 : 0;
                for (int i = 0; i < count; i++)
                {
                    level.add(new ItemEntity(new ResourceItem(Resource.apple), x * 16 + random.nextInt(10) + 3, y * 16 + random.nextInt(10) + 3));
                }
            }
            int damage = level.getData(x, y) + dmg;
            level.add(new SmashParticle(x * 16 + 8, y * 16 + 8));
            level.add(new TextParticle("" + dmg, x * 16 + 8, y * 16 + 8, ColorHelper.get(-1, 500, 500, 500)));
            if (damage >= 20)
            {
                int count = random.nextInt(2) + 1;
                for (int i = 0; i < count; i++)
                {
                    level.add(new ItemEntity(new ResourceItem(Resource.wood), x * 16 + random.nextInt(10) + 3, y * 16 + random.nextInt(10) + 3));
                }
                count = random.nextInt(random.nextInt(4) + 1);
                for (int i = 0; i < count; i++)
                {
                    level.add(new ItemEntity(new ResourceItem(Resource.acorn), x * 16 + random.nextInt(10) + 3, y * 16 + random.nextInt(10) + 3));
                }
                level.setTile(x, y, Tile.grass, 0);
            }
            else
            {
                level.setData(x, y, damage);
            }
        }
    }
}
