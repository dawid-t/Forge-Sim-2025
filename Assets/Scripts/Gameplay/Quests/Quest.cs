using Critsoft.ForgeSim2025.Gameplay.Controllers;
using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace Critsoft.ForgeSim2025.Gameplay.Quests
{
    public class Quest : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<Quest>
        { }

        #region Events

        public event Action<int> QuestProgressUpdated;
        public event Action<int, MachineType> QuestFinished;

        #endregion

        #region Serialized Fields

        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private TMP_Text _progressText;

        #endregion

        #region Fields

        private InventoryController _inventoryController;

        #endregion

        #region Properties

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public ItemType Requirement { get; private set; }
        public int Progress { get; private set; }
        public int ProgressRequired { get; private set; }
        public MachineType Reward { get; private set; }
        public TMP_Text DescriptionText => _descriptionText;
        public TMP_Text ProgressText => _progressText;

        #endregion

        #region Public Methods

        [Inject]
        public void Construct(InventoryController inventoryController)
        {
            _inventoryController = inventoryController;
            _inventoryController.ItemAdded += OnItemAdded;
        }

        public void SetQuestData(QuestDataSO questData)
        {
            Id = questData.Id;
            Name = questData.Name;
            Description = questData.Description;
            Requirement = questData.Requirement;
            ProgressRequired = questData.ProgressRequired;
            Reward = questData.Reward;

            DescriptionText.text = questData.Description;
            ProgressText.text = "0/" + questData.ProgressRequired;
        }

        #endregion

        #region Private Methods

        private void OnDestroy()
        {
            if (_inventoryController != null)
            {
                _inventoryController.ItemAdded -= OnItemAdded;
            }
        }

        private void OnItemAdded(ItemType itemType, int quantity)
        {
            if (itemType == Requirement)
            {
                QuestProgressUpdated?.Invoke(Id);

                Progress++;
                ProgressText.text = Progress + "/" + ProgressRequired;

                if (Progress >= ProgressRequired)
                {
                    QuestFinished?.Invoke(Id, Reward);
                }
            }
        }

        #endregion
    }
}
