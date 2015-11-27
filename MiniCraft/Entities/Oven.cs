using MiniCraft.Crafts;
using MiniCraft.Gfx;
using MiniCraft.Screens;

namespace MiniCraft.Entities
{
    public class Oven : Furniture
    {
        public Oven()
            : base("Oven")
        {
            col = ColorHelper.get(-1, 000, 332, 442);
            sprite = 2;
            xr = 3;
            yr = 2;
        }

        public override bool use(Player player, int attackDir)
        {
            player.game.setMenu(new CraftingMenu(Crafting.ovenRecipes, player));
            return true;
        }
    }
}
