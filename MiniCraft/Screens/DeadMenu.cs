﻿using System;
using MiniCraft.Gfx;

namespace MiniCraft.Screens
{

    public class DeadMenu : Menu
    {
        private int inputDelay = 60;

        public DeadMenu()
        {
        }

        public override void tick()
        {
            if (inputDelay > 0)
                inputDelay--;
            else if (input.attack.clicked || input.menu.clicked)
            {
                game.setMenu(new TitleMenu());
            }
        }

        public override void render(Screen screen)
        {
            Font.renderFrame(screen, "", 1, 3, 18, 9);
            Font.draw("You died! Aww!", screen, 2 * 8, 4 * 8, ColorHelper.get(-1, 555, 555, 555));

            int seconds = game.gameTime / 60;
            int minutes = seconds / 60;
            int hours = minutes / 60;
            minutes %= 60;
            seconds %= 60;

            string timeString = "";
            if (hours > 0)
            {
                timeString = hours + "h" + (minutes < 10 ? "0" : "") + minutes + "m";
            }
            else
            {
                timeString = minutes + "m " + (seconds < 10 ? "0" : "") + seconds + "s";
            }
            Font.draw("Time:", screen, 2 * 8, 5 * 8, ColorHelper.get(-1, 555, 555, 555));
            Font.draw(timeString, screen, (2 + 5) * 8, 5 * 8, ColorHelper.get(-1, 550, 550, 550));
            Font.draw("Score:", screen, 2 * 8, 6 * 8, ColorHelper.get(-1, 555, 555, 555));
            Font.draw("" + game.player.score, screen, (2 + 6) * 8, 6 * 8, ColorHelper.get(-1, 550, 550, 550));
            Font.draw("Press C to lose", screen, 2 * 8, 8 * 8, ColorHelper.get(-1, 333, 333, 333));
        }
    }
}
