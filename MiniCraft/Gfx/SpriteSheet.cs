using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniCraft.Gfx
{
    public class SpriteSheet
    {
        public int width, height;
        public int[] pixels;

        public SpriteSheet(Texture2D image)
        {
            width = image.Width;
            height = image.Height;

            var colors = new Color[width * height];
            image.GetData<Color>(colors);

            pixels = new int[width * height];
            for (int i = 0; i < pixels.Length; i++)
                pixels[i] = colors[i].B / 64;
        }
    }
}
