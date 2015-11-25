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

    public class Tile
    {
        public static int tickCount = 0;
        protected Random random = new Random();

        public static Tile[] tiles = new Tile[256];
        public static Tile grass = new GrassTile(0);
        public static Tile rock = new RockTile(1);
        public static Tile water = new WaterTile(2);
        public static Tile flower = new FlowerTile(3);
        public static Tile tree = new TreeTile(4);
        public static Tile dirt = new DirtTile(5);
        public static Tile sand = new SandTile(6);
        public static Tile cactus = new CactusTile(7);
        public static Tile hole = new HoleTile(8);
        public static Tile treeSapling = new SaplingTile(9, grass, tree);
        public static Tile cactusSapling = new SaplingTile(10, sand, cactus);
        public static Tile farmland = new FarmTile(11);
        public static Tile wheat = new WheatTile(12);
        public static Tile lava = new LavaTile(13);
        public static Tile stairsDown = new StairsTile(14, false);
        public static Tile stairsUp = new StairsTile(15, true);
        public static Tile infiniteFall = new InfiniteFallTile(16);
        public static Tile cloud = new CloudTile(17);
        public static Tile hardRock = new HardRockTile(18);
        public static Tile ironOre = new OreTile(19, Resource.ironOre);
        public static Tile goldOre = new OreTile(20, Resource.goldOre);
        public static Tile gemOre = new OreTile(21, Resource.gem);
        public static Tile cloudCactus = new CloudCactusTile(22);

        public readonly byte id;

        public bool connectsToGrass = false;
        public bool connectsToSand = false;
        public bool connectsToLava = false;
        public bool connectsToWater = false;

        public Tile(int id)
        {
            this.id = (byte)id;
            if (tiles[id] != null) throw new InvalidOperationException("Duplicate tile ids!");
            tiles[id] = this;
        }

        public virtual void render(Screen screen, Level level, int x, int y)
        {
        }

        public virtual bool mayPass(Level level, int x, int y, Entity e)
        {
            return true;
        }

        public virtual int getLightRadius(Level level, int x, int y)
        {
            return 0;
        }

        public virtual void hurt(Level level, int x, int y, Mob source, int dmg, int attackDir)
        {
        }

        public virtual void bumpedInto(Level level, int xt, int yt, Entity entity)
        {
        }

        public virtual void tick(Level level, int xt, int yt)
        {
        }

        public virtual void steppedOn(Level level, int xt, int yt, Entity entity)
        {
        }

        public virtual bool interact(Level level, int xt, int yt, Player player, Item item, int attackDir)
        {
            return false;
        }

        public virtual bool use(Level level, int xt, int yt, Player player, int attackDir)
        {
            return false;
        }

        public virtual bool connectsToLiquid()
        {
            return connectsToWater || connectsToLava;
        }
    }
}
