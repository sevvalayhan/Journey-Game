using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface IQuestionProvider 
{
    public List<Question> LoadedQuestions { get; set; }
    public Task<bool> TryLoadQuestion(string levelName);//flagName koymadýk çünkü metodun çaðýrýldýðý yerden flagName e ulaþmak zor, gerek yok, her bölümde bir jsona ulaþýlacak
}
