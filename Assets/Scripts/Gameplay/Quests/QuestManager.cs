using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Critsoft.ForgeSim2025.Gameplay.Quests
{
    public class QuestManager : MonoBehaviour
    {
        #region Events

        public event Action AllQuestsFinished;
        public event Action<int, MachineType> QuestFinished;

        #endregion

        #region Serialized Fields

        [SerializeField] private QuestsLibrarySO _questsLibrary;
        [SerializeField] private GameObject _questPrefab;
        [SerializeField] private Transform _questsGrid;

        #endregion

        #region Fields

        private Quest.Factory _questFactory;
        private List<Quest> _quests = new List<Quest>();

        #endregion

        #region Public Methods

        [Inject]
        public void Construct(Quest.Factory questFactory)
        {
            _questFactory = questFactory;
        }

        #endregion

        #region Private Methods

        private void Start()
        {
            CreateQuests();
        }

        private void OnDestroy()
        {
            foreach (var quest in _quests)
            {
                if (quest != null)
                {
                    quest.QuestFinished -= OnQuestFinished;
                }
            }
        }

        private void CreateQuests()
        {
            foreach (QuestDataSO questData in _questsLibrary.Quests)
            {
                Quest quest = _questFactory.Create();
                quest.SetQuestData(questData);
                quest.QuestFinished += OnQuestFinished;
                _quests.Add(quest);
            }
        }

        private void OnQuestFinished(int questId, MachineType machineType)
        {
            QuestFinished?.Invoke(questId, machineType);

            int lastQuestId = _quests[_quests.Count - 1].Id;
            if (lastQuestId == questId)
            {
                AllQuestsFinished?.Invoke();
            }
        }

        #endregion
    }
}
