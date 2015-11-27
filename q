[1mdiff --git a/MiniCraft/Items/Resources/PlantableResource.cs b/MiniCraft/Items/Resources/PlantableResource.cs[m
[1mindex 3286976..874f987 100644[m
[1m--- a/MiniCraft/Items/Resources/PlantableResource.cs[m
[1m+++ b/MiniCraft/Items/Resources/PlantableResource.cs[m
[36m@@ -10,14 +10,14 @@[m [mnamespace MiniCraft.Items.Resources[m
 {[m
     public class PlantableResource : Resource[m
     {[m
[31m-        private List<Tile> sourceTiles;[m
[31m-        private Tile targetTile;[m
[32m+[m[32m        private List<TileID> sourceTiles;[m
[32m+[m[32m        private TileID targetTile;[m
 [m
[31m-        public PlantableResource(String name, int sprite, int color, Tile targetTile, params Tile[] sourceTiles1) :[m
[31m-            this(name, sprite, color, targetTile, new List<Tile>(sourceTiles1))[m
[32m+[m[32m        public PlantableResource(String name, int sprite, int color, TileID targetTile, params TileID[] sourceTiles1) :[m
[32m+[m[32m            this(name, sprite, color, targetTile, new List<TileID>(sourceTiles1))[m
         { }[m
 [m
[31m-        public PlantableResource(String name, int sprite, int color, Tile targetTile, List<Tile> sourceTiles)[m
[32m+[m[32m        public PlantableResource(String name, int sprite, int color, TileID targetTile, List<TileID> sourceTiles)[m
             : base(name, sprite, color)[m
         {[m
             this.sourceTiles = sourceTiles;[m
[36m@@ -26,9 +26,10 @@[m [mnamespace MiniCraft.Items.Resources[m
 [m
         public override bool interactOn(Tile tile, Level level, int xt, int yt, Player player, int attackDir)[m
         {[m
[31m-            if (sourceTiles.Contains(tile))[m
[32m+[m[32m            if (sourceTiles.Contains((TileID)tile.id))[m
             {[m
[31m-                level.setTile(xt, yt, targetTile, 0);[m
[32m+[m[32m                var tt = Tile.tiles[(byte)targetTile];[m
[32m+[m[32m                level.setTile(xt, yt, tt, 0);[m
                 return true;[m
             }[m
             return false;[m
[1mdiff --git a/MiniCraft/Items/Resources/Resource.cs b/MiniCraft/Items/Resources/Resource.cs[m
[1mindex 4539e99..85669d6 100644[m
[1m--- a/MiniCraft/Items/Resources/Resource.cs[m
[1m+++ b/MiniCraft/Items/Resources/Resource.cs[m
[36m@@ -12,14 +12,18 @@[m [mnamespace MiniCraft.Items.Resources[m
 [m
     public class Resource[m
     {[m
[32m+[m[32m        public static List<Resource> allResources = new List<Resource>();[m
[32m+[m
         public static Resource wood = new Resource("Wood", 1 + 4 * 32, ColorHelper.get(-1, 200, 531, 430));[m
         public static Resource stone = new Resource("Stone", 2 + 4 * 32, ColorHelper.get(-1, 111, 333, 555));[m
[31m-        public static Resource flower = new PlantableResource("Flower", 0 + 4 * 32, ColorHelper.get(-1, 10, 444, 330), Tile.flower, Tile.grass);[m
[31m-        public static Resource acorn = new PlantableResource("Acorn", 3 + 4 * 32, ColorHelper.get(-1, 100, 531, 320), Tile.treeSapling, Tile.grass);[m
[31m-        public static Resource dirt = new PlantableResource("Dirt", 2 + 4 * 32, ColorHelper.get(-1, 100, 322, 432), Tile.dirt, Tile.hole, Tile.water, Tile.lava);[m
[31m-        public static Resource sand = new PlantableResource("Sand", 2 + 4 * 32, ColorHelper.get(-1, 110, 440, 550), Tile.sand, Tile.grass, Tile.dirt);[m
[31m-        public static Resource cactusFlower = new PlantableResource("Cactus", 4 + 4 * 32, ColorHelper.get(-1, 10, 40, 50), Tile.cactusSapling, Tile.sand);[m
[31m-        public static Resource seeds = new PlantableResource("Seeds", 5 + 4 * 32, ColorHelper.get(-1, 10, 40, 50), Tile.wheat, Tile.farmland);[m
[32m+[m[32m        public static Resource flower = new PlantableResource("Flower", 0 + 4 * 32, ColorHelper.get(-1, 10, 444, 330), TileID.FLOWER, TileID.GRASS);[m
[32m+[m[32m        public static Resource acorn = new PlantableResource("Acorn", 3 + 4 * 32, ColorHelper.get(-1, 100, 531, 320), TileID.TREE_SAPLING, TileID.GRASS);[m
[32m+[m[32m        public static Resource dirt = new PlantableResource("Dirt", 2 + 4 * 32, ColorHelper.get(-1, 100, 322, 432), TileID.DIRT, TileID.HOLE, TileID.WATER, TileID.LAVA);[m
[32m+[m[32m        public static Resource sand = new PlantableResource("Sand", 2 + 4 * 32, ColorHelper.get(-1, 110, 440, 550), TileID.SAND, TileID.GRASS, TileID.DIRT);[m
[32m+[m[32m        public static Resource cactusFlower = new PlantableResource("Cactus", 4 + 4 * 32, ColorHelper.get(-1, 10, 40, 50), TileID.CACTUS_SAPLING, TileID.SAND);[m
[32m+[m[32m        public static Resource seeds = new PlantableResource("Seeds", 5 + 4 * 32, ColorHelper.get(-1, 10, 40, 50), TileID.WHEAT, TileID.FARMLAND);[m
[32m+[m[32m        public static Resource cloud = new PlantableResource("cloud", 2 + 4 * 32, ColorHelper.get(-1, 222, 555, 444), TileID.CLOUD, TileID.INFINITE_FALL);[m
[32m+[m
         public static Resource wheat = new Resource("Wheat", 6 + 4 * 32, ColorHelper.get(-1, 110, 330, 550));[m
         public static Resource bread = new FoodResource("Bread", 8 + 4 * 32, ColorHelper.get(-1, 110, 330, 550), 2, 5);[m
         public static Resource apple = new FoodResource("Apple", 9 + 4 * 32, ColorHelper.get(-1, 100, 300, 500), 1, 5);[m
[36m@@ -33,7 +37,6 @@[m [mnamespace MiniCraft.Items.Resources[m
         public static Resource slime = new Resource("SLIME", 10 + 4 * 32, ColorHelper.get(-1, 10, 30, 50));[m
         public static Resource glass = new Resource("glass", 12 + 4 * 32, ColorHelper.get(-1, 555, 555, 555));[m
         public static Resource cloth = new Resource("cloth", 1 + 4 * 32, ColorHelper.get(-1, 25, 252, 141));[m
[31m-        public static Resource cloud = new PlantableResource("cloud", 2 + 4 * 32, ColorHelper.get(-1, 222, 555, 444), Tile.cloud, Tile.infiniteFall);[m
         public static Resource gem = new Resource("gem", 13 + 4 * 32, ColorHelper.get(-1, 101, 404, 545));[m
 [m
         public readonly String name;[m
[36m@@ -46,6 +49,8 @@[m [mnamespace MiniCraft.Items.Resources[m
             this.name = name;[m
             this.sprite = sprite;[m
             this.color = color;[m
[32m+[m
[32m+[m[32m            allResources.add(this);[m
         }[m
 [m
         public virtual bool interactOn(Tile tile, Level level, int xt, int yt, Player player, int attackDir)[m
[1mdiff --git a/MiniCraft/MCGame.cs b/MiniCraft/MCGame.cs[m
[1mindex e1c5bf7..34e383d 100644[m
[1m--- a/MiniCraft/MCGame.cs[m
[1m+++ b/MiniCraft/MCGame.cs[m
[36m@@ -4,6 +4,8 @@[m [musing Microsoft.Xna.Framework.Graphics;[m
 using Microsoft.Xna.Framework.Input;[m
 using MiniCraft.Entities;[m
 using MiniCraft.Gfx;[m
[32m+[m[32musing MiniCraft.Items;[m
[32m+[m[32musing MiniCraft.Items.Resources;[m
 using MiniCraft.Levels;[m
 using MiniCraft.Levels.Tiles;[m
 using MiniCraft.Screens;[m
[36m@@ -18,9 +20,9 @@[m [mnamespace MiniCraft[m
         private const long serialVersionUID = 1L;[m
         private Random random = new Random();[m
         public const String NAME = "Minicraft";[m
[31m-        public const int HEIGHT = 120;[m
[31m-        public const int WIDTH = 160;[m
[31m-        public const int SCALE = 4;[m
[32m+[m[32m        public const int HEIGHT = 120 * 2;[m
[32m+[m[32m        public const int WIDTH = 160 * 2;[m
[32m+[m[32m        public const int SCALE = 3;[m
 [m
         private Texture2D image;[m
         private Color[] pixels;[m
[36m@@ -73,6 +75,7 @@[m [mnamespace MiniCraft[m
 [m
         public void resetGame()[m
         {[m
[32m+[m
             playerDeadTime = 0;[m
             wonTimer = 0;[m
             gameTime = 0;[m
[1mdiff --git a/MiniCraft/Screens/TitleMenu.cs b/MiniCraft/Screens/TitleMenu.cs[m
[1mindex 71d7a7e..163543d 100644[m
[1m--- a/MiniCraft/Screens/TitleMenu.cs[m
[1m+++ b/MiniCraft/Screens/TitleMenu.cs[m
[36m@@ -3,6 +3,8 @@[m [musing System.Collections.Generic;[m
 using System.Linq;[m
 using System.Text;[m
 using MiniCraft.Gfx;[m
[32m+[m[32musing MiniCraft.Items;[m
[32m+[m[32musing MiniCraft.Items.Resources;[m
 using MiniCraft.Sounds;[m
 [m
 namespace MiniCraft.Screens[m
[36m@@ -33,6 +35,10 @@[m [mnamespace MiniCraft.Screens[m
                     Sound.test.play();[m
                     game.resetGame();[m
                     game.setMenu(null);[m
[32m+[m
[32m+[m[32m                    //if (Resource.allResources != null)[m
[32m+[m[32m                    //    foreach (var resource in Resource.allResources)[m
[32m+[m[32m                            game.player.inventory.add(new ResourceItem(Resource.wood, 999));[m
                 }[m
                 if (selected == 1) game.setMenu(new InstructionsMenu(this));[m
                 if (selected == 2) game.setMenu(new AboutMenu(this));[m
