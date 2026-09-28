using UnityEngine;

namespace _Project.Develop.Gameplay.Effects
{
    public class ParticlesEffect : MonoBehaviour
    {
        [Header("References:")]
        [SerializeField] private ParticleSystem _effectPrefab = null;

        public void Play()
        {
            ParticleSystem particlesInstance = Instantiate(_effectPrefab, this.transform.position, Quaternion.identity);
            particlesInstance.Play();

            Destroy(this.gameObject, particlesInstance.main.duration);
        }
    }
}