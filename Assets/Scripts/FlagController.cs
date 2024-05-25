using UnityEngine;
public class FlagController : MonoBehaviour, IFlagController
{
    [field: SerializeField] public string FlagName { get; set; }
    [field: SerializeField] public DialogueManager DialogueManager { get; set; }
    [field: SerializeField] public JsonProvider DatabaseProvider { get; set; }
    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if (collision.gameObject.CompareTag("Player"))
        {
            if (DatabaseProvider.TryLoadDialogue("", FlagName))
            {
                DialogueManager.Initialize();
                Debug.Log("Çalýþtý");
            }
        }        
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
        }
    }  
}