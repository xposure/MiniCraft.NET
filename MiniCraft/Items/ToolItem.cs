using System;
using MiniCraft.Entities;
using MiniCraft.Gfx;

namespace MiniCraft.Items
{

    public class ToolItem : Item
    {
        private Random random = new Random();

        public const int MAX_LEVEL = 5;
        public static readonly String[] LEVEL_NAMES = { //
	        "Wood", "Rock", "Iron", "Gold", "Gem"//
	    };

        public static readonly int[] LEVEL_COLORS = {//
	        ColorHelper.get(-1, 100, 321, 431),//
			ColorHelper.get(-1, 100, 321, 111),//
			ColorHelper.get(-1, 100, 321, 555),//
			ColorHelper.get(-1, 100, 321, 550),//
			ColorHelper.get(-1, 100, 321, 055),//
	    };

        public ToolType type;
        public int level = 0;

        public ToolItem(ToolType type, int level)
        {
            this.type = type;
            this.level = level;
        }

        public override int getColor()
        {
            return LEVEL_COLORS[level];
        }

        public override int getSprite()
        {
            return type.sprite + 5 * 32;
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
            return LEVEL_NAMES[level] + " " + type.name;
        }

        public override void onTake(ItemEntity itemEntity)
        {
        }

        public override bool canAttack()
        {
            return true;
        }

        public override int getAttackDamageBonus(Entity e)
        {
            if (type == ToolType.axe)
            {
                return (level + 1) * 2 + random.nextInt(4);
            }
            if (type == ToolType.sword)
            {
                return (level + 1) * 3 + random.nextInt(2 + level * level * 2);
            }
            return 1;
        }

        public override bool matches(Item item)
        {
            if (item is ToolItem)
            {
                ToolItem other = (ToolItem)item;
                if (other.type != type) return false;
                if (other.level != level) return false;
                return true;
            }
            return false;
        }
    }
}
