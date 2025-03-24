using UnityEngine;

namespace Critsoft.ForgeSim2025.Gameplay.Quests
{
    [CreateAssetMenu(fileName = "QuestsLibrary", menuName = "ScriptableObjects/QuestsLibrary", order = 4)]
    public class QuestsLibrarySO : ScriptableObject
    {
        public QuestDataSO[] Quests;
    }
}
