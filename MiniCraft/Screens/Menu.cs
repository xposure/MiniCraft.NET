using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniCraft.Gfx;

namespace MiniCraft.Screens
{
    public class Menu
    {
        protected MCGame game;
        protected InputHandler input;

        public void init(MCGame game, InputHandler input)
        {
            this.input = input;
            this.game = game;
        }

        public virtual void tick()
        {
        }

        public virtual void render(Screen screen)
        {
        }

        public void renderItemList(Screen screen, int xo, int yo, int x1, int y1, IEnumerable<ListItem> listItemsIter, int selected)
        {
            var listItems = listItemsIter.ToArray();

            bool renderCursor = true;
            if (selected < 0)
            {
                selected = -selected - 1;
                renderCursor = false;
            }
            int w = x1 - xo;
            int h = y1 - yo - 1;
            int i0 = 0;
            int i1 = listItems.Length;
            if (i1 > h) i1 = h;
            int io = selected - h / 2;
            if (io > listItems.Length - h) io = listItems.Length - h;
            if (io < 0) io = 0;

            for (int i = i0; i < i1; i++)
            {
                listItems[i + io].renderInventory(screen, (1 + xo) * 8, (i + 1 + yo) * 8);
            }

            if (renderCursor)
            {
                int yy = selected + 1 - io + yo;
                Font.draw(">", screen, (xo + 0) * 8, yy * 8, ColorHelper.get(5, 555, 555, 555));
                Font.draw("<", screen, (xo + w) * 8, yy * 8, ColorHelper.get(5, 555, 555, 555));
            }
        }
    }
}
