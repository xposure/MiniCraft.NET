using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniCraft.Gfx;
using MiniCraft.Sounds;

namespace MiniCraft.Entities.Particles
{
    public class SmashParticle : Entity
    {
        private int time = 0;

        public SmashParticle(int x, int y)
        {
            this.x = x;
            this.y = y;
            Sound.monsterHurt.play();
        }

        public override void tick()
        {
            time++;
            if (time > 10)
            {
                remove();
            }
        }

        public override void render(Screen screen)
        {
            int col = ColorHelper.get(-1, 555, 555, 555);
            screen.render(x - 8, y - 8, 5 + 12 * 32, col, 2);
            screen.render(x - 0, y - 8, 5 + 12 * 32, col, 3);
            screen.render(x - 8, y - 0, 5 + 12 * 32, col, 0);
            screen.render(x - 0, y - 0, 5 + 12 * 32, col, 1);
        }
    }
}
