using UnityEngine;
using UnityEngine.EventSystems;

namespace Critsoft.ForgeSim2025.Gameplay
{
    public class Slot : MonoBehaviour, IDropHandler
    {
        #region Fields

        private bool _isEmpty = true;
        private Item _item;

        #endregion

        #region Properties

        public bool IsEmpty => _isEmpty;
        public Item Item => _item;

        #endregion

        #region Public Methods

        public void AddItem(Item item)
        {
            if (_isEmpty)
            {
                _item = item;
            }
            else if (_item.Type == item.Type)
            {
                _item.AddQuantity(item.Quantity);
            }

            _isEmpty = false;
        }

        public void AddItem(int quantity)
        {
            if (_item != null)
            {
                _item.AddQuantity(quantity);
            }
        }

        public int RemoveItem(int quantity)
        {
            if (_item != null)
            {
                if (quantity >= _item.Quantity)
                {
                    quantity = _item.Quantity;
                    _item = null;
                    _isEmpty = true;
                }

                _item.RemoveQuantity(quantity);
                return quantity;
            }

            return 0;
        }

        public void ClearSlot()
        {
            _item = null;
            _isEmpty = true;
        }

        public void OnDrop(PointerEventData eventData) // IDropHandler
        {
            if (!_isEmpty)
                return;

            GameObject droppedGO = eventData.pointerDrag;
            Item item = droppedGO.GetComponent<Item>();
            item.CurrentSlot.ClearSlot();

            DraggableItem draggableItem = droppedGO.GetComponent<DraggableItem>();
            draggableItem.ChangeSlot(this);

            _item = item;
            _isEmpty = false;
        }

        #endregion
    }
}
