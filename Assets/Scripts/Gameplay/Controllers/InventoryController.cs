using Critsoft.ForgeSim2025.Gameplay.Models;
using Critsoft.ForgeSim2025.Gameplay.Views;
using System;
using UnityEngine;
using Zenject;

namespace Critsoft.ForgeSim2025.Gameplay.Controllers
{
    public class InventoryController : MonoBehaviour
    {
        #region Events

        public event Action InventoryUpdated;
        public event Action<ItemType, int> ItemAdded;
        public event Action<ItemType, int> ItemRemoved;

        #endregion

        #region Fields

        private InventoryModel _model;
        private InventoryView _view;

        #endregion

        #region Public Methods

        [Inject]
        public void Construct(InventoryModel model, InventoryView view)
        {
            _model = model;
            _view = view;

            _model.InventoryUpdated += OnInventoryUpdated;
            _model.ItemAdded += OnItemAdded;
            _model.ItemRemoved += OnItemRemoved;
        }

        public void AddItem(ItemType itemType, int quantity = 1)
        {
            _model.AddItem(itemType, quantity);
        }
        public void RemoveItem(ItemType itemType, int quantity = 1)
        {
            _model.RemoveItem(itemType, quantity);
        }

        public int GetItemQuantity(ItemType itemType)
        {
            return _model.GetItemQuantity(itemType);
        }

        public void ReturnItemToFreeSlot(Item item)
        {
            _view.ReturnItemToFreeSlot(item);
        }

        #endregion

        #region Private Methods

        private void Start() // only for testing
        {
            AddItem(ItemType.FireShard, 2);
            AddItem(ItemType.IronOre, 6);
            AddItem(ItemType.FireShard, 2);
            AddItem(ItemType.DraconicCrown, 2);
            AddItem(ItemType.DragonScale, 5);
            //AddItem(ItemType.TimeAmulet);
        }

        private void OnDestroy()
        {
            if (_model != null)
            {
                _model.InventoryUpdated -= OnInventoryUpdated;
                _model.ItemAdded -= OnItemAdded;
                _model.ItemRemoved -= OnItemRemoved;
            }
        }

        private void OnInventoryUpdated()
        {
            InventoryUpdated?.Invoke();
        }

        private void OnItemAdded(ItemType itemType, int amount)
        {
            ItemAdded?.Invoke(itemType, amount);
        }

        private void OnItemRemoved(ItemType itemType, int amount)
        {
            ItemRemoved?.Invoke(itemType, amount);
        }

        #endregion
    }
}
