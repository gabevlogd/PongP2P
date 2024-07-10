namespace GV.Sound
{
    using System.Collections.Generic;
    using UnityEngine;


    /// <summary>
    /// This class contains the data of all audio clips of the game
    /// </summary>
    [CreateAssetMenu(menuName = "Scriptable Objects/Audio Clips Datas", fileName = "Audio Clips Datas")]
    public class AudioClipsData : ScriptableObject
    {
        public List<AudioClip> AudioClips;
        public List<string> AudioClipNames;


    }
}
