using UnityEngine;

namespace Critsoft.ForgeSim2025.Gameplay
{
    [CreateAssetMenu(fileName = "ItemsLibrary", menuName = "ScriptableObjects/ItemsLibrary", order = 1)]
    public class ItemsLibrarySO : ScriptableObject
    {
        public ItemDataSO[] Items;
    }
}
