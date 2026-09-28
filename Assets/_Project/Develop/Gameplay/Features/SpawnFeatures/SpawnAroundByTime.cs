using System.Collections;
using UnityEngine;

namespace _Project.Develop.Gameplay.Features.SpawnFeatures
{
    public class SpawnAroundByTime
    {
        // Settings
        private readonly float _intervalBetweenSpawn = 5f;
        private readonly float _maxRadius = 5f;

        //References
        private readonly GameObject _prefab = null;
        private readonly Transform _objectAroundSpawn = null;
        private readonly MonoBehaviour _coroutineRunner = null;

        // Runtime
        private Coroutine _spawnRoutine = null;

        public SpawnAroundByTime(
            GameObject prefab,
            float intervalBetweenSpawn,
            float maxRadius,
            Transform objectAroundSpawn,
            MonoBehaviour coroutineRunner)
        {
            _prefab = prefab;
            _intervalBetweenSpawn = intervalBetweenSpawn;
            _maxRadius = maxRadius;
            _objectAroundSpawn = objectAroundSpawn;
            _coroutineRunner = coroutineRunner;
        }

        // Runtime
        public bool IsEnabled { get; private set; } = false;

        public void ToggleEnable()
        {
            IsEnabled = !IsEnabled;

            ToggleRoutine();
        }

        private void ToggleRoutine()
        {
            if (IsEnabled)
                StartRoutine();
            else
                StopRoutine();
        }

        private void StartRoutine()
        {
            StopRoutine();

            _spawnRoutine = _coroutineRunner.StartCoroutine(SpawnRoutine());
        }

        private void StopRoutine()
        {
            if (_spawnRoutine == null)
                return;

            _coroutineRunner.StopCoroutine(SpawnRoutine());
            _spawnRoutine = null;
        }

        private IEnumerator SpawnRoutine()
        {
            while (IsEnabled)
            {
                SpawnAroundInRandomPosition();

                yield return new WaitForSeconds(_intervalBetweenSpawn);
            }
        }

        private void SpawnAroundInRandomPosition()
        {
            var spawnPosition = _objectAroundSpawn.position + Random.insideUnitSphere * _maxRadius;
            spawnPosition.y = _objectAroundSpawn.transform.position.y;

            GameObject.Instantiate(_prefab, spawnPosition, Quaternion.identity);
        }
    }
}