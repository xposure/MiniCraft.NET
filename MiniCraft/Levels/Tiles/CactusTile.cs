using MiniCraft.Entities;
using MiniCraft.Entities.Particles;
using MiniCraft.Gfx;
using MiniCraft.Items;
using MiniCraft.Items.Resources;

namespace MiniCraft.Levels.Tiles
{

    public class CactusTile : Tile
    {
        public CactusTile(TileID id)
            : base(id)
        {
            connectsToSand = true;
        }

        public override void render(Screen screen, Level level, int x, int y)
        {
            int col = ColorHelper.get(20, 40, 50, level.sandColor);
            screen.render(x * 16 + 0, y * 16 + 0, 8 + 2 * 32, col, 0);
            screen.render(x * 16 + 8, y * 16 + 0, 9 + 2 * 32, col, 0);
            screen.render(x * 16 + 0, y * 16 + 8, 8 + 3 * 32, col, 0);
            screen.render(x * 16 + 8, y * 16 + 8, 9 + 3 * 32, col, 0);
        }

        public override bool mayPass(Level level, int x, int y, Entity e)
        {
            return false;
        }

        public override void hurt(Level level, int x, int y, Mob source, int dmg, int attackDir)
        {
            int damage = level.getData(x, y) + dmg;
            level.add(new SmashParticle(x * 16 + 8, y * 16 + 8));
            level.add(new TextParticle("" + dmg, x * 16 + 8, y * 16 + 8, ColorHelper.get(-1, 500, 500, 500)));
            if (damage >= 10)
            {
                int count = random.nextInt(2) + 1;
                for (int i = 0; i < count; i++)
                {
                    level.add(new ItemEntity(new ResourceItem(Resource.cactusFlower), x * 16 + random.nextInt(10) + 3, y * 16 + random.nextInt(10) + 3));
                }
                level.setTile(x, y, Tile.sand, 0);
            }
            else
            {
                level.setData(x, y, damage);
            }
        }

        public override void bumpedInto(Level level, int x, int y, Entity entity)
        {
            entity.hurt(this, x, y, 1);
        }

        public override void tick(Level level, int xt, int yt)
        {
            int damage = level.getData(xt, yt);
            if (damage > 0) level.setData(xt, yt, damage - 1);
        }
    }
}
