using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniCraft.Gfx;
using MiniCraft.Items;
using MiniCraft.Items.Resources;

namespace MiniCraft.Entities
{
    public class Slime : Mob
    {
        private int xa, ya;
        private int jumpTime = 0;
        private int lvl;

        public Slime(int lvl)
        {
            this.lvl = lvl;
            x = random.nextInt(64 * 16);
            y = random.nextInt(64 * 16);
            health = maxHealth = lvl * lvl * 5;
        }

        public override void tick()
        {
            base.tick();

            int speed = 1;
            if (!move(xa * speed, ya * speed) || random.nextInt(40) == 0)
            {
                if (jumpTime <= -10)
                {
                    xa = (random.nextInt(3) - 1);
                    ya = (random.nextInt(3) - 1);

                    if (level.player != null)
                    {
                        int xd = level.player.x - x;
                        int yd = level.player.y - y;
                        if (xd * xd + yd * yd < 50 * 50)
                        {
                            if (xd < 0) xa = -1;
                            if (xd > 0) xa = +1;
                            if (yd < 0) ya = -1;
                            if (yd > 0) ya = +1;
                        }

                    }

                    if (xa != 0 || ya != 0) jumpTime = 10;
                }
            }

            jumpTime--;
            if (jumpTime == 0)
            {
                xa = ya = 0;
            }
        }

        protected override void die()
        {
            base.die();

            int count = random.nextInt(2) + 1;
            for (int i = 0; i < count; i++)
            {
                level.add(new ItemEntity(new ResourceItem(Resource.slime), x + random.nextInt(11) - 5, y + random.nextInt(11) - 5));
            }

            if (level.player != null)
            {
                level.player.score += 25 * lvl;
            }

        }

        public override void render(Screen screen)
        {
            int xt = 0;
            int yt = 18;

            int xo = x - 8;
            int yo = y - 11;

            if (jumpTime > 0)
            {
                xt += 2;
                yo -= 4;
            }

            int col = ColorHelper.get(-1, 10, 252, 555);
            if (lvl == 2) col = ColorHelper.get(-1, 100, 522, 555);
            if (lvl == 3) col = ColorHelper.get(-1, 111, 444, 555);
            if (lvl == 4) col = ColorHelper.get(-1, 000, 111, 224);

            if (hurtTime > 0)
            {
                col = ColorHelper.get(-1, 555, 555, 555);
            }

            screen.render(xo + 0, yo + 0, xt + yt * 32, col, 0);
            screen.render(xo + 8, yo + 0, xt + 1 + yt * 32, col, 0);
            screen.render(xo + 0, yo + 8, xt + (yt + 1) * 32, col, 0);
            screen.render(xo + 8, yo + 8, xt + 1 + (yt + 1) * 32, col, 0);
        }

        public override void touchedBy(Entity entity)
        {
            if (entity is Player)
            {
                entity.hurt(this, lvl, dir);
            }
        }
    }
}
