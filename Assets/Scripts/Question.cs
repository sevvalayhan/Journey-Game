using System.Collections;
using System.Collections.Generic;
public class Question 
{
    public string QuestionText{ get; set; }
    public List<string> Answers { get; set; }
    public  int RightAnswerIndex { get; set; }
    public Question(int rightAnswerIndex, List<string> answers, string questionText)
    {
        RightAnswerIndex = rightAnswerIndex;
        Answers = answers;
        QuestionText = questionText;
    }
}