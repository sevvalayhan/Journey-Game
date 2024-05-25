using Newtonsoft.Json;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class OpenAIController : MonoBehaviour
{
    [field:SerializeField] public TextMeshProUGUI DialogueText {get; set; }
    [field:SerializeField] public TMP_InputField InputText { get; set; }
    [field:SerializeField] public TextMeshProUGUI ClueText { get; set; }    
    [field: SerializeField] public Button OkButton;
    private OpenAIAPI api;
    public List<ChatMessage> messages;
    public List<Question> AIResponse;
    public JsonProvider DatabaseController;
    public QuizController questionController;
    public const string OPENAI_API_KEY = "OPENAI_API_KEY";
    private int counter;
    private void Awake()
    {
        api = new OpenAIAPI(Environment.GetEnvironmentVariable(OPENAI_API_KEY, EnvironmentVariableTarget.User));                
    }
    private void Start()
    {
        OpenAIInitialize(LevelController.CurrentLevel.ToString());
    }
    public void OpenAIInitialize(string currentLevelName) 
    {         
        StartConversation();
        GetJsonResponse(currentLevelName);
        AIResponse = new List<Question>();
        counter = 0;
    }
    private void StartConversation()
    {
        messages = new List<ChatMessage>();      
    }
    public async void GetResponse() 
    {       
        Debug.Log(counter);
        if (counter < DatabaseController.InformationalData.Count)
        {
            OkButton.gameObject.SetActive(false); 
            ChatMessage userMessage = new ChatMessage();
            userMessage.Role = ChatMessageRole.User;
            userMessage.Content = DatabaseController.InformationalData[counter].InformationText;
            if (userMessage.Content.Length > 100)
            {
                userMessage.Content = userMessage.Content.Substring(0, 100);
            }
            messages.Add(userMessage);
            DialogueText.text = string.Concat($"You: {userMessage.Content}");
            InputText.text = "";
            var chatResult = await api.Chat.CreateChatCompletionAsync(new ChatRequest()
            {
                Model = Model.ChatGPTTurbo,
                Temperature = 0.1,
                MaxTokens = 500,
                Messages = messages
            });
            //Get the Response message
            ChatMessage responseMessage = new ChatMessage();
            responseMessage.Role = chatResult.Choices[0].Message.Role;
            responseMessage.Content = chatResult.Choices[0].Message.Content;
            //Add the response to the list of message
            messages.Add(responseMessage);
            //Update the text field with the response
            DialogueText.text = string.Format($"You: {userMessage.Content}\n\nGuard: {responseMessage.Content}");
            await Task.Delay(5000);
            OkButton.gameObject.SetActive(true); 
        }
        else
        {
            Debug.Log("Bölüm Sonu");           
        }
        counter++;
    }      
    public async void GetJsonResponse(string currentLevelName) 
    {
        if (await DatabaseController.LoadInformationText(currentLevelName))
        {
            ChatMessage userMessage = new ChatMessage();
            userMessage.Role = ChatMessageRole.User;
            userMessage.Content = DatabaseController.InformationText;
            messages.Add(userMessage);
            var chatResult = await api.Chat.CreateChatCompletionAsync(new ChatRequest()
            {
                Model = Model.ChatGPTTurbo,
                Temperature = 0.1,
                MaxTokens = 500,
                Messages = messages
            });
            ChatMessage responseMessage = new ChatMessage();
            responseMessage.Role = chatResult.Choices[0].Message.Role;
            responseMessage.Content = chatResult.Choices[0].Message.Content;
            messages.Add(responseMessage);
            AIResponse = JsonConvert.DeserializeObject<List<Question>>(responseMessage.Content);
            Debug.Log(AIResponse.ToString());
        }
    }
    public async void GetClueResponse()
    {
        //þu anki soruya kopya verecek
        //bu metodu clue butonunun içerisine koyduk
        ChatMessage question = new ChatMessage();
        question.Role = ChatMessageRole.User;
        question.Content = DatabaseController.LoadedQuestions[questionController.Counter].QuestionText + " bu soruya bir ipucu metni hazýrlar mýsýn? Bu ipucu metni bir soru cevap yarýþmasýnda kullanýcýya doðru cevabý düþündürmeye yönelik olacak. Yani doðrudan doðru cevabý söylememeliyiz.";
        if (question.Content.Length > 100)
        {
            question.Content = question.Content.Substring(0, 100);
        }
        messages.Add(question);
        ClueText.text = string.Concat($"You: {question.Content}");
        var chatResult = await api.Chat.CreateChatCompletionAsync(new ChatRequest()
        {
            Model = Model.ChatGPTTurbo,
            Temperature = 0.1,
            MaxTokens = 500,
            Messages = messages
        });
        ChatMessage responseMessage = new ChatMessage();
        responseMessage.Role = chatResult.Choices[0].Message.Role;
        responseMessage.Content = chatResult.Choices[0].Message.Content;
        //Add the response to the list of message
        messages.Add(responseMessage);
        //Update the text field with the response
        Debug.Log(responseMessage.Content);
        PanelController.Instance.CluePanel.SetActive(true);
        ClueText.text = string.Concat($"{responseMessage.Content}");
        Debug.Log("Tamamlandý");
    }
}
