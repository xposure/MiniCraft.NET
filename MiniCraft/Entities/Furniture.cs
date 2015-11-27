using System;
using MiniCraft.Gfx;
using MiniCraft.Items;

namespace MiniCraft.Entities
{
    public class Furniture : Entity
    {
        private int pushTime = 0;
        private int pushDir = -1;
        public int col, sprite;
        public String name;
        private Player shouldTake;

        public Furniture(String name)
        {
            this.name = name;
            xr = 3;
            yr = 3;
        }

        public override void tick()
        {
            if (shouldTake != null)
            {
                if (shouldTake.activeItem is PowerGloveItem)
                {
                    remove();
                    shouldTake.inventory.add(0, shouldTake.activeItem);
                    shouldTake.activeItem = new FurnitureItem(this);
                }
                shouldTake = null;
            }
            if (pushDir == 0) move(0, +1);
            if (pushDir == 1) move(0, -1);
            if (pushDir == 2) move(-1, 0);
            if (pushDir == 3) move(+1, 0);
            pushDir = -1;
            if (pushTime > 0) pushTime--;
        }

        public override void render(Screen screen)
        {
            screen.render(x - 8, y - 8 - 4, sprite * 2 + 8 * 32, col, 0);
            screen.render(x - 0, y - 8 - 4, sprite * 2 + 8 * 32 + 1, col, 0);
            screen.render(x - 8, y - 0 - 4, sprite * 2 + 8 * 32 + 32, col, 0);
            screen.render(x - 0, y - 0 - 4, sprite * 2 + 8 * 32 + 33, col, 0);
        }

        public override bool blocks(Entity e)
        {
            return true;
        }

        public override void touchedBy(Entity entity)
        {
            if (entity is Player && pushTime == 0)
            {
                pushDir = ((Player)entity).dir;
                pushTime = 10;
            }
        }

        public void take(Player player)
        {
            shouldTake = player;
        }
    }
}
