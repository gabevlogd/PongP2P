namespace GV.Utility
{
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public class Dialogue
    {
        public string DialogueName;

        [SerializeField]
        private List<Speaker> m_speakers;
        [SerializeField]
        private List<string> m_speakersOrder;



        public List<string> GetDialogue()
        {
            List<string> dialogue = new List<string>();

            foreach (string speakerName in m_speakersOrder)
            {
                foreach (Speaker speaker in m_speakers)
                {
                    if (speaker.SpeakerName != speakerName) continue;
                    else if (speaker.Script.Count == 0)
                    {
                        Debug.LogWarning("The dialogue " + DialogueName + " will be corrupted or incomplete.");
                        Debug.LogWarning("You're trying to call " + speakerName + " more times than there are lines in the script.\nMake sure the " + speakerName + "'s number of sript lines equals the number of times he is called in the speakers order");
                        return null;
                    }

                    dialogue.Add(speakerName + ": " + speaker.Speak() + "\n");
                    break;
                }
            }

            return dialogue;

        }
    }
}

