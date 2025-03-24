using TMPro;
using UnityEngine;

namespace Critsoft.ForgeSim2025.Gameplay
{
    public class BonusEffectDescription : MonoBehaviour
    {
        [SerializeField] private TMP_Text _descriptionText;

        public TMP_Text DescriptionText => _descriptionText;
    }
}
