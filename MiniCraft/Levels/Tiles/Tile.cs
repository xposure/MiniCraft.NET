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
    public enum TileID
    {
        GRASS,
        ROCK,
        WATER,
        FLOWER,
        TREE,
        DIRT,
        SAND,
        CACTUS,
        HOLE,
        TREE_SAPLING,
        CACTUS_SAPLING,
        FARMLAND,
        WHEAT,
        LAVA,
        STAIRS_DOWN,
        STAIRS_UP,
        INFINITE_FALL,
        CLOUD,
        HARDROCK,
        IRON_ORE,
        GOLD_ORE,
        GEM_ORE,
        CLOUD_CACTUS
    }

    public class Tile
    {
        public static int tickCount = 0;
        protected Random random = new Random();

        public static Tile[] tiles = new Tile[256];
        public static Tile grass;
        public static Tile rock;
        public static Tile water;
        public static Tile flower;
        public static Tile tree;
        public static Tile dirt;
        public static Tile sand;
        public static Tile cactus;
        public static Tile hole;
        public static Tile treeSapling;
        public static Tile cactusSapling;
        public static Tile farmland;
        public static Tile wheat;
        public static Tile lava;
        public static Tile stairsDown;
        public static Tile stairsUp;
        public static Tile infiniteFall;
        public static Tile cloud;
        public static Tile hardRock;
        public static Tile ironOre;
        public static Tile goldOre;
        public static Tile gemOre;
        public static Tile cloudCactus;

        static Tile()
        {
            grass = new GrassTile(TileID.GRASS);
            rock = new RockTile(TileID.ROCK);
            water = new WaterTile(TileID.WATER);
            flower = new FlowerTile(TileID.FLOWER);
            tree = new TreeTile(TileID.TREE);
            dirt = new DirtTile(TileID.DIRT);
            sand = new SandTile(TileID.SAND);
            cactus = new CactusTile(TileID.CACTUS);
            hole = new HoleTile(TileID.HOLE);
            treeSapling = new SaplingTile(TileID.TREE_SAPLING, grass, tree);
            cactusSapling = new SaplingTile(TileID.CACTUS_SAPLING, sand, cactus);
            farmland = new FarmTile(TileID.FARMLAND);
            wheat = new WheatTile(TileID.WHEAT);
            lava = new LavaTile(TileID.LAVA);
            stairsDown = new StairsTile(TileID.STAIRS_DOWN, false);
            stairsUp = new StairsTile(TileID.STAIRS_UP, true);
            infiniteFall = new InfiniteFallTile(TileID.INFINITE_FALL);
            cloud = new CloudTile(TileID.CLOUD);
            hardRock = new HardRockTile(TileID.HARDROCK);
            ironOre = new OreTile(TileID.IRON_ORE, Resource.ironOre);
            goldOre = new OreTile(TileID.GOLD_ORE, Resource.goldOre);
            gemOre = new OreTile(TileID.GEM_ORE, Resource.gem);
            cloudCactus = new CloudCactusTile(TileID.CLOUD_CACTUS);
        }


        public readonly byte id;

        public bool connectsToGrass = false;
        public bool connectsToSand = false;
        public bool connectsToLava = false;
        public bool connectsToWater = false;

        public Tile(TileID tileId)
        {
            this.id = (byte)tileId;
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
