using System;
using System.Collections.Generic;
using MiniCraft.Entities;
using MiniCraft.Levels;
using MiniCraft.Levels.Tiles;

namespace MiniCraft.Items.Resources
{
    public class PlantableResource : Resource
    {
        private List<TileID> sourceTiles;
        private TileID targetTile;

        public PlantableResource(string name, int sprite, int color, TileID targetTile, params TileID[] sourceTiles1) :
            this(name, sprite, color, targetTile, new List<TileID>(sourceTiles1))
        { }

        public PlantableResource(string name, int sprite, int color, TileID targetTile, List<TileID> sourceTiles)
            : base(name, sprite, color)
        {
            this.sourceTiles = sourceTiles;
            this.targetTile = targetTile;
        }

        public override bool interactOn(Tile tile, Level level, int xt, int yt, Player player, int attackDir)
        {
            if (sourceTiles.Contains((TileID)tile.id))
            {
                var tt = Tile.tiles[(byte)targetTile];
                level.setTile(xt, yt, tt, 0);
                return true;
            }
            return false;
        }
    }
}
