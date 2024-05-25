using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface IQuestionProvider 
{
    public List<Question> LoadedQuestions { get; set; }
    public Task<bool> TryLoadQuestion(string levelName);//flagName koymad�k ��nk� metodun �a��r�ld��� yerden flagName e ula�mak zor, gerek yok, her b�l�mde bir jsona ula��lacak
}
