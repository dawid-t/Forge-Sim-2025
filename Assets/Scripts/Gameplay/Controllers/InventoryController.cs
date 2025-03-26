using Critsoft.ForgeSim2025.Gameplay.Models;
using Critsoft.ForgeSim2025.Gameplay.Quests;
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
        private QuestManager _questManager;

        #endregion

        #region Public Methods

        [Inject]
        public void Construct(InventoryModel model, InventoryView view, QuestManager questManager)
        {
            _model = model;
            _view = view;
            _questManager = questManager;

            _model.InventoryUpdated += OnInventoryUpdated;
            _model.ItemAdded += OnItemAdded;
            _model.ItemRemoved += OnItemRemoved;

            _questManager.AllQuestsFinished += OnAllQuestsFinished;
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

        private void Start()
        {
            InitRandomItems();
        }

        private void OnDestroy()
        {
            if (_model != null)
            {
                _model.InventoryUpdated -= OnInventoryUpdated;
                _model.ItemAdded -= OnItemAdded;
                _model.ItemRemoved -= OnItemRemoved;
            }

            if (_questManager != null)
            {
                _questManager.AllQuestsFinished -= OnAllQuestsFinished;
            }
        }

        private void InitRandomItems()
        {
            // Normal items
            int randomQuantity = UnityEngine.Random.Range(GameConfig.IronOreInitRandomMin, GameConfig.IronOreInitRandomMax + 1);
            AddItem(ItemType.IronOre, randomQuantity);

            randomQuantity = UnityEngine.Random.Range(GameConfig.GoldOreInitRandomMin, GameConfig.GoldOreInitRandomMax + 1);
            AddItem(ItemType.GoldOre, randomQuantity);

            randomQuantity = UnityEngine.Random.Range(GameConfig.FireShardInitRandomMin, GameConfig.FireShardInitRandomMax + 1);
            AddItem(ItemType.FireShard, randomQuantity);

            randomQuantity = UnityEngine.Random.Range(GameConfig.EmberDustInitRandomMin, GameConfig.EmberDustInitRandomMax + 1);
            AddItem(ItemType.EmberDust, randomQuantity);

            randomQuantity = UnityEngine.Random.Range(GameConfig.DragonScaleInitRandomMin, GameConfig.DragonScaleInitRandomMax + 1);
            AddItem(ItemType.DragonScale, randomQuantity);

            // Bonus items
            float randomPercentage = UnityEngine.Random.Range(0f, 1f);
            if (randomPercentage <= GameConfig.LuckyCharmInitPercentageChance)
            {
                AddItem(ItemType.LuckyCharm);
            }

            randomPercentage = UnityEngine.Random.Range(0f, 1f);
            if (randomPercentage <= GameConfig.TimeAmuletInitPercentageChance)
            {
                AddItem(ItemType.TimeAmulet);
            }
        }

        private void AddRandomBonusItem()
        {
            int randomBonusItemIndex = UnityEngine.Random.Range(0, 2); // todo: Get items from ItemsLibrary instead of hardcoding them like this
            switch (randomBonusItemIndex)
            {
                case 0:
                    AddItem(ItemType.LuckyCharm);
                    break;

                case 1:
                default:
                    AddItem(ItemType.TimeAmulet);
                    break;
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

        private void OnAllQuestsFinished()
        {
            AddRandomBonusItem();
        }

        #endregion
    }
}
