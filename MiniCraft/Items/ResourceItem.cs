using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniCraft.Entities;
using MiniCraft.Gfx;
using MiniCraft.Items.Resources;
using MiniCraft.Levels;
using MiniCraft.Levels.Tiles;

namespace MiniCraft.Items
{
    public class ResourceItem : Item
    {
        public Resource resource;
        public int count = 1;

        public ResourceItem(Resource resource)
        {
            this.resource = resource;
        }

        public ResourceItem(Resource resource, int count)
        {
            this.resource = resource;
            this.count = count;
        }

        public override int getColor()
        {
            return resource.color;
        }

        public override int getSprite()
        {
            return resource.sprite;
        }

        public override void renderIcon(Screen screen, int x, int y, int bits = 0)
        {
            screen.render(x, y, resource.sprite, resource.color, bits);
        }

        public override void renderInventory(Screen screen, int x, int y)
        {
            screen.render(x, y, resource.sprite, resource.color, 0);
            Font.draw(resource.name, screen, x + 32, y, ColorHelper.get(-1, 555, 555, 555));
            int cc = count;
            if (cc > 999) cc = 999;
            Font.draw("" + cc, screen, x + 8, y, ColorHelper.get(-1, 444, 444, 444));
        }

        public override String getName()
        {
            return resource.name;
        }

        public override void onTake(ItemEntity itemEntity)
        {
        }

        public override bool interactOn(Tile tile, Level level, int xt, int yt, Player player, int attackDir)
        {
            if (resource.interactOn(tile, level, xt, yt, player, attackDir))
            {
                count--;
                return true;
            }
            return false;
        }

        public override bool isDepleted()
        {
            return count <= 0;
        }

    }
}
