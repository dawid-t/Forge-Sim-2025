using UnityEngine;

namespace Critsoft.ForgeSim2025.Gameplay
{
    public class RandomResource : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private ItemsLibrarySO _itemsLibrary;

        #endregion

        #region Fields

        private readonly ItemType[] resourceTypes = { ItemType.IronOre, ItemType.GoldOre,
            ItemType.FireShard, ItemType.EmberDust, ItemType.DragonScale };

        private readonly float[] chances = { GameConfig.IronOreSpawnPercentageChance, GameConfig.GoldOreSpawnPercentageChance,
            GameConfig.FireShardSpawnPercentageChance, GameConfig.EmberDustSpawnPercentageChance, GameConfig.DragonScaleSpawnPercentageChance };

        #endregion

        public ItemType ResourceType { get; private set; }

        #region Public Methods

        public ItemType DrawResource()
        {
            float randomValue = Random.Range(0f, 1f);
            float cumulativeChance = 0f;

            for (int i = 0; i < resourceTypes.Length; i++)
            {
                cumulativeChance += chances[i];
                if (randomValue < cumulativeChance)
                {
                    return resourceTypes[i];
                }
            }

            return resourceTypes[resourceTypes.Length - 1];
        }

        #endregion

        #region Private Methods

        private void OnEnable()
        {
            ResourceType = DrawResource();
            UpdateImage();
        }

        private void UpdateImage()
        {
            foreach (ItemDataSO itemData in _itemsLibrary.Items)
            {
                if (itemData.Type == ResourceType)
                {
                    _spriteRenderer.sprite = itemData.Sprite;
                    return;
                }
            }
        }
        
        #endregion
    }
}
