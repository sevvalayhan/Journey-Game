using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private JsonProvider dialogueDataProvider;
    [SerializeField] private Button continueButton;
    [SerializeField] private GameObject inputButtons;
    [SerializeField] public Animator DialoguePanelAnimator;
    [SerializeField] private DialogueCanvasController dialogueCanvasController;
    [SerializeField] private GameObject[] FaceImages;
    private Queue<string> sentences;
    public int DialogueCounter;
    private void Start()
    {
        sentences = new Queue<string>();
    }
    public void Initialize()
    {
        DialogueCounter = 0;
        SetDialogueData();
    }
    public void SetDialogueData()
    {
        sentences.Clear();
        if (dialogueDataProvider != null && dialogueDataProvider.LoadedDialogueData != null)
        {
            foreach (Dialogue sentence in dialogueDataProvider.LoadedDialogueData)
            {
                foreach (string text in sentence.DialogueTexts)
                {
                    sentences.Enqueue(text);
                }
                Debug.Log(sentence + " sentence");
            }
            DisplayNextDialogue();
        }
    }
    public void DisplayNextDialogue()
    {
        Debug.Log(sentences.Count + " sentences count");
        if (sentences.Count.Equals(0))
        {
            EndDialogue();
        }
        else
        {
            string sentence = sentences.Dequeue();
            StartCoroutine(TypeSentences(sentence));
        }
        DialogueCounter++;
    }
    IEnumerator TypeSentences(string sentence)
    {
        dialogueCanvasController.ActivatedCanvas();
        continueButton.gameObject.SetActive(false);
        nameText.text = dialogueDataProvider.LoadedDialogueData[DialogueCounter].Name;
        foreach (var image in FaceImages)
        {
            image.SetActive(false);
        }
        FaceImages[dialogueDataProvider.LoadedDialogueData[DialogueCounter].FaceSpriteIndex].SetActive(true);
        Debug.Log(nameText.text = dialogueDataProvider.LoadedDialogueData[DialogueCounter].Name);
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            yield return new WaitForSeconds(0.1f);
            dialogueText.text += letter;
        }
        continueButton.gameObject.SetActive(true);      
    }
    public void EndDialogue()
    {
        Debug.Log("bitti");
        dialogueCanvasController.DeactivateCanvasWithDelay(1f);
    }
}