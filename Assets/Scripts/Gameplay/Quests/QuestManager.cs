using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Critsoft.ForgeSim2025.Gameplay.Quests
{
    public class QuestManager : MonoBehaviour
    {
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

        private void CreateQuests()
        {
            foreach (QuestDataSO questData in _questsLibrary.Quests)
            {
                Quest quest = _questFactory.Create();
                quest.SetQuestData(questData);
                _quests.Add(quest);
            }
        }

        #endregion
    }
}
