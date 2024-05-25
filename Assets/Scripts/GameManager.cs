using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static bool IsGameOver { get; set; }
    public static bool IsGameActive { get; set; } 
    void Start()
    {
        IsGameOver = false;
        IsGameActive = false;
    }
    public void OnGameOver()
    {
        GameManager.IsGameOver = true;
        GameManager.IsGameActive = false;  
    }   
    public void OnGameActive()
    {
        GameManager.IsGameActive = true;
        GameManager.IsGameActive = false;
    }
    public void OnGameStop()
    {
        GameManager.IsGameActive=false;
        GameManager.IsGameOver = false;
    }
    public void AppQuit()
    {
        Application.Quit();
    }    
}