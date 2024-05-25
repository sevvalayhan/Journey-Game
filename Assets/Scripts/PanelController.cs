using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class PanelController : MonoBehaviour
{
    [field: SerializeField] public GameObject QuizPanel { get; private set; }
    [field: SerializeField] public GameObject ResultPanel { get; private set; }
    [field: SerializeField] public GameObject QuestionEntryPanel { get; private set; }
    [field: SerializeField] public GameObject AppQuitPanel { get; private set; }
    [field: SerializeField] public GameObject LevelCompletedPanel { get; private set; }
    [field: SerializeField] public GameObject CluePanel { get; private set; }
    [field: SerializeField] public GameObject DialoguePanel { get; set; }
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private List<GameObject> allPanels;
    public const string PLAYER_PREF_NAME = "name";
    public const string PLAYER_HIGH_SCORE = "score";
    public static PanelController Instance;
    void Start()
    {
        Instance = this;
        if (string.IsNullOrEmpty(PlayerPrefs.GetString(PLAYER_PREF_NAME, string.Empty)))
        {            
        }       
    }
    public void SavePlayerButton()
    {
        PlayerPrefs.SetInt(PLAYER_HIGH_SCORE, 0);
        PlayerPrefs.Save();
    }
    public void SetPanelActive(GameObject targetPanel)
    {
        for (int i = 0; i < allPanels.Count; i++)
        {
            if (allPanels[i] == targetPanel)
            {
                allPanels[i].SetActive(true);
            }
            else
            {
                allPanels[i].SetActive(false);
            }
        }
    }
 
}