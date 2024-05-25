using Gamekit2D;
using UnityEngine;
using UnityEngine.UI;
public class LevelButton : MonoBehaviour
{
    [SerializeField] Button levelButton;
    [SerializeField] OpenAIController openAIController;
    public int Level;
    private void Awake()
    {
        levelButton.onClick.AddListener(() => OnLevelButtonClicked(Level));
    }
    public  void OnLevelButtonClicked(int level)
    {
        LevelController.CurrentLevel = level;
        SceneController.Instance.OpenScene("Zone"+level.ToString());
    }
}