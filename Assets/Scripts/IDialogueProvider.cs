using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
public interface IDialogueProvider
{
    public List<Dialogue> LoadedDialogueData {  get; set; }
    public bool TryLoadDialogue(string levelName,string flagName);
}
