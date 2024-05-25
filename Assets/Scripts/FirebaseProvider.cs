using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Firebase;
using Firebase.Database;
using System;
using Newtonsoft.Json;
public class FirebaseProvider : MonoBehaviour,IQuestionProvider
{ 
    public List<Question> LoadedQuestions { get; set; }
    private DatabaseReference reference;
    private static FirebaseProvider instance;
    public static FirebaseProvider Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<FirebaseProvider>();
            }
            return instance;
        }
    }       
    private void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            reference = FirebaseDatabase.DefaultInstance.RootReference;
        });
    }
    public async Task<bool> TryLoadQuestion(string categoryName)
    {
        try
        {            
            var categorySnapshot = await reference.Child("Quizes").Child(categoryName).GetValueAsync();
            if (!categorySnapshot.Exists)
            {
                Debug.LogError("Error loading questions: Category snapshot does not exist.");
            }
            string questionJson = categorySnapshot.GetRawJsonValue();
            LoadedQuestions = JsonConvert.DeserializeObject<List<Question>>(questionJson);
            LoadedQuestions.RemoveAll((question) => question == null);
            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError("Error loading questions: " + ex);
            return false;
        }
    }
    public async Task TrySaveQuestion(Question newQuestion, string categoryName, int questionIndex) //Editor icin
    {
        try
        {
            string jsonString = JsonConvert.SerializeObject(newQuestion);
            await reference.Child("Quizes").Child(categoryName).Child(questionIndex.ToString()).SetRawJsonValueAsync(jsonString);
        }
        catch (Exception ex)
        {
            Debug.LogError("Error saving questions:" + ex);
            throw;
        }
    }   
}