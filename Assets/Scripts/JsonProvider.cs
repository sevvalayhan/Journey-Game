using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
public class JsonProvider : MonoBehaviour, IDialogueProvider, IQuestionProvider,IDatabaseProvider
{
    public List<Dialogue> LoadedDialogueData { get; set; }
    public List<Question> LoadedQuestions { get; set; }
    public List<InformationData> InformationalData { get; set; }
    [NonSerialized] public string InformationText;
    public async Task<bool> TryLoadQuestion(string levelName)
    {
        string path = Path.Combine(Application.dataPath, "JsonData","QuestionJson",levelName + ".json");
        if (File.Exists(path))
        {
            var jsonFile = await File.ReadAllTextAsync(path);
            LoadedQuestions = JsonConvert.DeserializeObject<List<Question>>(jsonFile);
            return true;
        }
        else
        {
            LoadedQuestions = new List<Question>();
            return false;
        }
    }
    public bool TryLoadDialogue(string levelName, string flagName)
    {
        string path = Path.Combine(Application.dataPath,"JsonData", "DialogueJson", flagName + ".json");
        if (File.Exists(path))
        {
            string jsonFile =  File.ReadAllText(path);
            LoadedDialogueData = JsonConvert.DeserializeObject<List<Dialogue>>(jsonFile);
            return true;
        }
        else
        {
            LoadedDialogueData = new List<Dialogue>();
            return false;
        }
    }
    public async Task<bool> LoadInformationData(string flagName)
    {
        string path = Path.Combine(Application.dataPath, "JsonData","informationData" + ".json");
        string jsonData = await File.ReadAllTextAsync(path);
        //if (File.Exists(jsonData))
        if (jsonData is not null)
        {
            InformationalData = JsonConvert.DeserializeObject<List<InformationData>>(jsonData);
            return  true;
        }
        else
        {
            InformationalData = new List<InformationData>();
            return false;
        }
    }
    public async Task<bool> LoadInformationText(string currentLevelName)
    {
        string path = Path.Combine(Application.dataPath, "InformationTextData", "chatgpt" ,currentLevelName+ ".txt");
        string textData = await File.ReadAllTextAsync(path);
        Debug.Log(textData);
        if (textData is not null)
        {
            InformationText = textData;
            return true;
        }
        else
        {
            throw new Exception("Text file is not null");
        }
    }
}