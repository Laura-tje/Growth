using UnityEngine;
using UnityEngine.SceneManagement;


public class Utilitis : MonoBehaviour
{
    public void Start()
    {
        Time.timeScale = 0f;
    }
    
    public void StartGame()
    {
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void EndGame()
    {
        
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f;
    }


}
