using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFlagController 
{
    public string FlagName { get; set; }
    public  DialogueManager DialogueManager{ get; set; }
    public  JsonProvider DatabaseProvider { get; set; }
}
