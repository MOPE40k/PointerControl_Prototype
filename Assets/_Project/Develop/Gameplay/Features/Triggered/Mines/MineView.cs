using _Project.Develop.Gameplay.Effects;
using _Project.Develop.Gameplay.Features.AudioFeatures;
using UnityEngine;

namespace _Project.Develop.Gameplay.Features.Triggered.Mines
{
    public class MineView : MonoBehaviour
    {
        [Header("Settings:")]
        [SerializeField] private Color _inAreaColor = Color.red;
        [SerializeField] private Color _outAreaColor = Color.green;

        [Space]
        [Header("References:")]
        [SerializeField] private SpriteRenderer _renderer = null;
        [SerializeField] private ParticlesEffect _particlesEffectPrefab = null;
        [SerializeField] private SoundEffect _soundEffectPrefab = null;

        public void InArea()
            => _renderer.color = _inAreaColor;

        public void OutArea()
            => _renderer.color = _outAreaColor;

        public void Explosion()
        {
            ParticlesEffect particlesEffectInstance = Instantiate(_particlesEffectPrefab, this.transform.position, Quaternion.identity);
            particlesEffectInstance.Play();

            SoundEffect soundEffectInstance = Instantiate(_soundEffectPrefab, this.transform.position, Quaternion.identity);
            soundEffectInstance.Play();
        }
    }
}