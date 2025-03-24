using UnityEngine;

namespace Critsoft.ForgeSim2025.Gameplay
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData", order = 2)]
    public class ItemDataSO : ScriptableObject
    {
        public ItemType Type;
        public bool IsCrafted;
        public string Name;
        public string Description;
        public Sprite Sprite;
        [Space]
        public bool HasBonusEffect;
        public BonusEffectType BonusEffect;
        public float BonusEffectValue;
    }
}
