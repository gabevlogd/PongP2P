#if UNITY_EDITOR
namespace GV.Sound
{
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(SoundManager))]
    public class SoundManager_Inspector : Editor
    {
        private AudioClipsData _audioClipsData;

        private void Awake()
        {
            _audioClipsData = Resources.Load<AudioClipsData>("Audio Clips Datas");
            EditorUtility.SetDirty(_audioClipsData);
        }

        public override void OnInspectorGUI()
        {
            DrawClipsList();

            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("+", GUILayout.Width(30f)))
                    QueueNewClip();
                if (GUILayout.Button("-", GUILayout.Width(30f)) && _audioClipsData.AudioClips.Count > 0)
                    DequeueLastClip();
            }

        }

        private void DrawClipsList()
        {
            if (_audioClipsData.AudioClips != null)
            {
                for (int i = 0; i < _audioClipsData.AudioClips.Count; i++)
                {
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        _audioClipsData.AudioClips[i] = EditorGUILayout.ObjectField(_audioClipsData.AudioClips[i], typeof(AudioClip), false) as AudioClip;
                        _audioClipsData.AudioClipNames[i] = EditorGUILayout.TextField(_audioClipsData.AudioClipNames[i], GUILayout.Width(100f));
                    }

                }
            }
        }

        private void QueueNewClip()
        {
            if (_audioClipsData.AudioClips == null)
            {
                _audioClipsData.AudioClips = new List<AudioClip>();
                _audioClipsData.AudioClipNames = new List<string>();
            }

            _audioClipsData.AudioClips.Add(null);
            _audioClipsData.AudioClipNames.Add(null);
        }

        private void DequeueLastClip()
        {
            _audioClipsData.AudioClips.RemoveAt(_audioClipsData.AudioClips.Count - 1);
            _audioClipsData.AudioClipNames.RemoveAt(_audioClipsData.AudioClipNames.Count - 1);
        }
    }
}
#endif
