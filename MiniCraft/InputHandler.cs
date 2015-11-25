using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MiniCraft
{
    public class InputHandler : KeyListener
    {
        public class Key
        {            
            public int presses, absorbs;
            public bool down, clicked;

            public Key(InputHandler handler)
            {
                handler.keys.Add(this);
            }

            public void toggle(bool pressed)
            {
                if (pressed != down)
                {
                    down = pressed;
                }
                if (pressed)
                {
                    presses++;
                }
            }

            public virtual void tick()
            {
                if (absorbs < presses)
                {
                    absorbs++;
                    clicked = true;                  
                }
                else
                {
                    clicked = false;
                }
            }
        }

        private List<Key> keys = new List<Key>();

        public Key up;
        public Key down;
        public Key left;
        public Key right;
        public Key attack;
        public Key menu;

        public InputHandler()
        {
            up = new Key(this);
            down = new Key(this);
            left = new Key(this);
            right = new Key(this);
            attack = new Key(this);
            menu = new Key(this);
        }

        public void releaseAll()
        {
            for (int i = 0; i < keys.Count; i++)
            {
                keys[i].down = false;
            }
        }

        public override void tick()
        {
            base.tick();

            for (int i = 0; i < keys.Count; i++)
            {
                keys[i].tick();
            }
        }

        public override void keyPressed(Keys ke)
        {
            toggle(ke, true);
        }

        public override void keyReleased(Keys ke)
        {
            toggle(ke, false);
        }

        private void toggle(Keys ke, bool pressed)
        {
            if (ke == Keys.NumPad8) up.toggle(pressed);
            if (ke == Keys.NumPad2) down.toggle(pressed);
            if (ke == Keys.NumPad4) left.toggle(pressed);
            if (ke == Keys.NumPad6) right.toggle(pressed);
            if (ke == Keys.W) up.toggle(pressed);
            if (ke == Keys.S) down.toggle(pressed);
            if (ke == Keys.A) left.toggle(pressed);
            if (ke == Keys.D) right.toggle(pressed);
            if (ke == Keys.Up) up.toggle(pressed);
            if (ke == Keys.Down) down.toggle(pressed);
            if (ke == Keys.Left) left.toggle(pressed);
            if (ke == Keys.Right) right.toggle(pressed);

            if (ke == Keys.Tab) menu.toggle(pressed);
            if (ke == Keys.LeftAlt) menu.toggle(pressed);
            if (ke == Keys.RightAlt) menu.toggle(pressed);
            if (ke == Keys.Space) attack.toggle(pressed);
            if (ke == Keys.LeftControl) attack.toggle(pressed);
            if (ke == Keys.NumPad0) attack.toggle(pressed);
            if (ke == Keys.Insert) attack.toggle(pressed);
            if (ke == Keys.Enter) menu.toggle(pressed);

            if (ke == Keys.X) menu.toggle(pressed);
            if (ke == Keys.C) attack.toggle(pressed);
        }
    }
}
