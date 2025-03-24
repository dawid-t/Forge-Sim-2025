using DG.Tweening;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Critsoft.ForgeSim2025.Gameplay
{
    public class Item : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _quantityText;
        [SerializeField] private ItemsLibrarySO _itemsLibrary;

        #endregion

        #region Fields

        private const int MinValueToShowQuantity = 2;

        private ItemType _type;
        private bool _isCrafted;
        private int _quantity;
        private string _name;
        private string _description;
        
        private bool _hasBonusEffect;
        private BonusEffectType _bonusEffect;
        private float _bonusEffectValue;
        
        private Slot _currentSlot;
        private DraggableItem _draggableItem;

        #endregion

        #region Properties

        public ItemType Type => _type;
        public bool IsCrafted => _isCrafted;
        public int Quantity => _quantity;
        public string Name => _name;
        public string Description => _description;
        public bool HasBonusEffect => _hasBonusEffect;
        public BonusEffectType BonusEffect => _bonusEffect;
        public float BonusEffectValue => _bonusEffectValue;
        public Image Image => _image;
        public Slot CurrentSlot => _currentSlot;

        #endregion

        #region Public Methods

        public void Init(ItemType itemType, Slot currentSlot, int quantity = 1)
        {
            ItemDataSO itemData = _itemsLibrary.Items.FirstOrDefault(item => item.Type == itemType);

            _type = itemData.Type;
            _isCrafted = itemData.IsCrafted;
            _quantity = quantity;
            _name = itemData.Name;
            _description = itemData.Description;

            _hasBonusEffect = itemData.HasBonusEffect;
            _bonusEffect = itemData.BonusEffect;
            _bonusEffectValue = itemData.BonusEffectValue;
            _image.sprite = itemData.Sprite;
            
            _currentSlot = currentSlot;
            UpdateQuantityText();
            PlayPunchScaleAnimation();

            _draggableItem = GetComponent<DraggableItem>();
            if (_draggableItem != null)
            {
                _draggableItem.SlotChanged += OnSlotChanged;
            }
        }

        public void AddQuantity(int quantity)
        {
            _quantity += quantity;
            UpdateQuantityText();
            PlayPunchScaleAnimation();
        }

        public void RemoveQuantity(int quantity)
        {
            _quantity -= quantity;

            if (_quantity <= 0)
            {
                Destroy(gameObject);
                //enabled = false; // todo: object pooling
            }
            else
            {
                UpdateQuantityText();
                PlayPunchScaleAnimation();
            }
        }

        #endregion

        #region Private Methods

        private void OnDestroy()
        {
            if (_draggableItem != null)
            {
                _draggableItem.SlotChanged -= OnSlotChanged;
            }
            DOTween.Kill(transform);
        }

        private void UpdateQuantityText()
        {
            _quantityText.text = _quantity.ToString();
            _quantityText.gameObject.SetActive(_quantity >= MinValueToShowQuantity);
        }

        private void PlayPunchScaleAnimation(float punchScale = 0.2f, float duration = 0.3f)
        {
            DOTween.Kill(transform);
            transform.DOPunchScale(Vector3.one * punchScale, duration);
        }

        private void OnSlotChanged(Slot slot)
        {
            _currentSlot = slot;
        }

        #endregion
    }
}
