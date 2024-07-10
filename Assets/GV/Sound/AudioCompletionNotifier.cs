namespace GV.Sound
{
    using System;
    using UnityEngine;

    /// <summary>
    /// This class, attached to audio sources used by the sound manager, send an event when the audio source have finished to reproduce sound
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class AudioCompletionNotifier : MonoBehaviour
    {
        public static event Action<AudioSource> AudioClipEnded;
        private AudioSource _audioSource;

        private void Awake() => _audioSource = _audioSource == null ? GetComponent<AudioSource>() : _audioSource;

        private void Update()
        {
            if (!_audioSource.isPlaying)
                AudioClipEnded?.Invoke(_audioSource);
        }

    }
}
