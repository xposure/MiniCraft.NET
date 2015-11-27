﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniCraft.Entities;
using MiniCraft.Gfx;
using MiniCraft.Items;
using MiniCraft.Items.Resources;

namespace MiniCraft.Levels.Tiles
{

    public class SandTile : Tile
    {
        public SandTile(TileID id)
            : base(id)
        {
            connectsToSand = true;
        }

        public override void render(Screen screen, Level level, int x, int y)
        {
            int col = ColorHelper.get(level.sandColor + 2, level.sandColor, level.sandColor - 110, level.sandColor - 110);
            int transitionColor = ColorHelper.get(level.sandColor - 110, level.sandColor, level.sandColor - 110, level.dirtColor);

            bool u = !level.getTile(x, y - 1).connectsToSand;
            bool d = !level.getTile(x, y + 1).connectsToSand;
            bool l = !level.getTile(x - 1, y).connectsToSand;
            bool r = !level.getTile(x + 1, y).connectsToSand;

            bool steppedOn = level.getData(x, y) > 0;

            if (!u && !l)
            {
                if (!steppedOn)
                    screen.render(x * 16 + 0, y * 16 + 0, 0, col, 0);
                else
                    screen.render(x * 16 + 0, y * 16 + 0, 3 + 1 * 32, col, 0);
            }
            else
                screen.render(x * 16 + 0, y * 16 + 0, (l ? 11 : 12) + (u ? 0 : 1) * 32, transitionColor, 0);

            if (!u && !r)
            {
                screen.render(x * 16 + 8, y * 16 + 0, 1, col, 0);
            }
            else
                screen.render(x * 16 + 8, y * 16 + 0, (r ? 13 : 12) + (u ? 0 : 1) * 32, transitionColor, 0);

            if (!d && !l)
            {
                screen.render(x * 16 + 0, y * 16 + 8, 2, col, 0);
            }
            else
                screen.render(x * 16 + 0, y * 16 + 8, (l ? 11 : 12) + (d ? 2 : 1) * 32, transitionColor, 0);
            if (!d && !r)
            {
                if (!steppedOn)
                    screen.render(x * 16 + 8, y * 16 + 8, 3, col, 0);
                else
                    screen.render(x * 16 + 8, y * 16 + 8, 3 + 1 * 32, col, 0);

            }
            else
                screen.render(x * 16 + 8, y * 16 + 8, (r ? 13 : 12) + (d ? 2 : 1) * 32, transitionColor, 0);
        }

        public override void tick(Level level, int x, int y)
        {
            int d = level.getData(x, y);
            if (d > 0) level.setData(x, y, d - 1);
        }

        public override void steppedOn(Level level, int x, int y, Entity entity)
        {
            if (entity is Mob)
            {
                level.setData(x, y, 10);
            }
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
                        level.add(new ItemEntity(new ResourceItem(Resource.sand), xt * 16 + random.nextInt(10) + 3, yt * 16 + random.nextInt(10) + 3));
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
