using System;
using System.Collections.Generic;

namespace Critsoft.ForgeSim2025.Gameplay.Models
{
    public class InventoryModel
    {
        #region Events

        public event Action InventoryUpdated;
        public event Action<ItemType, int> ItemAdded;
        public event Action<ItemType, int> ItemRemoved;

        #endregion

        #region Fields

        private Dictionary<ItemType, int> _items = new Dictionary<ItemType, int>();

        #endregion

        #region Public Methods

        public void AddItem(ItemType itemType, int quantity)
        {
            if (_items.ContainsKey(itemType))
            {
                _items[itemType] += quantity;
            }
            else
            {
                _items[itemType] = quantity;
            }

            ItemAdded?.Invoke(itemType, quantity);
            InventoryUpdated?.Invoke();
        }

        public void RemoveItem(ItemType itemType, int quantity)
        {
            if (!_items.ContainsKey(itemType))
                return;

            _items[itemType] -= quantity;

            if (_items[itemType] <= 0)
            {
                _items.Remove(itemType);
            }

            ItemRemoved?.Invoke(itemType, quantity);
            InventoryUpdated?.Invoke();
        }

        public int GetItemQuantity(ItemType itemType)
        {
            if (_items.ContainsKey(itemType))
            {
                return _items[itemType];
            }
            else
            {
                return 0;
            }
        }

        #endregion
    }
}
