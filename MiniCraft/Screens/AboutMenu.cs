﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniCraft.Gfx;

namespace MiniCraft.Screens
{
    public class AboutMenu : Menu
    {
        private Menu parent;

        public AboutMenu(Menu parent)
        {
            this.parent = parent;
        }

        public override void tick()
        {
            if (input.attack.clicked || input.menu.clicked)
            {
                game.setMenu(parent);
            }
        }

        public override void render(Screen screen)
        {
            screen.clear(0);

            Font.draw("About Minicraft", screen, 2 * 8 + 4, 1 * 8, ColorHelper.get(0, 555, 555, 555));
            Font.draw("Minicraft was made", screen, 0 * 8 + 4, 3 * 8, ColorHelper.get(0, 333, 333, 333));
            Font.draw("by Markus Persson", screen, 0 * 8 + 4, 4 * 8, ColorHelper.get(0, 333, 333, 333));
            Font.draw("For the 22'nd ludum", screen, 0 * 8 + 4, 5 * 8, ColorHelper.get(0, 333, 333, 333));
            Font.draw("dare competition in", screen, 0 * 8 + 4, 6 * 8, ColorHelper.get(0, 333, 333, 333));
            Font.draw("december 2011.", screen, 0 * 8 + 4, 7 * 8, ColorHelper.get(0, 333, 333, 333));
            Font.draw("it is dedicated to", screen, 0 * 8 + 4, 9 * 8, ColorHelper.get(0, 333, 333, 333));
            Font.draw("my father. <3", screen, 0 * 8 + 4, 10 * 8, ColorHelper.get(0, 333, 333, 333));
        }
    }
}
