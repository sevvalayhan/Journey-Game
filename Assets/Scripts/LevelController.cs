using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelController : MonoBehaviour
{
    [SerializeField] public Button[] levelButtons;
    [SerializeField] private GameObject levelObject;
    public const string UNLOCKED_LEVEL = "unlockedLevel";
    public static LevelController Instance;
    public static int CurrentLevel { get; set; }
    private void Awake()
    {
        CurrentLevel = PlayerPrefs.GetInt(UNLOCKED_LEVEL, 1);
        Instance = this;
        ButtonToList();
        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = false;
        }
        for (int i = 0; i < PlayerPrefs.GetInt(UNLOCKED_LEVEL, 1); i++)
        {          
            levelButtons[i].interactable = true;
            Debug.Log(PlayerPrefs.GetInt(UNLOCKED_LEVEL, 1) + " awake");
        }
    }
    void ButtonToList()
    {
        int childCOunt = levelObject.transform.childCount;
        levelButtons = new Button[childCOunt];
        for (int i = 0; i < childCOunt; i++)
        {
            levelButtons[i] = levelObject.transform.GetChild(i).GetComponent<Button>();
        }
    }
    private void OnDestroy()
    {
        PlayerPrefs.SetInt(UNLOCKED_LEVEL, CurrentLevel);
    }
    //Scene'ler arasý geçiþte kullanýldý.  
    public void UnlockNewLevel()
    {        
        if (!levelButtons.Length.Equals(SceneManager.GetActiveScene().buildIndex))
        {
            if (PlayerPrefs.GetInt(UNLOCKED_LEVEL) <= SceneManager.GetActiveScene().buildIndex)
            {
                PlayerPrefs.SetInt(UNLOCKED_LEVEL, SceneManager.GetActiveScene().buildIndex + 1);
            }            
        }      
    }
    public bool Unlock_NewLevel()
    {
        if (PlayerPrefs.GetInt(UNLOCKED_LEVEL) <= SceneManager.GetActiveScene().buildIndex)
        {              
            PlayerPrefs.SetInt(UNLOCKED_LEVEL, PlayerPrefs.GetInt(UNLOCKED_LEVEL)+1);
            return true;
        }
        if (levelButtons.Length.Equals(SceneManager.GetActiveScene().buildIndex))
        {
            return false;
        }
        return false;
    }
}