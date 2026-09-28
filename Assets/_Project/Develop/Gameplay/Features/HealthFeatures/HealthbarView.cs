using _Project.Develop.Gameplay.Characters;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Develop.Gameplay.Features.HealthFeatures
{
    public class HealthbarView : MonoBehaviour
    {
        [Header("References:")]
        [SerializeField] private RectTransform _canvasTransform = null;
        [SerializeField] private Image _fillImage = null;
        [SerializeField] private Camera _camera = null;
        [SerializeField] private AgentCharacter _character = null;

        private void Awake()
        {
            if (_camera == null)
                _camera = Camera.main;
        }

        private void Update()
            => ViewRotateToCamera();

        public void TakeDamage()
            => Redraw();

        public void Heal()
            => Redraw();

        public void Redraw()
            => _fillImage.fillAmount = _character.CurrentHealth / _character.MaxHealth;

        private void ViewRotateToCamera()
        {
            Quaternion targetRotation = Quaternion.LookRotation(_camera.transform.forward);

            _canvasTransform.rotation = targetRotation;
        }
    }
}