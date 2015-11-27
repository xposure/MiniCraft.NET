using MiniCraft.Entities;
using MiniCraft.Items;

namespace MiniCraft.Crafts
{
    public class FurnitureRecipe<T> : Recipe
        where T : Furniture, new()
    {

        public FurnitureRecipe()
            : base(new FurnitureItem(new T()))
        {
        }

        public override void craft(Player player)
        {
            player.inventory.add(0, new FurnitureItem(new T()));
        }
    }
}
