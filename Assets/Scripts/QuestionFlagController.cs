using System.Collections.Generic;
using UnityEngine;
public class QuestionFlagController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PanelController.Instance.SetPanelActive(PanelController.Instance.QuestionEntryPanel);//QuestionEntry Panel içindeki Play butonu içine Initialize yerleþtirildi
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