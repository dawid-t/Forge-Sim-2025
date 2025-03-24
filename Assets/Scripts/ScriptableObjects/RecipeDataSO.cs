using System.Collections.Generic;
using UnityEngine;

namespace Critsoft.ForgeSim2025.Gameplay
{
    [CreateAssetMenu(fileName = "RecipeData", menuName = "ScriptableObjects/RecipeData", order = 3)]
    public class RecipeDataSO : ScriptableObject
    {
        public MachineType MachineType;
        public string Name;
        public float TimeInSeconds;
        [Range(0f, 1f)]
        public float SuccessRate;
        [Space]
        public ItemType ResultItem;
        public List<ItemType> Resources;
    }
}
