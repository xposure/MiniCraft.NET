﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniCraft.Items
{
    public class ToolType
    {
        public static ToolType shovel = new ToolType("Shvl", 0);
        public static ToolType hoe = new ToolType("Hoe", 1);
        public static ToolType sword = new ToolType("Swrd", 2);
        public static ToolType pickaxe = new ToolType("Pick", 3);
        public static ToolType axe = new ToolType("Axe", 4);

        public readonly String name;
        public readonly int sprite;

        private ToolType(String name, int sprite)
        {
            this.name = name;
            this.sprite = sprite;
        }
    }
}
