namespace WarCroft.Entities.Inventory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Items;
    using WarCroft.Constants;
    public abstract class Bag : IBag
    {
        private List<Item> items;
        protected Bag(int capacity)
        {
            Capacity = capacity;
            items = new List<Item>();
        }

        public int Capacity { get; set; } = 100;

        public int Load => items.Sum(x => x.Weight);

        public IReadOnlyCollection<Item> Items => this.items.AsReadOnly();

        public void AddItem(Item item)
        {
            if (this.Load + item.Weight > this.Capacity)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.ExceedMaximumBagCapacity));
            }
            items.Add(item);
        }

        public Item GetItem(string name)
        {
            if (!items.Any())
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.EmptyBag));
            }

            var item = items.FirstOrDefault(x => x.GetType().Name == name);

            if (item == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.ItemNotFoundInBag, name));
            }

            items.Remove(item);
            return item;
        }
    }
}
