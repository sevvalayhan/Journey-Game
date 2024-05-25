using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class QuestionAttacherWindow : EditorWindow
{
    FirebaseProvider questionProvider;
    private string categoryName;
    private string questionText;
    private string answer1;
    private string answer2;
    private string answer3;
    private int rightAnswerIndex;
    private int questionIndex;
    [MenuItem("Window/QuestionAttacher")]
    public static void ShowWindow()
    {
        GetWindow<QuestionAttacherWindow>("Question Attacher");
    }
    private void OnEnable()
    {
        questionProvider = FirebaseProvider.Instance;
    }
     async void OnGUI()
    {
        questionProvider = FirebaseProvider.Instance;
        GUILayout.Label("Kategori adlar�: science, cografya, art, philosophy,music", EditorStyles.boldLabel);
        questionProvider = EditorGUILayout.ObjectField("Script Reference", questionProvider, typeof(FirebaseProvider), false) as FirebaseProvider;
        categoryName = EditorGUILayout.TextField("Kategori Ad�: ", categoryName);
        questionText = EditorGUILayout.TextField("Soru metni: ", questionText);
        answer1 = EditorGUILayout.TextField("answers[0]: ", answer1);
        answer2 = EditorGUILayout.TextField("answers[1]: ", answer2);
        answer3 = EditorGUILayout.TextField("answers[2]: ", answer3);
        rightAnswerIndex = EditorGUILayout.IntField("rightAnswerIndex: ", rightAnswerIndex);
        questionIndex = EditorGUILayout.IntField("QuestionIndex:", questionIndex);
        var answerList = new List<string>() { answer1, answer2, answer3 };
        Question newQuestion = new Question(rightAnswerIndex,answerList,questionText);
        if (GUILayout.Button("Kategoriye Yeni Soru Kaydet veya G�ncelle"))
        {
            await questionProvider.TrySaveQuestion(newQuestion, categoryName, questionIndex);
        }
    }
}