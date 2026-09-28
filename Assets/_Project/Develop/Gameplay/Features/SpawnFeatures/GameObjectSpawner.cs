using UnityEngine;

namespace _Project.Develop.Gameplay.Features.SpawnFeatures
{
    public class GameObjectSpawner : MonoBehaviour
    {
        // // Consts
        // private const KeyCode SpawnKey = KeyCode.F;

        [Header("Settings:")]
        [SerializeField] private float _intervalBetweenSpawn = 5f;
        [SerializeField] private float _maxRadiusSpawn = 5f;

        [Space]
        [Header("References:")]
        [SerializeField] private GameObject _gameObjectPrefab = null;
        [SerializeField] private Transform _objectAroundSpawn = null;

#if UNITY_EDITOR
        [Space]
        [Header("GIZMOS SETTINGS:")]
        [SerializeField] private Color _areaColor = Color.green;

        [Space]
        [Header("GUI TEXT SETTINGS:")]
        [SerializeField] private int _textSize = 24;
        [SerializeField] private float _xPositionText = 10f;
        [SerializeField] private float _yPositionText = 1024f;
        [SerializeField] private float _textFieldWidth = 512f;
        [SerializeField] private float _textFieldHeight = 512f;
        [SerializeField] private Color _textColor = Color.green;
#endif

        // Runtime
        private SpawnAroundByTime _spawnByTimer = null;

        private void Awake()
            => _spawnByTimer = new SpawnAroundByTime(
                _gameObjectPrefab,
                _intervalBetweenSpawn,
                _maxRadiusSpawn,
                _objectAroundSpawn,
                this);

        public void EnableSpawn()
            => _spawnByTimer.ToggleEnable();

#if UNITY_EDITOR
        private void OnGUI()
        {
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.fontSize = _textSize;
            style.normal.textColor = _textColor;

            GUI.Label(new Rect(
                _xPositionText, _yPositionText, _textFieldWidth, _textFieldHeight),
                $"Spawn status: {_spawnByTimer.IsEnabled}",
                style);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = _areaColor;

            Vector3 spherePosition = (_objectAroundSpawn == null) ? this.transform.position : _objectAroundSpawn.position;

            Gizmos.DrawWireSphere(spherePosition, _maxRadiusSpawn);
        }
#endif
    }
}