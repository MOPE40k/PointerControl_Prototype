using UnityEngine;

namespace _Project.Develop.Gameplay.Features.AudioFeatures
{
    public class AudioPlayer
    {
        // Consts
        private const float _minPitch = 0.8f;
        private const float _maxPitch = 1.2f;
        private const float _defaultPitch = 1.0f;

        // References
        private readonly AudioSource _audioSource = null;

        public AudioPlayer(AudioSource audioSource)
            => _audioSource = audioSource;

        public void PlaySound(AudioClip clip)
            => _audioSource.PlayOneShot(clip);

        public void PlaySoundWithRandomPitch(AudioClip clip)
        {
            SetPitch(Random.Range(_minPitch, _maxPitch));

            PlaySound(clip);
        }

        private void SetPitch(float pitchValue)
            => _audioSource.pitch = pitchValue;

        private void ResetPitch()
            => SetPitch(_defaultPitch);
    }
}