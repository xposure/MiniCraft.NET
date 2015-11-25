using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniCraft.Crafts;
using MiniCraft.Gfx;
using MiniCraft.Screens;

namespace MiniCraft.Entities
{
    public class Workbench : Furniture
    {
        public Workbench()
            : base("Workbench")
        {
            col = ColorHelper.get(-1, 100, 321, 431);
            sprite = 4;
            xr = 3;
            yr = 2;
        }

        public override bool use(Player player, int attackDir)
        {
            player.game.setMenu(new CraftingMenu(Crafting.workbenchRecipes, player));
            return true;
        }
    }
}
