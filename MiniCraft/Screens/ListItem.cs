using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniCraft.Gfx;

namespace MiniCraft.Screens
{
    public interface ListItem
    {
        void renderInventory(Screen screen, int i, int j);
    }
}
