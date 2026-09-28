using UnityEngine;
using UnityEngine.UI;

namespace _Project.Develop.Gameplay.Features.AudioFeatures
{
    public class AudioControlView : MonoBehaviour
    {
        [Header("References:")]
        [SerializeField] private Button _soundsButton = null;
        [SerializeField] private Button _musicButton = null;

        // Runtime
        private AudioMixerHandler _audioMixerHandler = null;

#if UNITY_EDITOR
        [Space]
        [Header("GUI TEXT SETTINGS:")]
        [SerializeField] private int _textSize = 24;
        [SerializeField] private float _xSoundPositionText = 10f;
        [SerializeField] private float _ySoundPositionText = 1014f;
        [SerializeField] private float _xMusicPositionText = 10f;
        [SerializeField] private float _yMusicPositionText = 1004f;
        [SerializeField] private float _textFieldWidth = 512f;
        [SerializeField] private float _textFieldHeight = 512f;
        [SerializeField] private Color _textColor = Color.green;
#endif

        private void OnEnable()
            => Subscribe();

        private void OnDisable()
            => Unsubscribe();

        public void SetAudioMixer(AudioMixerHandler handler)
            => _audioMixerHandler = handler;

        private void Subscribe()
        {
            _soundsButton.onClick.AddListener(SoundsToggle);
            _musicButton.onClick.AddListener(MusicToggle);
        }

        private void Unsubscribe()
        {
            _soundsButton.onClick.RemoveListener(SoundsToggle);
            _musicButton.onClick.RemoveListener(MusicToggle);
        }


        private void SoundsToggle()
        {
            if (_audioMixerHandler.IsSoundsOn())
                _audioMixerHandler.SoundsOff();
            else
                _audioMixerHandler.SoundsOn();
        }

        private void MusicToggle()
        {
            if (_audioMixerHandler.IsMusicOn())
                _audioMixerHandler.MusicOff();
            else
                _audioMixerHandler.MusicOn();
        }

#if UNITY_EDITOR
        private void OnGUI()
        {
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.fontSize = _textSize;
            style.normal.textColor = _textColor;

            GUI.Label(new Rect(
                _xSoundPositionText, _ySoundPositionText, _textFieldWidth, _textFieldHeight),
                $"Sound status: {_audioMixerHandler.IsSoundsOn()}",
                style);

            GUI.Label(new Rect(
                _xMusicPositionText, _yMusicPositionText, _textFieldWidth, _textFieldHeight),
                $"Music status: {_audioMixerHandler.IsMusicOn()}",
                style);
        }
#endif
    }
}