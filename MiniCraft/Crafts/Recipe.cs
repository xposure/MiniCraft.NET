using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniCraft.Entities;
using MiniCraft.Gfx;
using MiniCraft.Items;
using MiniCraft.Items.Resources;
using MiniCraft.Screens;

namespace MiniCraft.Crafts
{
    public abstract class Recipe : ListItem
    {
        public List<Item> costs = new List<Item>();
        public bool canCraft = false;
        public Item resultTemplate;

        public Recipe(Item resultTemplate)
        {
            this.resultTemplate = resultTemplate;
        }

        public Recipe addCost(Resource resource, int count)
        {
            costs.add(new ResourceItem(resource, count));
            return this;
        }

        public void checkCanCraft(Player player)
        {
            for (int i = 0; i < costs.size(); i++)
            {
                Item item = costs.get(i);
                if (item is ResourceItem)
                {
                    ResourceItem ri = (ResourceItem)item;
                    if (!player.inventory.hasResources(ri.resource, ri.count))
                    {
                        canCraft = false;
                        return;
                    }
                }
            }
            canCraft = true;
        }

        public virtual void renderInventory(Screen screen, int x, int y)
        {
            screen.render(x, y, resultTemplate.getSprite(), resultTemplate.getColor(), 0);
            int textColor = canCraft ? ColorHelper.get(-1, 555, 555, 555) : ColorHelper.get(-1, 222, 222, 222);
            Font.draw(resultTemplate.getName(), screen, x + 8, y, textColor);
        }

        public abstract void craft(Player player);

        public void deductCost(Player player)
        {
            for (int i = 0; i < costs.size(); i++)
            {
                Item item = costs.get(i);
                if (item is ResourceItem)
                {
                    ResourceItem ri = (ResourceItem)item;
                    player.inventory.removeResource(ri.resource, ri.count);
                }
            }
        }
    }
}
