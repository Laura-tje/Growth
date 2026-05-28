using UnityEngine;

public class Utilitis : MonoBehaviour
{
    public void EnterMainScene()
    {

    }

    public void EnterBeginScene()
    {

    }

    public void EnterEndScene()
    {

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
