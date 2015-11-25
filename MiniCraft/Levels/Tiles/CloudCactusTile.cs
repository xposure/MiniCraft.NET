using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniCraft.Entities;
using MiniCraft.Entities.Particles;
using MiniCraft.Gfx;
using MiniCraft.Items;

namespace MiniCraft.Levels.Tiles
{
    public class CloudCactusTile : Tile
    {
        public CloudCactusTile(int id)
            : base(id)
        {
        }

        public override void render(Screen screen, Level level, int x, int y)
        {
            int color = ColorHelper.get(444, 111, 333, 555);
            screen.render(x * 16 + 0, y * 16 + 0, 17 + 1 * 32, color, 0);
            screen.render(x * 16 + 8, y * 16 + 0, 18 + 1 * 32, color, 0);
            screen.render(x * 16 + 0, y * 16 + 8, 17 + 2 * 32, color, 0);
            screen.render(x * 16 + 8, y * 16 + 8, 18 + 2 * 32, color, 0);
        }

        public override bool mayPass(Level level, int x, int y, Entity e)
        {
            if (e is AirWizard) return true;
            return false;
        }

        public override void hurt(Level level, int x, int y, Mob source, int dmg, int attackDir)
        {
            hurt(level, x, y, 0);
        }

        public override bool interact(Level level, int xt, int yt, Player player, Item item, int attackDir)
        {
            if (item is ToolItem)
            {
                ToolItem tool = (ToolItem)item;
                if (tool.type == ToolType.pickaxe)
                {
                    if (player.payStamina(6 - tool.level))
                    {
                        hurt(level, xt, yt, 1);
                        return true;
                    }
                }
            }
            return false;
        }

        public void hurt(Level level, int x, int y, int dmg)
        {
            int damage = level.getData(x, y) + 1;
            level.add(new SmashParticle(x * 16 + 8, y * 16 + 8));
            level.add(new TextParticle("" + dmg, x * 16 + 8, y * 16 + 8, ColorHelper.get(-1, 500, 500, 500)));
            if (dmg > 0)
            {
                if (damage >= 10)
                {
                    level.setTile(x, y, Tile.cloud, 0);
                }
                else
                {
                    level.setData(x, y, damage);
                }
            }
        }

        public override void bumpedInto(Level level, int x, int y, Entity entity)
        {
            if (entity is AirWizard) return;
            entity.hurt(this, x, y, 3);
        }
    }
}
