namespace GV.Utility
{
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public class Speaker
    {
        public string SpeakerName;
        public List<string> Script;

        public string Speak()
        {
            if (Script.Count == 0)
            {
                Debug.LogWarning("You're trying to call " + SpeakerName + " more times than there are lines in the script.");
                return null;
            }

            string result = Script[0];

            Script.Remove(result);

            return result;
        }
    }
}

