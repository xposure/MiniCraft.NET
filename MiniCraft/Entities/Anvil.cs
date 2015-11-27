using MiniCraft.Crafts;
using MiniCraft.Gfx;
using MiniCraft.Screens;

namespace MiniCraft.Entities
{
    public class Anvil : Furniture
    {
        public Anvil()
            : base("Anvil")
        {
            col = ColorHelper.get(-1, 000, 111, 222);
            sprite = 0;
            xr = 3;
            yr = 2;
        }

        public override bool use(Player player, int attackDir)
        {
            player.game.setMenu(new CraftingMenu(Crafting.anvilRecipes, player));
            return true;
        }
    }
}
