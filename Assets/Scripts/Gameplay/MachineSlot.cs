using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Critsoft.ForgeSim2025.Gameplay
{
    public class MachineSlot : Slot
    {
        [SerializeField] private Machine _machine;
        private List<ItemType> _allowedResources = new List<ItemType>();

        private void Awake()
        {
            foreach (RecipeDataSO recipe in _machine.CraftingRecipes)
            {
                foreach (ItemType resourceType in recipe.Resources)
                {
                    _allowedResources.Add(resourceType);
                }
            }
        }

        public override void OnDrop(PointerEventData eventData)
        {
            GameObject droppedGO = eventData.pointerDrag;
            Item item = droppedGO.GetComponent<Item>();

            if (!_allowedResources.Contains(item.Type))
                return;

            base.OnDrop(eventData);
        }
    }
}
