using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniCraft.Entities;
using MiniCraft.Gfx;
using MiniCraft.Items;
using MiniCraft.Items.Resources;

namespace MiniCraft.Levels.Tiles
{

    public class WheatTile : Tile
    {
        public WheatTile(int id)
            : base(id)
        {
        }

        public override void render(Screen screen, Level level, int x, int y)
        {
            int age = level.getData(x, y);
            int col = ColorHelper.get(level.dirtColor - 121, level.dirtColor - 11, level.dirtColor, 50);
            int icon = age / 10;
            if (icon >= 3)
            {
                col = ColorHelper.get(level.dirtColor - 121, level.dirtColor - 11, 50 + (icon) * 100, 40 + (icon - 3) * 2 * 100);
                if (age == 50)
                {
                    col = ColorHelper.get(0, 0, 50 + (icon) * 100, 40 + (icon - 3) * 2 * 100);
                }
                icon = 3;
            }

            screen.render(x * 16 + 0, y * 16 + 0, 4 + 3 * 32 + icon, col, 0);
            screen.render(x * 16 + 8, y * 16 + 0, 4 + 3 * 32 + icon, col, 0);
            screen.render(x * 16 + 0, y * 16 + 8, 4 + 3 * 32 + icon, col, 1);
            screen.render(x * 16 + 8, y * 16 + 8, 4 + 3 * 32 + icon, col, 1);
        }

        public override void tick(Level level, int xt, int yt)
        {
            if (random.nextInt(2) == 0) return;

            int age = level.getData(xt, yt);
            if (age < 50) level.setData(xt, yt, age + 1);
        }

        public override bool interact(Level level, int xt, int yt, Player player, Item item, int attackDir)
        {
            if (item is ToolItem)
            {
                ToolItem tool = (ToolItem)item;
                if (tool.type == ToolType.shovel)
                {
                    if (player.payStamina(4 - tool.level))
                    {
                        level.setTile(xt, yt, Tile.dirt, 0);
                        return true;
                    }
                }
            }
            return false;
        }

        public override void steppedOn(Level level, int xt, int yt, Entity entity)
        {
            if (random.nextInt(60) != 0) return;
            if (level.getData(xt, yt) < 2) return;
            harvest(level, xt, yt);
        }

        public override void hurt(Level level, int x, int y, Mob source, int dmg, int attackDir)
        {

            harvest(level, x, y);
        }

        private void harvest(Level level, int x, int y)
        {
            int age = level.getData(x, y);

            int count = random.nextInt(2);
            for (int i = 0; i < count; i++)
            {
                level.add(new ItemEntity(new ResourceItem(Resource.seeds), x * 16 + random.nextInt(10) + 3, y * 16 + random.nextInt(10) + 3));
            }

            count = 0;
            if (age == 50)
            {
                count = random.nextInt(3) + 2;
            }
            else if (age >= 40)
            {
                count = random.nextInt(2) + 1;
            }
            for (int i = 0; i < count; i++)
            {
                level.add(new ItemEntity(new ResourceItem(Resource.wheat), x * 16 + random.nextInt(10) + 3, y * 16 + random.nextInt(10) + 3));
            }

            level.setTile(x, y, Tile.dirt, 0);
        }
    }

}
