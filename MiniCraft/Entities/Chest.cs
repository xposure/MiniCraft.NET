using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniCraft.Gfx;
using MiniCraft.Screens;

namespace MiniCraft.Entities
{
    public class Chest : Furniture
    {
        public Inventory inventory = new Inventory();

        public Chest()
            : base("Chest")
        {
            col = ColorHelper.get(-1, 110, 331, 552);
            sprite = 1;
        }

        public override bool use(Player player, int attackDir)
        {
            player.game.setMenu(new ContainerMenu(player, "Chest", inventory));
            return true;
        }
    }
}
