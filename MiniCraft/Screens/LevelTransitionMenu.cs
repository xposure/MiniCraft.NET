using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniCraft.Gfx;

namespace MiniCraft.Screens
{

    public class LevelTransitionMenu : Menu
    {
        private int dir;
        private int time = 0;

        public LevelTransitionMenu(int dir)
        {
            this.dir = dir;
        }

        public override void tick()
        {
            time += 2;
            if (time == 30) game.changeLevel(dir);
            if (time == 60) game.setMenu(null);
        }

        public override void render(Screen screen)
        {
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 15; y++)
                {
                    int dd = (y + x % 2 * 2 + x / 3) - time;
                    if (dd < 0 && dd > -30)
                    {
                        if (dir > 0)
                            screen.render(x * 8, y * 8, 0, 0, 0);
                        else
                            screen.render(x * 8, screen.h - y * 8 - 8, 0, 0, 0);
                    }
                }
            }
        }
    }

}
