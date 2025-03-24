using UnityEngine;

namespace Critsoft.ForgeSim2025.Gameplay.Quests
{
    [CreateAssetMenu(fileName = "QuestData", menuName = "ScriptableObjects/QuestData", order = 5)]
    public class QuestDataSO : ScriptableObject
    {
        public int Id;
        public string Name;
        public string Description;
        [Space]
        public ItemType Requirement; // todo: Use lists instead of a single type of item / requirement
        public int ProgressRequired;
        [Space]
        public MachineType Reward; // todo: Use lists instead of a single type of reward
    }
}
