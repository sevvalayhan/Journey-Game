using UnityEngine;
public class FinishPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PanelController.Instance.LevelCompletedPanel.SetActive(true);
           // PlayerPrefs.SetInt(LevelController.UNLOCKED_LEVEL, PlayerPrefs.GetInt(LevelController.UNLOCKED_LEVEL) + 1);
            //if (LevelController.Instance.UnlockNewLevel())
            //{
            //    //for (int i = 0; i <= PlayerPrefs.GetInt(LevelController.UNLOCKED_LEVEL, 1); i++)
            //    //{
            //    //    LevelController.Instance.levelButtons[i].interactable = true;                    
            //    //}
            //}
            LevelController.Instance.UnlockNewLevel();      
            //for (int i = 0; i < LevelController.Instance.levelButtons.Length; i++)
            //{
            //    LevelController.Instance.levelButtons[i].interactable = false;
            //}
            //for (int i = 0; i < PlayerPrefs.GetInt(LevelController.UNLOCKED_LEVEL, 1); i++)
            //{
            //    LevelController.Instance.levelButtons[i].interactable = true;
            //    Debug.Log(PlayerPrefs.GetInt(LevelController.UNLOCKED_LEVEL, 1) + " awake");
            //}
            //Debug.Log(PlayerPrefs.GetInt(LevelController.UNLOCKED_LEVEL, 1));
        }
    }    
}