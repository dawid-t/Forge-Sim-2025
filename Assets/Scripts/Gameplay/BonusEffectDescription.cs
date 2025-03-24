using TMPro;
using UnityEngine;

public class BonusEffectDescription : MonoBehaviour
{
    [SerializeField] private TMP_Text _descriptionText;
    
    public TMP_Text DescriptionText => _descriptionText;
}
