using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniCraft.Gfx;
using MiniCraft.Items;
using MiniCraft.Levels;
using MiniCraft.Levels.Tiles;

namespace MiniCraft.Entities
{
    public class Entity
    {
        protected readonly Random random = new Random();
        public int x, y;
        public int xr = 6;
        public int yr = 6;
        public bool removed;
        public Level level;

        public virtual void render(Screen screen)
        {
        }

        public virtual void tick()
        {
        }

        public virtual void remove()
        {
            removed = true;
        }

        public virtual void init(Level level)
        {
            this.level = level;
        }

        public virtual bool intersects(int x0, int y0, int x1, int y1)
        {
            return !(x + xr < x0 || y + yr < y0 || x - xr > x1 || y - yr > y1);
        }

        public virtual bool blocks(Entity e)
        {
            return false;
        }

        public virtual void hurt(Mob mob, int dmg, int attackDir)
        {
        }

        public virtual void hurt(Tile tile, int x, int y, int dmg)
        {
        }

        public virtual bool move(int xa, int ya)
        {
            if (xa != 0 || ya != 0)
            {
                bool stopped = true;
                if (xa != 0 && move2(xa, 0)) stopped = false;
                if (ya != 0 && move2(0, ya)) stopped = false;
                if (!stopped)
                {
                    int xt = x >> 4;
                    int yt = y >> 4;
                    level.getTile(xt, yt).steppedOn(level, xt, yt, this);
                }
                return !stopped;
            }
            return true;
        }

        protected virtual bool move2(int xa, int ya)
        {
            if (xa != 0 && ya != 0) throw new InvalidOperationException("Move2 can only move along one axis at a time!");

            int xto0 = ((x) - xr) >> 4;
            int yto0 = ((y) - yr) >> 4;
            int xto1 = ((x) + xr) >> 4;
            int yto1 = ((y) + yr) >> 4;

            int xt0 = ((x + xa) - xr) >> 4;
            int yt0 = ((y + ya) - yr) >> 4;
            int xt1 = ((x + xa) + xr) >> 4;
            int yt1 = ((y + ya) + yr) >> 4;
            bool blocked = false;
            for (int yt = yt0; yt <= yt1; yt++)
                for (int xt = xt0; xt <= xt1; xt++)
                {
                    if (xt >= xto0 && xt <= xto1 && yt >= yto0 && yt <= yto1) continue;
                    level.getTile(xt, yt).bumpedInto(level, xt, yt, this);
                    if (!level.getTile(xt, yt).mayPass(level, xt, yt, this))
                    {
                        blocked = true;
                        return false;
                    }
                }
            if (blocked) return false;

            List<Entity> wasInside = level.getEntities(x - xr, y - yr, x + xr, y + yr);
            List<Entity> isInside = level.getEntities(x + xa - xr, y + ya - yr, x + xa + xr, y + ya + yr);
            for (int i = 0; i < isInside.Count; i++)
            {
                Entity e = isInside[i];
                if (e == this) continue;

                e.touchedBy(this);
            }
            isInside.removeAll(wasInside);
            for (int i = 0; i < isInside.Count; i++)
            {
                Entity e = isInside.get(i);
                if (e == this) continue;

                if (e.blocks(this))
                {
                    return false;
                }
            }

            x += xa;
            y += ya;
            return true;
        }

        //TODO: was protected
        public virtual void touchedBy(Entity entity)
        {
        }

        public virtual bool isBlockableBy(Mob mob)
        {
            return true;
        }

        public virtual void touchItem(ItemEntity itemEntity)
        {
        }

        public virtual bool canSwim()
        {
            return false;
        }

        public virtual bool interact(Player player, Item item, int attackDir)
        {
            return item.interact(player, this, attackDir);
        }

        public virtual bool use(Player player, int attackDir)
        {
            return false;
        }

        public virtual int getLightRadius()
        {
            return 0;
        }
    }
}
