using UnityEngine;
using UnityEngine.Audio;

namespace _Project.Develop.Gameplay.Features.AudioFeatures
{
    public class AudioMixerHandler
    {
        // Consts
        private const float OnVolumeValue = 0f;
        private const float OffVolumeValue = -80f;

        private const string SoundsVolumeKey = "SoundsVolume";
        private const string MusicVolumeKey = "MusicVolume";

        // References
        private readonly AudioMixer _audioMixer = null;

        public AudioMixerHandler(AudioMixer audioMixer)
            => _audioMixer = audioMixer;

        public bool IsSoundsOn()
            => IsVolumeOn(SoundsVolumeKey);

        public bool IsMusicOn()
            => IsVolumeOn(MusicVolumeKey);

        public void SoundsOn()
            => OnVolume(SoundsVolumeKey);

        public void SoundsOff()
            => OffVolume(SoundsVolumeKey);

        public void MusicOn()
            => OnVolume(MusicVolumeKey);

        public void MusicOff()
            => OffVolume(MusicVolumeKey);

        private bool IsVolumeOn(string key)
        {
            _audioMixer.GetFloat(key, out float volumeValue);

            return Mathf.Abs(volumeValue - OnVolumeValue) <= Mathf.Epsilon;
        }

        private void OnVolume(string key)
            => _audioMixer.SetFloat(key, OnVolumeValue);

        private void OffVolume(string key)
            => _audioMixer.SetFloat(key, OffVolumeValue);
    }
}