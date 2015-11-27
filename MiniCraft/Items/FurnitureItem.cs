using System;
using MiniCraft.Entities;
using MiniCraft.Gfx;
using MiniCraft.Levels;
using MiniCraft.Levels.Tiles;

namespace MiniCraft.Items
{
    public class FurnitureItem : Item
    {
        public Furniture furniture;
        public bool placed = false;

        public FurnitureItem(Furniture furniture)
        {
            this.furniture = furniture;
        }

        public override int getColor()
        {
            return furniture.col;
        }

        public override int getSprite()
        {
            return furniture.sprite + 10 * 32;
        }

        public override void renderIcon(Screen screen, int x, int y, int bits = 0)
        {
            screen.render(x, y, getSprite(), getColor(), bits);
        }

        public override void renderInventory(Screen screen, int x, int y)
        {
            screen.render(x, y, getSprite(), getColor(), 0);
            Font.draw(furniture.name, screen, x + 8, y, ColorHelper.get(-1, 555, 555, 555));
        }

        public override void onTake(ItemEntity itemEntity)
        {
        }

        public override bool canAttack()
        {
            return false;
        }

        public override bool interactOn(Tile tile, Level level, int xt, int yt, Player player, int attackDir)
        {
            if (tile.mayPass(level, xt, yt, furniture))
            {
                furniture.x = xt * 16 + 8;
                furniture.y = yt * 16 + 8;
                level.add(furniture);
                placed = true;
                return true;
            }
            return false;
        }

        public override bool isDepleted()
        {
            return placed;
        }

        public override String getName()
        {
            return furniture.name;
        }
    }
}
