using Microsoft.Xna.Framework.Input;

namespace MiniCraft
{
    public class KeyListener
    {
        private bool[] keys = new bool[256];
        private KeyboardState state;

        public KeyListener()
        {
            state = Keyboard.GetState();
        }

        public virtual void tick()
        {
            state = Keyboard.GetState();

            for (var i = 0; i < 256; i++)
            {
                var key = (Keys)i;
                if(keys[i] && state.IsKeyUp(key))
                {
                    keys[i] = false;
                    keyReleased(key);
                }
                else if (!keys[i] && state.IsKeyDown(key))
                {
                    keys[i] = true;
                    keyPressed(key);
                }
            }
        }

        public virtual void keyPressed(Keys ke)
        {
        }

        public virtual void keyReleased(Keys ke)
        {
        }
    }
}
