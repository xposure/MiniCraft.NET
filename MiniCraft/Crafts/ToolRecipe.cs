using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniCraft.Entities;
using MiniCraft.Items;

namespace MiniCraft.Crafts
{
    public class ToolRecipe : Recipe
    {
        private ToolType type;
        private int level;

        public ToolRecipe(ToolType type, int level)
            :base (new ToolItem(type, level))
        {
            this.type = type;
            this.level = level;
        }

        public override void craft(Player player)
        {
            player.inventory.add(0, new ToolItem(type, level));
        }
    }
}
