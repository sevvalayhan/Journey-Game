using UnityEngine;
using UnityEngine.UI;
public class CategoryController : MonoBehaviour
{
    public Button playButton;
    [SerializeField] private FirebaseProvider databaseProvider;
    [SerializeField] private QuizController questionController;
    public string categoryName;
    private void Start()
    {
        playButton.onClick.AddListener(()=>PlayGame(categoryName));
    }
    public async void PlayGame(string categoryName)
    {
        if (await databaseProvider.TryLoadQuestion(categoryName))
        {
            questionController.Initialize();            
        }
    }
}
