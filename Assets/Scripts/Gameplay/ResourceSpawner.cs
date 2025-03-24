using System.Collections;
using UnityEngine;
using Zenject;

namespace Critsoft.ForgeSim2025.Gameplay
{
    public class ResourceSpawner : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField] private float minSpawnPointX = -5;
        [SerializeField] private float maxSpawnPointX = 5;
        [SerializeField] private float spawnInterval = 1.5f;
        [Space]
        [SerializeField] private GameObject _fallingResourcePrefab;

        #endregion

        #region Fields

        private FallingResource.Pool _resourcePool;
        private float _spawnY;

        #endregion

        #region Public Methods

        [Inject]
        public void Construct(FallingResource.Pool resourcePool)
        {
            _resourcePool = resourcePool;
            _spawnY = Camera.main.orthographicSize + 1;

            StartCoroutine(SpawnRoutine());
        }

        #endregion

        #region Private Methods

        private IEnumerator SpawnRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(spawnInterval);
                SpawnResource();
            }
        }

        private void SpawnResource()
        {
            float randomX = Random.Range(minSpawnPointX, maxSpawnPointX);
            Vector3 spawnPosition = new Vector3(randomX, _spawnY, 0);

            FallingResource resource = _resourcePool.Spawn();
            resource.transform.position = spawnPosition;
            resource.Init(_resourcePool);
        }

        #endregion
    }

}
