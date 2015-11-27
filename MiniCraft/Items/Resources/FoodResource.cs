using System;
using MiniCraft.Entities;
using MiniCraft.Levels;
using MiniCraft.Levels.Tiles;

namespace MiniCraft.Items.Resources
{
    public class FoodResource : Resource
    {
        private int heal;
        private int staminaCost;

        public FoodResource(String name, int sprite, int color, int heal, int staminaCost)
            : base(name, sprite, color)
        {
            this.heal = heal;
            this.staminaCost = staminaCost;
        }

        public override bool interactOn(Tile tile, Level level, int xt, int yt, Player player, int attackDir)
        {
            if (player.health < player.maxHealth && player.payStamina(staminaCost))
            {
                player.heal(heal);
                return true;
            }
            return false;
        }
    }
}
