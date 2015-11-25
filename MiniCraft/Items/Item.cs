using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniCraft.Entities;
using MiniCraft.Gfx;
using MiniCraft.Levels;
using MiniCraft.Levels.Tiles;
using MiniCraft.Screens;

namespace MiniCraft.Items
{
    public class Item : ListItem
    {
        public virtual int getColor()
        {
            return 0;
        }

        public virtual int getSprite()
        {
            return 0;
        }

        public virtual void onTake(ItemEntity itemEntity)
        {
        }

        public virtual void renderInventory(Screen screen, int x, int y)
        {
        }

        public virtual bool interact(Player player, Entity entity, int attackDir)
        {
            return false;
        }

        public virtual void renderIcon(Screen screen, int x, int y)
        {
        }

        public virtual bool interactOn(Tile tile, Level level, int xt, int yt, Player player, int attackDir)
        {
            return false;
        }

        public virtual bool isDepleted()
        {
            return false;
        }

        public virtual bool canAttack()
        {
            return false;
        }

        public virtual int getAttackDamageBonus(Entity e)
        {
            return 0;
        }

        public virtual String getName()
        {
            return "";
        }

        public virtual bool matches(Item item)
        {
            return item.GetType() == this.GetType();
        }
    }
}
