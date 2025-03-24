using UnityEngine;
using Zenject;

namespace Critsoft.ForgeSim2025.Gameplay
{
    [RequireComponent(typeof(RandomResource))]
    public class FallingResource : MonoBehaviour
    {
        #region Fields

        private float _fallSpeed = 2f;
        private float _destroyY;
        private Pool _pool;
        private RandomResource _randomResource;

        #endregion

        #region Public Methods

        public void Init(Pool pool)
        {
            _pool = pool;
            _randomResource = GetComponent<RandomResource>();
            _destroyY = -Camera.main.orthographicSize - 1;
        }

        public ItemType CollectResource()
        {
            _pool.Despawn(this);
            return _randomResource.ResourceType;
        }

        #endregion

        #region Private Methods

        private void Update()
        {
            transform.position += Vector3.down * _fallSpeed * Time.deltaTime;

            if (transform.position.y <= _destroyY)
            {
                _pool.Despawn(this);
            }
        }

        #endregion

        public class Pool : MonoMemoryPool<FallingResource>
        {
            protected override void OnDespawned(FallingResource item)
            {
                item.gameObject.SetActive(false);
            }

            protected override void OnSpawned(FallingResource item)
            {
                item.gameObject.SetActive(true);
            }
        }
    }
}
