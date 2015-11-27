using System.Collections.Generic;
using MiniCraft.Items;
using MiniCraft.Items.Resources;

namespace MiniCraft.Entities
{
    public class Inventory
    {
        public List<Item> items = new List<Item>();

        public void add(Item item)
        {
            add(items.Count, item);
        }

        public void add(int slot, Item item)
        {
            if (item is ResourceItem)
            {
                ResourceItem toTake = (ResourceItem)item;
                ResourceItem has = findResource(toTake.resource);
                if (has == null)
                {
                    items.Insert(slot, toTake);
                }
                else
                {
                    has.count += toTake.count;
                }
            }
            else
            {
                items.Insert(slot, item);
            }
        }

        private ResourceItem findResource(Resource resource)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] is ResourceItem)
                {
                    ResourceItem has = (ResourceItem)items[i];
                    if (has.resource == resource) return has;
                }
            }
            return null;
        }

        public bool hasResources(Resource r, int count)
        {
            ResourceItem ri = findResource(r);
            if (ri == null) return false;
            return ri.count >= count;
        }

        public bool removeResource(Resource r, int count)
        {
            ResourceItem ri = findResource(r);
            if (ri == null) return false;
            if (ri.count < count) return false;
            ri.count -= count;
            if (ri.count <= 0) items.Remove(ri);
            return true;
        }

        public int count(Item item)
        {
            if (item is ResourceItem)
            {
                ResourceItem ri = findResource(((ResourceItem)item).resource);
                if (ri != null) return ri.count;
            }
            else
            {
                int count = 0;
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].matches(item)) count++;
                }
                return count;
            }
            return 0;
        }
    }

}
