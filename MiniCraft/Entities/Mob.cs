using MiniCraft.Entities.Particles;
using MiniCraft.Gfx;
using MiniCraft.Levels;
using MiniCraft.Levels.Tiles;
using MiniCraft.Sounds;

namespace MiniCraft.Entities
{
    public class Mob : Entity
    {
        protected int walkDist = 0;

        public int dir { get; protected set; }
        public int hurtTime = 0;
        protected int xKnockback, yKnockback;
        public int maxHealth = 10;
        public int health = 10;
        public int swimTimer = 0;
        public int tickTime = 0;

        public Mob()
        {
            x = y = 8;
            xr = 4;
            yr = 3;
        }

        public override void tick()
        {
            tickTime++;
            if (level.getTile(x >> 4, y >> 4) == Tile.lava)
            {
                hurt(this, 4, dir ^ 1);
            }

            if (health <= 0)
            {
                die();
            }
            if (hurtTime > 0) hurtTime--;
        }

        protected virtual void die()
        {
            remove();
        }

        public override bool move(int xa, int ya)
        {
            if (isSwimming())
            {
                if (swimTimer++ % 2 == 0) return true;
            }
            if (xKnockback < 0)
            {
                move2(-1, 0);
                xKnockback++;
            }
            if (xKnockback > 0)
            {
                move2(1, 0);
                xKnockback--;
            }
            if (yKnockback < 0)
            {
                move2(0, -1);
                yKnockback++;
            }
            if (yKnockback > 0)
            {
                move2(0, 1);
                yKnockback--;
            }
            if (hurtTime > 0) return true;
            if (xa != 0 || ya != 0)
            {
                walkDist++;
                if (xa < 0) dir = 2;
                if (xa > 0) dir = 3;
                if (ya < 0) dir = 1;
                if (ya > 0) dir = 0;
            }
            return base.move(xa, ya);
        }

        protected bool isSwimming()
        {
            Tile tile = level.getTile(x >> 4, y >> 4);
            return tile == Tile.water || tile == Tile.lava;
        }

        public override bool blocks(Entity e)
        {
            return e.isBlockableBy(this);
        }

        public override void hurt(Tile tile, int x, int y, int damage)
        {
            int attackDir = dir ^ 1;
            doHurt(damage, attackDir);
        }

        public override void hurt(Mob mob, int damage, int attackDir)
        {
            doHurt(damage, attackDir);
        }

        public virtual void heal(int heal)
        {
            if (hurtTime > 0) return;

            level.add(new TextParticle("" + heal, x, y, ColorHelper.get(-1, 50, 50, 50)));
            health += heal;
            if (health > maxHealth) health = maxHealth;
        }

        protected virtual void doHurt(int damage, int attackDir)
        {
            if (hurtTime > 0) return;

            if (level.player != null)
            {
                int xd = level.player.x - x;
                int yd = level.player.y - y;
                if (xd * xd + yd * yd < 80 * 80)
                {
                    Sound.monsterHurt.play();
                }
            }
            level.add(new TextParticle("" + damage, x, y, ColorHelper.get(-1, 500, 500, 500)));
            health -= damage;
            if (attackDir == 0) yKnockback = +6;
            if (attackDir == 1) yKnockback = -6;
            if (attackDir == 2) xKnockback = -6;
            if (attackDir == 3) xKnockback = +6;
            hurtTime = 10;
        }

        public virtual bool findStartPos(Level level)
        {
            int x = random.nextInt(level.w);
            int y = random.nextInt(level.h);
            int xx = x * 16 + 8;
            int yy = y * 16 + 8;

            if (level.player != null)
            {
                int xd = level.player.x - xx;
                int yd = level.player.y - yy;
                if (xd * xd + yd * yd < 80 * 80) return false;
            }

            int r = level.monsterDensity * 16;
            if (level.getEntities(xx - r, yy - r, xx + r, yy + r).size() > 0) return false;

            if (level.getTile(x, y).mayPass(level, x, y, this))
            {
                this.x = xx;
                this.y = yy;
                return true;
            }

            return false;
        }
    }

}
