using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniCraft.Entities;
using MiniCraft.Levels;
using MiniCraft.Levels.Tiles;

namespace MiniCraft.Items.Resources
{
    public class PlantableResource : Resource
    {
        private List<Tile> sourceTiles;
        private Tile targetTile;

        public PlantableResource(String name, int sprite, int color, Tile targetTile, params Tile[] sourceTiles1) :
            this(name, sprite, color, targetTile, new List<Tile>(sourceTiles1))
        { }

        public PlantableResource(String name, int sprite, int color, Tile targetTile, List<Tile> sourceTiles)
            : base(name, sprite, color)
        {
            this.sourceTiles = sourceTiles;
            this.targetTile = targetTile;
        }

        public override bool interactOn(Tile tile, Level level, int xt, int yt, Player player, int attackDir)
        {
            if (sourceTiles.Contains(tile))
            {
                level.setTile(xt, yt, targetTile, 0);
                return true;
            }
            return false;
        }
    }
}
