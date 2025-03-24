using Critsoft.ForgeSim2025.Gameplay.Controllers;
using Critsoft.ForgeSim2025.Gameplay.Quests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Critsoft.ForgeSim2025.Gameplay
{
    public class Machine : MonoBehaviour
    {
        #region Events

        public event Action<Machine> MachineUnlocked;

        #endregion

        #region Serialized Fields

        [SerializeField] private bool _isLocked;
        [SerializeField] private MachineType _type;
        [SerializeField] private List<RecipeDataSO> _craftingRecipes;
        [SerializeField] private List<MachineSlot> _slots;
        [Space]
        [SerializeField] private Image _machineImage;
        [SerializeField] private Button _forgeButton;
        [SerializeField] private TMP_Text _forgeButtonText;

        #endregion

        #region Fields

        private const float CraftTimerTick = 0.1f;
        private const string ForgeButtonText = "Forge";

        private bool _isCraftingInProgress;
        private float _remainingCraftingTime;
        private WaitForSeconds _craftWaitForSeconds = new WaitForSeconds(CraftTimerTick);
        private InventoryController _inventoryController;
        private QuestManager _questManager;

        #endregion

        #region Properties

        public List<RecipeDataSO> CraftingRecipes => _craftingRecipes;

        #endregion

        #region Public Methods

        [Inject]
        public void Construct(InventoryController inventoryController, QuestManager questManager)
        {
            _inventoryController = inventoryController;
            _questManager = questManager;

            _questManager.QuestFinished += OnQuestFinished;
        }

        public void OnButtonClicked()
        {
            StartCraftingItem();
        }

        public void Unlock()
        {
            _isLocked = false;
            _machineImage.color = Color.white;
            _forgeButton.enabled = true;
        }

        #endregion

        #region Private Methods

        private void Awake()
        {
            if (_isLocked)
            {
                _machineImage.color = new Color(0.5f, 0.5f, 0.5f, 0.2f);
                _forgeButton.enabled = false;
            }
        }

        private void OnDestroy()
        {
            if (_questManager != null)
            {
                _questManager.QuestFinished -= OnQuestFinished;
            }
        }

        private void StartCraftingItem()
        {
            if (_isLocked || _isCraftingInProgress)
                return;

            List<ItemType> resourcesInSlots = new List<ItemType>();
            foreach (MachineSlot slot in _slots)
            {
                if (!slot.IsEmpty)
                {
                    resourcesInSlots.Add(slot.Item.Type);
                }
                else
                {
                    return;
                }
            }

            bool isResourceValid = false;
            RecipeDataSO recipeInUse = null;
            foreach (RecipeDataSO recipe in _craftingRecipes)
            {
                foreach (ItemType resource in recipe.Resources)
                {
                    if (resourcesInSlots.Contains(resource))
                    {
                        isResourceValid = true;
                        recipeInUse = recipe;
                        break;
                    }
                }

                if (isResourceValid)
                    break;
            }

            if (!isResourceValid)
                return;

            StartCoroutine(CraftItem(recipeInUse));
        }

        private IEnumerator CraftItem(RecipeDataSO recipeInUse)
        {
            // Start
            _isCraftingInProgress = true;
            _forgeButton.enabled = false;

            foreach (ItemType resource in recipeInUse.Resources)
            {
                Item remainingResource = _slots.Select(slot => slot.Item).FirstOrDefault(item => item.Type == resource);
                _inventoryController.ReturnItemToFreeSlot(remainingResource);
                _inventoryController.RemoveItem(resource);
            }

            // In progress...
            _remainingCraftingTime = recipeInUse.TimeInSeconds;
            while (_remainingCraftingTime > 0)
            {
                yield return _craftWaitForSeconds;

                _remainingCraftingTime -= CraftTimerTick;
                _forgeButtonText.text = $"{_remainingCraftingTime:F1}s";
            }

            // Finish
            float successPercentage = UnityEngine.Random.Range(0f, 1f);
            if (successPercentage <= recipeInUse.SuccessRate)
            {
                ItemType craftedItem = recipeInUse.ResultItem;
                _inventoryController.AddItem(craftedItem);
            }

            _isCraftingInProgress = false;
            _forgeButton.enabled = true;
            _forgeButtonText.text = ForgeButtonText;
        }

        private void OnQuestFinished(int questId, MachineType machineType)
        {
            if (_isLocked && machineType == _type)
            {
                Unlock();
                MachineUnlocked?.Invoke(this);
            }
        }

        #endregion
    }
}
