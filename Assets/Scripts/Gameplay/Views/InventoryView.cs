using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Critsoft.ForgeSim2025.Gameplay.Controllers;

namespace Critsoft.ForgeSim2025.Gameplay.Views
{
    public class InventoryView : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField] private GameObject _itemPrefab;
        [SerializeField] private List<Slot> _slots;

        #endregion

        #region Fields

        private InventoryController _controller;

        #endregion

        #region Public Methods

        [Inject]
        public void Construct(InventoryController controller)
        {
            _controller = controller;

            _controller.InventoryUpdated += OnInventoryUpdated;
            _controller.ItemAdded += OnItemAdded;
            _controller.ItemRemoved += OnItemRemoved;
        }

        public void ReturnItemToFreeSlot(Item item)
        {
            for (int i = 0; i < _slots.Count; i++)
            {
                Slot slot = _slots[i];
                if (slot.IsEmpty)
                {
                    item.CurrentSlot.ClearSlot();
                    
                    DraggableItem draggableItem = item.GetComponent<DraggableItem>();
                    draggableItem.ChangeSlot(slot);
                    
                    slot.AddItem(item);
                    return;
                }
            }
        }

        #endregion

        #region Private Methods

        private void OnDestroy()
        {
            if (_controller != null)
            {
                _controller.InventoryUpdated -= OnInventoryUpdated;
                _controller.ItemAdded -= OnItemAdded;
                _controller.ItemRemoved -= OnItemRemoved;
            }
        }

        private void OnItemAdded(ItemType item, int quantity)
        {
            CreateOrUpdateItem(item, quantity);
        }

        private void OnItemRemoved(ItemType itemType, int quantity)
        {
            for (int i = 0; i < _slots.Count; i++)
            {
                Slot slot = _slots[i];
                if (!slot.IsEmpty && slot.Item.Type == itemType)
                {
                    slot.RemoveItem(quantity);
                }
            }
        }

        private void OnInventoryUpdated()
        {
            //todo: something later
        }

        private void CreateOrUpdateItem(ItemType itemType, int quantity)
        {
            // Try to find a slot with existing item and update its quantity
            for (int i = 0; i < _slots.Count; i++)
            {
                Slot slot = _slots[i];
                if (!slot.IsEmpty && slot.Item.Type == itemType)
                {
                    slot.AddItem(quantity);
                    return;
                }
            }

            // Create a new instance of Item
            for (int i = 0; i < _slots.Count; i++)
            {
                Slot slot = _slots[i];
                if (slot.IsEmpty)
                {
                    GameObject createdItemGO = Instantiate(_itemPrefab, slot.transform);
                    Item createdItem = createdItemGO.GetComponent<Item>();
                    createdItem.Init(itemType, slot, quantity);

                    slot.AddItem(createdItem);
                    return;
                }
            }
        }

        #endregion
    }
}
