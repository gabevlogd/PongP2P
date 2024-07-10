namespace GV.Utility
{
    using System.Collections.Generic;

    public class DialoguesHandler
    {

        public List<List<string>> DialoguesScriptLines;
        public List<string> DialoguesNames;

        private List<Dialogue> m_dialogues;


        public DialoguesHandler(List<Dialogue> dialogues)
        {
            m_dialogues = dialogues;
            GenerateDialogues();
        }

        public List<string> GetDialogue(out string dialogueName)
        {
            dialogueName = DialoguesNames[0];
            List<string> dialogue = DialoguesScriptLines[0];

            DialoguesScriptLines.Remove(dialogue);
            DialoguesNames.Remove(dialogueName);

            return dialogue;
        }

        public void GenerateDialogues()
        {
            DialoguesScriptLines = new();
            DialoguesNames = new();

            foreach (Dialogue dialogue in m_dialogues)
            {
                DialoguesScriptLines.Add(dialogue.GetDialogue());
                DialoguesNames.Add(dialogue.DialogueName);
            }
        }


    }
}



