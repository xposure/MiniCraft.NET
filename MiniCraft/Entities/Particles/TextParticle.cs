﻿using System;
using MiniCraft.Gfx;

namespace MiniCraft.Entities.Particles
{
    public class TextParticle : Entity
    {
        private string msg;
        private int col;
        private int time = 0;
        public double xa, ya, za;
        public double xx, yy, zz;

        public TextParticle(string msg, int x, int y, int col)
        {
            this.msg = msg;
            this.x = x;
            this.y = y;
            this.col = col;
            xx = x;
            yy = y;
            zz = 2;
            xa = random.nextGaussian() * 0.3;
            ya = random.nextGaussian() * 0.2;
            za = random.nextFloat() * 0.7 + 2;
        }

        public override void tick()
        {
            time++;
            if (time > 60)
            {
                remove();
            }
            xx += xa;
            yy += ya;
            zz += za;
            if (zz < 0)
            {
                zz = 0;
                za *= -0.5;
                xa *= 0.6;
                ya *= 0.6;
            }
            za -= 0.15;
            x = (int)xx;
            y = (int)yy;
        }

        public override void render(Screen screen)
        {
            //		Font.draw(msg, screen, x - msg.length() * 4, y, ColorHelper.get(-1, 0, 0, 0));
            Font.draw(msg, screen, x - msg.length() * 4 + 1, y - (int)(zz) + 1, ColorHelper.get(-1, 0, 0, 0));
            Font.draw(msg, screen, x - msg.length() * 4, y - (int)(zz), col);
        }

    }
}
