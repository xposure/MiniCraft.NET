using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniCraft.Entities;
using MiniCraft.Gfx;
using MiniCraft.Levels;
using MiniCraft.Levels.Tiles;
using MiniCraft.Screens;

namespace MiniCraft
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MCGame : Game
    {
        private const long serialVersionUID = 1L;
        private Random random = new Random();
        public const string NAME = "Minicraft";
        public const int HEIGHT = 120 ;
        public const int WIDTH = 160 ;
        public const int SCALE = 4;

        private Texture2D image;
        private Color[] pixels;
        private bool running = false;
        private Screen screen;
        private Screen lightScreen;
        private InputHandler input;

        private Color[] colors;
        private int tickCount;
        public int gameTime = 0;

        private Level level;
        private Level[] levels = new Level[5];
        private int currentLevel = 3;
        public Player player;

        public Menu menu;
        private int playerDeadTime;
        private int pendingLevelChange;
        private int wonTimer = 0;
        public bool hasWon = false;


        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public bool hasFocus() { return this.IsActive; }

        public void setMenu(Menu menu)
        {
            this.menu = menu;
            if (menu != null) menu.init(this, input);
        }

        public MCGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = HEIGHT * SCALE;
            graphics.PreferredBackBufferWidth = WIDTH * SCALE;

            Content.RootDirectory = "Content";
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            running = false;
            base.OnExiting(sender, args);
        }

        public void resetGame()
        {

            playerDeadTime = 0;
            wonTimer = 0;
            gameTime = 0;
            hasWon = false;

            levels = new Level[5];
            currentLevel = 3;

            levels[4] = new Level(128, 128, 1, null);
            levels[3] = new Level(128, 128, 0, levels[4]);
            levels[2] = new Level(128, 128, -1, levels[3]);
            levels[1] = new Level(128, 128, -2, levels[2]);
            levels[0] = new Level(128, 128, -3, levels[1]);

            level = levels[currentLevel];
            player = new Player(this, input);
            player.findStartPos(level);

            level.add(player);

            for (int i = 0; i < 5; i++)
            {
                levels[i].trySpawn(5000);
            }
        }

        protected override void LoadContent()
        {
            running = true;

            var pp = 0;
            colors = new Color[256];

            for (int r = 0; r < 6; r++)
            {
                for (int g = 0; g < 6; g++)
                {
                    for (int b = 0; b < 6; b++)
                    {
                        int rr = (r * 255 / 5);
                        int gg = (g * 255 / 5);
                        int bb = (b * 255 / 5);
                        int mid = (rr * 30 + gg * 59 + bb * 11) / 100;

                        int r1 = ((rr + mid * 1) / 2) * 230 / 255 + 10;
                        int g1 = ((gg + mid * 1) / 2) * 230 / 255 + 10;
                        int b1 = ((bb + mid * 1) / 2) * 230 / 255 + 10;
                        colors[pp++] = new Color(r1, g1, b1);
                    }
                }
            }

            while (pp < 256)
            {
                colors[pp++] = Color.Black;
            }

            var spriteSheet = Content.Load<Texture2D>("Textures/icons");
            screen = new Screen(WIDTH, HEIGHT, new SpriteSheet(spriteSheet));
            lightScreen = new Screen(WIDTH, HEIGHT, new SpriteSheet(spriteSheet));

            Sounds.Sound.Initialize(this.Content);
            spriteBatch = new SpriteBatch(GraphicsDevice);

            pixels = new Color[WIDTH * HEIGHT];
            image = new Texture2D(GraphicsDevice, WIDTH, HEIGHT);
            input = new InputHandler();

            resetGame();
            setMenu(new TitleMenu());

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (running)
            {
                //assuming 60 updates from monogame
                tick();

                base.Update(gameTime);
            }
        }

        public virtual void tick()
        {
            tickCount++;
            if (!hasFocus())
            {
                input.releaseAll();
            }
            else
            {
                if (!player.removed && !hasWon) gameTime++;

                input.tick();
                if (menu != null)
                {
                    menu.tick();
                }
                else
                {
                    if (player.removed)
                    {
                        playerDeadTime++;
                        if (playerDeadTime > 60)
                        {
                            setMenu(new DeadMenu());
                        }
                    }
                    else
                    {
                        if (pendingLevelChange != 0)
                        {
                            setMenu(new LevelTransitionMenu(pendingLevelChange));
                            pendingLevelChange = 0;
                        }
                    }
                    if (wonTimer > 0)
                    {
                        if (--wonTimer == 0)
                        {
                            setMenu(new WonMenu());
                        }
                    }
                    level.tick();
                    Tile.tickCount++;
                }
            }
        }

        private void renderFocusNagger()
        {
            string msg = "Click to focus!";
            int xx = (WIDTH - msg.Length * 8) / 2;
            int yy = (HEIGHT - 8) / 2;
            int w = msg.Length;
            int h = 1;

            screen.render(xx - 8, yy - 8, 0 + 13 * 32, ColorHelper.get(-1, 1, 5, 445), 0);
            screen.render(xx + w * 8, yy - 8, 0 + 13 * 32, ColorHelper.get(-1, 1, 5, 445), 1);
            screen.render(xx - 8, yy + 8, 0 + 13 * 32, ColorHelper.get(-1, 1, 5, 445), 2);
            screen.render(xx + w * 8, yy + 8, 0 + 13 * 32, ColorHelper.get(-1, 1, 5, 445), 3);
            for (int x = 0; x < w; x++)
            {
                screen.render(xx + x * 8, yy - 8, 1 + 13 * 32, ColorHelper.get(-1, 1, 5, 445), 0);
                screen.render(xx + x * 8, yy + 8, 1 + 13 * 32, ColorHelper.get(-1, 1, 5, 445), 2);
            }
            for (int y = 0; y < h; y++)
            {
                screen.render(xx - 8, yy + y * 8, 2 + 13 * 32, ColorHelper.get(-1, 1, 5, 445), 0);
                screen.render(xx + w * 8, yy + y * 8, 2 + 13 * 32, ColorHelper.get(-1, 1, 5, 445), 1);
            }

            if ((tickCount / 20) % 2 == 0)
            {
                Font.draw(msg, screen, xx, yy, ColorHelper.get(5, 333, 333, 333));
            }
            else
            {
                Font.draw(msg, screen, xx, yy, ColorHelper.get(5, 555, 555, 555));
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            int xScroll = player.x - screen.w / 2;
            int yScroll = player.y - (screen.h - 8) / 2;
            if (xScroll < 16) xScroll = 16;
            if (yScroll < 16) yScroll = 16;
            if (xScroll > level.w * 16 - screen.w - 16) xScroll = level.w * 16 - screen.w - 16;
            if (yScroll > level.h * 16 - screen.h - 16) yScroll = level.h * 16 - screen.h - 16;
            if (currentLevel > 3)
            {
                int col = ColorHelper.get(20, 20, 121, 121);
                for (int y = 0; y < 14; y++)
                    for (int x = 0; x < 24; x++)
                    {
                        screen.render(x * 8 - ((xScroll / 4) & 7), y * 8 - ((yScroll / 4) & 7), 0, col, 0);
                    }
            }

            level.renderBackground(screen, xScroll, yScroll);
            level.renderSprites(screen, xScroll, yScroll);

            if (currentLevel < 3)
            {
                lightScreen.clear(0);
                level.renderLight(lightScreen, xScroll, yScroll);
                screen.overlay(lightScreen, xScroll, yScroll);
            }

            renderGui();

            if (!hasFocus()) renderFocusNagger();

            for (int y = 0; y < screen.h; y++)
            {
                for (int x = 0; x < screen.w; x++)
                {
                    int cc = screen.pixels[x + y * screen.w];
                    if (cc < 255) pixels[x + y * WIDTH] = colors[cc];
                }
            }
            image.SetData(pixels);

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(image, new Rectangle(0, 0, WIDTH * SCALE, HEIGHT * SCALE), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void renderGui()
        {
            for (int y = 0; y < 2; y++)
            {
                for (int x = 0; x < 20; x++)
                {
                    screen.render(x * 8, screen.h - 16 + y * 8, 0 + 12 * 32, ColorHelper.get(000, 000, 000, 000), 0);
                }
            }

            for (int i = 0; i < 10; i++)
            {
                if (i < player.health)
                    screen.render(i * 8, screen.h - 16, 0 + 12 * 32, ColorHelper.get(000, 200, 500, 533), 0);
                else
                    screen.render(i * 8, screen.h - 16, 0 + 12 * 32, ColorHelper.get(000, 100, 000, 000), 0);

                if (player.staminaRechargeDelay > 0)
                {
                    if (player.staminaRechargeDelay / 4 % 2 == 0)
                        screen.render(i * 8, screen.h - 8, 1 + 12 * 32, ColorHelper.get(000, 555, 000, 000), 0);
                    else
                        screen.render(i * 8, screen.h - 8, 1 + 12 * 32, ColorHelper.get(000, 110, 000, 000), 0);
                }
                else
                {
                    if (i < player.stamina)
                        screen.render(i * 8, screen.h - 8, 1 + 12 * 32, ColorHelper.get(000, 220, 550, 553), 0);
                    else
                        screen.render(i * 8, screen.h - 8, 1 + 12 * 32, ColorHelper.get(000, 110, 000, 000), 0);
                }
            }
            if (player.activeItem != null)
            {
                player.activeItem.renderInventory(screen, 10 * 8, screen.h - 16);
            }

            if (menu != null)
            {
                menu.render(screen);
            }
        }

        public void changeLevel(int dir)
        {
            level.remove(player);
            currentLevel += dir;
            level = levels[currentLevel];
            player.x = (player.x >> 4) * 16 + 8;
            player.y = (player.y >> 4) * 16 + 8;
            level.add(player);

        }

        public void scheduleLevelChange(int dir)
        {
            pendingLevelChange = dir;
        }

        public void won()
        {
            wonTimer = 60 * 3;
            hasWon = true;
        }
    }
}
