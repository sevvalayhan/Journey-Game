using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class QuizController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questionTitleText;
    [SerializeField] private TextMeshProUGUI[] answerTexts;
    [SerializeField] private FirebaseProvider databaseProvider;
    [SerializeField] private Animator quizAnimator;
    [SerializeField] private OpenAIController aiController;
    public int Counter;
    public static float loadingTime = 1f;
    [SerializeField] private List<AnswerButton> answerButtons;
    public void Initialize()
    {        
        //QuestionEntryPanel in Play buttonunda bulunuyor
        Counter = 0;
        PanelController.Instance.SetPanelActive(PanelController.Instance.QuizPanel);
        SetButtonJsonData();
    }
    public void SetButtonJsonData()
    {
        questionTitleText.text = databaseProvider.LoadedQuestions[Counter].QuestionText;
        for (int i = 0; i < answerButtons.Count; i++)
        {
            answerButtons[i].SetAnswer(i, databaseProvider.LoadedQuestions[Counter].Answers[i]);
        }
    }
    public void SetButtonAIData()
    {
        questionTitleText.text = aiController.AIResponse[Counter].QuestionText;
        for (int i = 0; i < answerButtons.Count; i++)
        {
            answerButtons[i].SetAnswer(i, aiController.AIResponse[Counter].Answers[i]);
        }
    }
    public void CheckAIQuestionAnswer(AnswerButton answerButton)
    {
        do
        {
            if (aiController.AIResponse[Counter].RightAnswerIndex == answerButton.AnswerIndex)
            {
                Debug.Log("true answer");
                TrueAnswer(answerButton);
            }
            else
            {
                FalseAnswer(answerButton);
                Debug.Log("False answer");
            }
        } while (aiController.AIResponse.Count != Counter);
        EndGame();
    }
    public void CheckAnswer(AnswerButton clickedButton)
    {
        if (!Counter.Equals(databaseProvider.LoadedQuestions.Count))
        {
            if (databaseProvider.LoadedQuestions[Counter].RightAnswerIndex == clickedButton.AnswerIndex)
            {
                StartCoroutine(clickedButton.ChangeButtonColorAnimation(Color.green));
                TrueAnswer(clickedButton);
            }
            else
            {
                StartCoroutine(clickedButton.ChangeButtonColorAnimation(Color.red));
                FalseAnswer(clickedButton);
            }
        }
        if (Counter.Equals(databaseProvider.LoadedQuestions.Count))
        {
            StartCoroutine(EndGame());
        }
    }
    public IEnumerator NextQuestion()
    {
        Counter++;
        quizAnimator.SetTrigger("QuizClose");
        yield return new WaitForSeconds(loadingTime);   
        quizAnimator.SetTrigger("QuizOpen");  
        SetButtonJsonData();   
    }
    public void TrueAnswer(AnswerButton clickedButton)
    {
        StartCoroutine(clickedButton.ChangeButtonColorAnimation(Color.green));
        StartCoroutine(NextQuestion());
    }
    public void FalseAnswer(AnswerButton clickedButton) 
    {
        aiController.AIResponse.Add(aiController.AIResponse[Counter]); 
        StartCoroutine(clickedButton.ChangeButtonColorAnimation(Color.red));
        StartCoroutine(answerButtons[aiController.AIResponse[Counter].RightAnswerIndex].ChangeButtonColorAnimation(Color.green));
        StartCoroutine(NextQuestion());
    }    
    public IEnumerator EndGame()
    {
        Counter = 0;
        yield return new WaitForSeconds(1);
        PanelController.Instance.SetPanelActive(PanelController.Instance.ResultPanel);
    }
}