using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniCraft.Crafts;
using MiniCraft.Gfx;
using MiniCraft.Screens;

namespace MiniCraft.Entities
{
    public class Furnace : Furniture
    {
        public Furnace()
            : base("Furnace")
        {
            col = ColorHelper.get(-1, 000, 222, 333);
            sprite = 3;
            xr = 3;
            yr = 2;
        }

        public override bool use(Player player, int attackDir)
        {
            player.game.setMenu(new CraftingMenu(Crafting.furnaceRecipes, player));
            return true;
        }
    }
}
