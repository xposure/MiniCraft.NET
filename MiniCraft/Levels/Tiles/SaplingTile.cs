using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniCraft.Entities;
using MiniCraft.Gfx;

namespace MiniCraft.Levels.Tiles
{
    public class SaplingTile : Tile
    {
        private Tile onType;
        private Tile growsTo;

        public SaplingTile(int id, Tile onType, Tile growsTo)
            : base(id)
        {
            this.onType = onType;
            this.growsTo = growsTo;
            connectsToSand = onType.connectsToSand;
            connectsToGrass = onType.connectsToGrass;
            connectsToWater = onType.connectsToWater;
            connectsToLava = onType.connectsToLava;
        }

        public override void render(Screen screen, Level level, int x, int y)
        {
            onType.render(screen, level, x, y);
            int col = ColorHelper.get(10, 40, 50, -1);
            screen.render(x * 16 + 4, y * 16 + 4, 11 + 3 * 32, col, 0);
        }

        public override void tick(Level level, int x, int y)
        {
            int age = level.getData(x, y) + 1;
            if (age > 100)
            {
                level.setTile(x, y, growsTo, 0);
            }
            else
            {
                level.setData(x, y, age);
            }
        }

        public override void hurt(Level level, int x, int y, Mob source, int dmg, int attackDir)
        {
            level.setTile(x, y, onType, 0);
        }
    }
}
