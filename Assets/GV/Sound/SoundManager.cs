namespace GV.Sound
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Audio;
    using GV.Patterns;

    /// <summary>
    /// This class manages the reproduction of sounds
    /// </summary>
    public class SoundManager : Pool<AudioSource>
    {
        private static SoundManager _this;

        private Dictionary<string, AudioClip> _audioClips;
        private AudioMixer _audioMixer;
        private AudioSettings _audioSettings;

        [SerializeField]
        [Min(2f)]
        private int _startingSourceCount;

        private void Awake() => InitializeManager();

        private void OnEnable() => AudioCompletionNotifier.AudioClipEnded += StopSource;

        private void OnDisable() => AudioCompletionNotifier.AudioClipEnded -= StopSource;

        private void InitializeManager()
        {
            _this = this;
            _poolObjPrefab = Resources.Load<AudioSource>("Audio Source");
            _audioMixer = Resources.Load<AudioMixer>("Audio Mixer");
            _audioSettings = Resources.Load<AudioSettings>("Audio Settings");
            AudioClipsData audioClipsData = Resources.Load<AudioClipsData>("Audio Clips Datas");
            _audioClips = new Dictionary<string, AudioClip>();
            for (int i = 0; i < audioClipsData.AudioClips.Count; i++)
                _audioClips.Add(audioClipsData.AudioClipNames[i], audioClipsData.AudioClips[i]);
            InitializePool(_startingSourceCount);
        }

        public static void Play(string audioClipName, bool loop)
        {
            if (_this._audioSettings.Mute) return;
            AudioSource audioSource = GetAudioSource(audioClipName, loop);
            audioSource.loop = loop;
            if (audioSource != null)
                audioSource.Play();
        }

        private static AudioSource GetAudioSource(string audioClipName, bool loop)
        {
            AudioSource audioSource = _this.GetObject(true);
            audioSource.clip = _this._audioClips[audioClipName];
            audioSource.outputAudioMixerGroup = _this._audioMixer.outputAudioMixerGroup;
            audioSource.loop = loop;
            if (audioSource.clip == null)
            {
                Debug.LogWarning("Invalid Audio Source Name");
                return null;
            }
            else return audioSource;
        }

        private void StopSource(AudioSource audioSource)
        {
            audioSource.clip = null;
            ReleaseObject(audioSource);
        }
    }
}

