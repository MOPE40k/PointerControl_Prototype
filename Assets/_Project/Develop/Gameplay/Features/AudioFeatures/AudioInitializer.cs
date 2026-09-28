using UnityEngine;
using UnityEngine.Audio;

namespace _Project.Develop.Gameplay.Features.AudioFeatures
{
    public class AudioInitializer : MonoBehaviour
    {
        [SerializeField] private AudioMixer _generalAudioMixer = null;
        [SerializeField] private AudioControlView _audioSettingsView = null;

        private AudioMixerHandler _audioMixerHandler = null;

        private void Awake()
        {
            _audioMixerHandler = new AudioMixerHandler(_generalAudioMixer);

            _audioSettingsView.SetAudioMixer(_audioMixerHandler);
        }
    }
}