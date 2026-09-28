using _Project.Develop.Gameplay.Features.AudioFeatures;
using UnityEngine;

namespace _Project.Develop.Gameplay.Characters
{
    public class CharacterSounds : MonoBehaviour
    {
        [Header("References:")]
        [SerializeField] private AudioSource _audioSource = null;

        [Space]
        [Header("Audio Clips:")]
        [SerializeField] private AudioClip _footStep = null;
        [SerializeField] private AudioClip _takeDamage = null;
        [SerializeField] private AudioClip _die = null;

        // References
        private AudioPlayer _audioPlayer = null;

        private void Awake()
            => _audioPlayer = new AudioPlayer(_audioSource);

        public void Footsteps()
            => PlaySoundWithRandomPitch(_footStep);

        public void TakeDamage()
            => PlaySoundWithRandomPitch(_takeDamage);

        public void Die()
            => PlaySoundWithRandomPitch(_die);

        private void PlaySoundWithRandomPitch(AudioClip clip)
            => _audioPlayer.PlaySoundWithRandomPitch(clip);
    }
}