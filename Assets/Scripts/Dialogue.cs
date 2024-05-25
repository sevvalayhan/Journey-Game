using System.Collections.Generic;
public class Dialogue 
{
    public List<string> DialogueTexts{ get; set; }
    public string Name { get; set; }
    public int FaceSpriteIndex { get; set; }
    public Dialogue(List<string> dialogueTexts, string name, List<string> faceSpritePath)
    {
        DialogueTexts = dialogueTexts;
        Name = name;
    }
}