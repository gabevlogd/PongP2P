namespace GV.Sound
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Scriptable Objects/Audio Settings", fileName = "Audio Settings")]
    public class AudioSettings : ScriptableObject
    {
        #region BOOL:
        [SerializeField]
        private bool _mute;
        public bool Mute
        {
            get => GetBool("Mute", ref _mute);
            set => SetBool("Mute", ref _mute, value);
        }
        [SerializeField]
        private bool _subtitleOn;
        public bool SubtitleOn
        {
            get => GetBool("SubtitleOn", ref _subtitleOn);
            set => SetBool("SubtitleOn", ref _subtitleOn, value);
        }
        #endregion
        #region VOLUMES:
        [SerializeField]
        [Range(0f, 1f)]
        private float _masterVolume;
        public float MasterVolume
        {
            get => GetFloat("MasterVolume", ref _masterVolume);
            set => SetFloat("MasterVolume", ref _masterVolume, value);
        }
        [SerializeField]
        [Range(0f, 1f)]
        private float _musicVolume;
        public float MusicVolume
        {
            get => GetFloat("MusicVolume", ref _musicVolume);
            set => SetFloat("MusicVolume", ref _musicVolume, value);
        }
        [SerializeField]
        [Range(0f, 1f)]
        private float _sfxVolume;
        public float SFXVolume
        {
            get => GetFloat("SFXVolume", ref _sfxVolume);
            set => SetFloat("SFXVolume", ref _sfxVolume, value);
        }
        [SerializeField]
        [Range(0f, 1f)]
        private float _voiceVolume;
        public float VoiceVolume
        {
            get => GetFloat("VoiceVolume", ref _voiceVolume);
            set => SetFloat("VoiceVolume", ref _voiceVolume, value);
        }
        #endregion
        #region LANGUAGES:
        [SerializeField]
        private Language _audioLanguage;
        public Language AudioLanguage
        {
            get => GetLanguage("AudioLanguage", ref _audioLanguage);
            set => SetLanguage("AudioLanguage", ref _audioLanguage, value);
        }
        [SerializeField]
        private Language _subtitlesLanguage;
        public Language SubtitlesLanguage
        {
            get => GetLanguage("SubtitlesLanguage", ref _subtitlesLanguage);
            set => SetLanguage("SubtitlesLanguage", ref _subtitlesLanguage, value);
        }
        #endregion

        #region METHODS:
        private float GetFloat(string floatKey, ref float target)
        {
            if (PlayerPrefs.HasKey(floatKey))
                target = PlayerPrefs.GetFloat(floatKey);
            return target;
        }
        private Language GetLanguage(string languageKey, ref Language target)
        {
            if (PlayerPrefs.HasKey(languageKey))
                target = (Language)PlayerPrefs.GetInt(languageKey);
            return target;
        }
        private bool GetBool(string boolKey, ref bool target)
        {
            if (PlayerPrefs.HasKey(boolKey))
                target = PlayerPrefs.GetInt(boolKey) == 1;
            return target;
        }
        private void SetFloat(string floatKey, ref float target, float value)
        {
            target = value;
            PlayerPrefs.SetFloat(floatKey, target);
        }
        private void SetLanguage(string languageKey, ref Language target, Language value)
        {
            target = value;
            PlayerPrefs.SetInt(languageKey, (int)target);
        }
        private void SetBool(string boolKey, ref bool target, bool value)
        {
            target = value;
            int parsedValue = 0;
            if (target) parsedValue = 1;
            PlayerPrefs.SetInt(boolKey, parsedValue);
        }

        public void ResetDefaultSettings()
        {
            PlayerPrefs.DeleteKey("Mute");
            _mute = false;
            PlayerPrefs.DeleteKey("SubtitleOn");
            _subtitleOn = false;
            PlayerPrefs.DeleteKey("MasterVolume");
            _masterVolume = 1f;
            PlayerPrefs.DeleteKey("MusicVolume");
            _musicVolume = 1f;
            PlayerPrefs.DeleteKey("SFXVolume");
            _sfxVolume = 1f;
            PlayerPrefs.DeleteKey("VoiceVolume");
            _voiceVolume = 1f;
            PlayerPrefs.DeleteKey("AudioLanguage");
            _audioLanguage = 0;
            PlayerPrefs.DeleteKey("SubtitlesLanguage");
            _subtitlesLanguage = 0;
        }
        #endregion
    }

    public enum Language
    {
        English,
        Italian
    }
}

