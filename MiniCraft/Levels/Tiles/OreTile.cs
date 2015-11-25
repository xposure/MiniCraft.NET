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

    public class OreTile : Tile
    {
        private Resource toDrop;
        private int color;

        public OreTile(int id, Resource toDrop)
            : base(id)
        {
            this.toDrop = toDrop;
            this.color = toDrop.color & 0xffff00;
        }

        public override void render(Screen screen, Level level, int x, int y)
        {
            //TODO: check is casting to int is ok
            color = (int)(toDrop.color & 0xffffff00) + ColorHelper.get(level.dirtColor);
            screen.render(x * 16 + 0, y * 16 + 0, 17 + 1 * 32, color, 0);
            screen.render(x * 16 + 8, y * 16 + 0, 18 + 1 * 32, color, 0);
            screen.render(x * 16 + 0, y * 16 + 8, 17 + 2 * 32, color, 0);
            screen.render(x * 16 + 8, y * 16 + 8, 18 + 2 * 32, color, 0);
        }

        public override bool mayPass(Level level, int x, int y, Entity e)
        {
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
                int count = random.nextInt(2);
                if (damage >= random.nextInt(10) + 3)
                {
                    level.setTile(x, y, Tile.dirt, 0);
                    count += 2;
                }
                else
                {
                    level.setData(x, y, damage);
                }
                for (int i = 0; i < count; i++)
                {
                    level.add(new ItemEntity(new ResourceItem(toDrop), x * 16 + random.nextInt(10) + 3, y * 16 + random.nextInt(10) + 3));
                }
            }
        }

        public override void bumpedInto(Level level, int x, int y, Entity entity)
        {
            entity.hurt(this, x, y, 3);
        }
    }
}
