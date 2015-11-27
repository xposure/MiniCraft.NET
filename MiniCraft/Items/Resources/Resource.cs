using System;
using System.Collections.Generic;
using MiniCraft.Entities;
using MiniCraft.Gfx;
using MiniCraft.Levels;
using MiniCraft.Levels.Tiles;

namespace MiniCraft.Items.Resources
{

    public class Resource
    {
        public static List<Resource> allResources = new List<Resource>();

        public static Resource wood = new Resource("Wood", 1 + 4 * 32, ColorHelper.get(-1, 200, 531, 430));
        public static Resource stone = new Resource("Stone", 2 + 4 * 32, ColorHelper.get(-1, 111, 333, 555));
        public static Resource flower = new PlantableResource("Flower", 0 + 4 * 32, ColorHelper.get(-1, 10, 444, 330), TileID.FLOWER, TileID.GRASS);
        public static Resource acorn = new PlantableResource("Acorn", 3 + 4 * 32, ColorHelper.get(-1, 100, 531, 320), TileID.TREE_SAPLING, TileID.GRASS);
        public static Resource dirt = new PlantableResource("Dirt", 2 + 4 * 32, ColorHelper.get(-1, 100, 322, 432), TileID.DIRT, TileID.HOLE, TileID.WATER, TileID.LAVA);
        public static Resource sand = new PlantableResource("Sand", 2 + 4 * 32, ColorHelper.get(-1, 110, 440, 550), TileID.SAND, TileID.GRASS, TileID.DIRT);
        public static Resource cactusFlower = new PlantableResource("Cactus", 4 + 4 * 32, ColorHelper.get(-1, 10, 40, 50), TileID.CACTUS_SAPLING, TileID.SAND);
        public static Resource seeds = new PlantableResource("Seeds", 5 + 4 * 32, ColorHelper.get(-1, 10, 40, 50), TileID.WHEAT, TileID.FARMLAND);
        public static Resource cloud = new PlantableResource("cloud", 2 + 4 * 32, ColorHelper.get(-1, 222, 555, 444), TileID.CLOUD, TileID.INFINITE_FALL);

        public static Resource wheat = new Resource("Wheat", 6 + 4 * 32, ColorHelper.get(-1, 110, 330, 550));
        public static Resource bread = new FoodResource("Bread", 8 + 4 * 32, ColorHelper.get(-1, 110, 330, 550), 2, 5);
        public static Resource apple = new FoodResource("Apple", 9 + 4 * 32, ColorHelper.get(-1, 100, 300, 500), 1, 5);

        public static Resource coal = new Resource("COAL", 10 + 4 * 32, ColorHelper.get(-1, 000, 111, 111));
        public static Resource ironOre = new Resource("I.ORE", 10 + 4 * 32, ColorHelper.get(-1, 100, 322, 544));
        public static Resource goldOre = new Resource("G.ORE", 10 + 4 * 32, ColorHelper.get(-1, 110, 440, 553));
        public static Resource ironIngot = new Resource("IRON", 11 + 4 * 32, ColorHelper.get(-1, 100, 322, 544));
        public static Resource goldIngot = new Resource("GOLD", 11 + 4 * 32, ColorHelper.get(-1, 110, 330, 553));

        public static Resource slime = new Resource("SLIME", 10 + 4 * 32, ColorHelper.get(-1, 10, 30, 50));
        public static Resource glass = new Resource("glass", 12 + 4 * 32, ColorHelper.get(-1, 555, 555, 555));
        public static Resource cloth = new Resource("cloth", 1 + 4 * 32, ColorHelper.get(-1, 25, 252, 141));
        public static Resource gem = new Resource("gem", 13 + 4 * 32, ColorHelper.get(-1, 101, 404, 545));

        public readonly string name;
        public readonly int sprite;
        public readonly int color;

        public Resource(string name, int sprite, int color)
        {
            if (name.Length > 6) throw new InvalidProgramException("Name cannot be longer than six characters!");
            this.name = name;
            this.sprite = sprite;
            this.color = color;

            allResources.add(this);
        }

        public virtual bool interactOn(Tile tile, Level level, int xt, int yt, Player player, int attackDir)
        {
            return false;
        }
    }
}
