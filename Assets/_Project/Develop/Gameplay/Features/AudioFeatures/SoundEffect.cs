using UnityEngine;

namespace _Project.Develop.Gameplay.Features.AudioFeatures
{
    public class SoundEffect : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private AudioSource _audioSource = null;
        [SerializeField] private AudioClip _audioClip = null;

        // References
        private AudioPlayer _audioPlayer = null;

        private void Awake()
            => _audioPlayer = new AudioPlayer(_audioSource);

        public void Play()
        {
            _audioPlayer.PlaySoundWithRandomPitch(_audioClip);

            Destroy(this.gameObject, _audioClip.length);
        }
    }
}