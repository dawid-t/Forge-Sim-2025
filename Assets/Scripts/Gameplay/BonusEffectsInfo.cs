using Critsoft.ForgeSim2025.Gameplay;
using Critsoft.ForgeSim2025.Gameplay.Controllers;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BonusEffectsInfo : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Transform _grid;
    [SerializeField] private GameObject _bonusEffectDescriptionPrefab;
    [SerializeField] private ItemsLibrarySO _itemsLibrary;

    private InventoryController _inventoryController;
    private List<ItemType> _itemsInUse = new List<ItemType>();

    [Inject]
    public void Construct(InventoryController inventoryController)
    {
        _inventoryController = inventoryController;
        _inventoryController.ItemAdded += OnItemAdded;
    }

    private void OnDestroy()
    {
        if (_inventoryController != null)
        {
            _inventoryController.ItemAdded -= OnItemAdded;
        }
    }

    private void OnItemAdded(ItemType itemType, int amount)
    {
        foreach (ItemDataSO item in _itemsLibrary.Items)
        {
            if (item.Type == itemType && item.HasBonusEffect && !_itemsInUse.Contains(itemType))
            {
                GameObject bonusEffectDescGO = Instantiate(_bonusEffectDescriptionPrefab, _grid);
                BonusEffectDescription BonusEffectDesc = bonusEffectDescGO.GetComponent<BonusEffectDescription>();
                
                BonusEffectDesc.DescriptionText.text = item.Description;
                _itemsInUse.Add(itemType);

                _canvas.enabled = true;
            }
        }
    }
}
