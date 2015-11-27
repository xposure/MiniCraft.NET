using MiniCraft.Entities;
using MiniCraft.Items;
using MiniCraft.Items.Resources;

namespace MiniCraft.Crafts
{
    public class ResourceRecipe : Recipe
    {
        private Resource resource;

        public ResourceRecipe(Resource resource)
            : base(new ResourceItem(resource, 1))
        {
            this.resource = resource;
        }

        public override void craft(Player player)
        {
            player.inventory.add(0, new ResourceItem(resource, 1));
        }
    }
}
