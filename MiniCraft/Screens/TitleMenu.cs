using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniCraft.Gfx;
using MiniCraft.Items;
using MiniCraft.Items.Resources;
using MiniCraft.Sounds;

namespace MiniCraft.Screens
{
    public class TitleMenu : Menu
    {
        private int selected = 0;

        private static readonly String[] options = { "Start game", "How to play", "About" };

        public TitleMenu()
        {
        }

        public override void tick()
        {
            if (input.up.clicked) selected--;
            if (input.down.clicked) selected++;

            int len = options.Length;
            if (selected < 0) selected += len;
            if (selected >= len) selected -= len;

            if (input.attack.clicked || input.menu.clicked)
            {
                if (selected == 0)
                {
                    Sound.test.play();
                    game.resetGame();
                    game.setMenu(null);

                    //if (Resource.allResources != null)
                    //    foreach (var resource in Resource.allResources)
                            game.player.inventory.add(new ResourceItem(Resource.wood, 999));
                }
                if (selected == 1) game.setMenu(new InstructionsMenu(this));
                if (selected == 2) game.setMenu(new AboutMenu(this));
            }
        }

        public override void render(Screen screen)
        {
            screen.clear(0);

            int h = 2;
            int w = 13;
            int titleColor = ColorHelper.get(0, 010, 131, 551);
            int xo = (screen.w - w * 8) / 2;
            int yo = 24;
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    screen.render(xo + x * 8, yo + y * 8, x + (y + 6) * 32, titleColor, 0);
                }
            }

            for (int i = 0; i < 3; i++)
            {
                String msg = options[i];
                int col = ColorHelper.get(0, 222, 222, 222);
                if (i == selected)
                {
                    msg = "> " + msg + " <";
                    col = ColorHelper.get(0, 555, 555, 555);
                }
                Font.draw(msg, screen, (screen.w - msg.Length * 8) / 2, (8 + i) * 8, col);
            }

            Font.draw("(Arrow keys,X and C)", screen, 0, screen.h - 8, ColorHelper.get(0, 111, 111, 111));
        }
    }
}
