using Critsoft.ForgeSim2025.Gameplay.Controllers;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Critsoft.ForgeSim2025.Gameplay
{
    public class ResourceClickHandler : MonoBehaviour
    {
        #region Fields

        private Camera _mainCamera;
        InventoryController _inventoryController;

        #endregion

        #region Public Methods

        [Inject]
        public void Construct(InventoryController inventoryController)
        {
            _inventoryController = inventoryController;
        }

        #endregion

        #region Private Methods

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!IsPointerOverUI())
                {
                    TryCollectResource();
                }
            }
        }

        private bool IsPointerOverUI()
        {
            return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
        }

        private void TryCollectResource()
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
            if (hit.collider != null)
            {
                FallingResource resource = hit.collider.GetComponent<FallingResource>();
                if (resource != null)
                {
                    ItemType resourceType = resource.CollectResource();
                    _inventoryController.AddItem(resourceType);
                }
            }
        }

        #endregion
    }
}
