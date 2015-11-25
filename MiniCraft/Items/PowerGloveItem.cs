using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniCraft.Entities;
using MiniCraft.Gfx;

namespace MiniCraft.Items
{
    public class PowerGloveItem : Item
    {
        public override int getColor()
        {
            return ColorHelper.get(-1, 100, 320, 430);
        }

        public override int getSprite()
        {
            return 7 + 4 * 32;
        }

        public override void renderIcon(Screen screen, int x, int y, int bits = 0)
        {
            screen.render(x, y, getSprite(), getColor(), bits);
        }

        public override void renderInventory(Screen screen, int x, int y)
        {
            screen.render(x, y, getSprite(), getColor(), 0);
            Font.draw(getName(), screen, x + 8, y, ColorHelper.get(-1, 555, 555, 555));
        }

        public override String getName()
        {
            return "Pow glove";
        }

        public override bool interact(Player player, Entity entity, int attackDir)
        {
            if (entity is Furniture)
            {
                Furniture f = (Furniture)entity;
                f.take(player);
                return true;
            }
            return false;
        }
    }
}
