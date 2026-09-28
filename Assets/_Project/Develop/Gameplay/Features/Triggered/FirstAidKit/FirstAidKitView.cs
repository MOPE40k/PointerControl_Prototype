using _Project.Develop.Gameplay.Effects;
using _Project.Develop.Gameplay.Features.AudioFeatures;
using UnityEngine;

namespace _Project.Develop.Gameplay.Features.Triggered.FirstAidKit
{
    public class FirstAidKitView : MonoBehaviour
    {
        [Header("References:")]
        [SerializeField] private ParticlesEffect _particlesEffectPrefab = null;
        [SerializeField] private SoundEffect _soundEffectPrefab = null;

        public void HealEffect()
        {
            ParticlesEffect particlesEffectInstance = Instantiate(_particlesEffectPrefab, this.transform.position, Quaternion.identity);
            particlesEffectInstance.Play();

            SoundEffect soundEffectInstance = Instantiate(_soundEffectPrefab, this.transform.position, Quaternion.identity);
            soundEffectInstance.Play();
        }
    }
}