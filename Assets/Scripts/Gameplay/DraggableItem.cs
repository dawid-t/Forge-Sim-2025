using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Critsoft.ForgeSim2025.Gameplay
{
    public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        #region Events

        public event Action<Slot> SlotChanged;

        #endregion

        #region Fields

        private Transform _originalParent;

        #endregion

        #region Properties

        public Transform OriginalParent => _originalParent;

        #endregion

        #region Public Methods

        public void ChangeSlot(Slot slot)
        {
            _originalParent = slot.transform;

            transform.SetParent(_originalParent);
            transform.localPosition = Vector3.zero;

            SlotChanged?.Invoke(slot);
        }

        public void OnBeginDrag(PointerEventData eventData) // IBeginDragHandler
        {
            _originalParent = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
        }

        public void OnDrag(PointerEventData eventData) // IDragHandler
        {
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData) // IEndDragHandler
        {
            transform.SetParent(_originalParent);
            transform.localPosition = Vector3.zero;
        }

        #endregion
    }
}
