using UnityEngine;

public class UIManager : MonoBehaviour
{
    private enum Menu
    {
        StartScreen,
        PlayMode,
        PauseMode,
        EndScreen,
    }

    private Menu currentState;
    
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject PauseButton;
    [SerializeField] private GameObject BeginScreen;
    [SerializeField] private GameObject EndScreen;
    
    
    
    void Start()
    {
        PauseMenu =  GameObject.Find("PauseMenu");
        PauseButton = GameObject.Find("PauseButton");
        BeginScreen = GameObject.Find("BeginScreen");
        EndScreen = GameObject.Find("EndScreen");
        
        currentState = Menu.StartScreen;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case Menu.StartScreen:
                BeginScreen.SetActive(true);
                PauseButton.SetActive(false);
                PauseMenu.SetActive(false);
                EndScreen.SetActive(false);
                break;
            case Menu.PlayMode:
                BeginScreen.SetActive(false);
                PauseButton.SetActive(true);
                PauseMenu.SetActive(false);
                EndScreen.SetActive(false);
                break;
            case Menu.PauseMode:
                BeginScreen.SetActive(false);
                PauseButton.SetActive(false);
                PauseMenu.SetActive(true);
                EndScreen.SetActive(false);
                break;
            case Menu.EndScreen:
                BeginScreen.SetActive(false);
                PauseButton.SetActive(false);
                PauseMenu.SetActive(false);
                EndScreen.SetActive(true);
                break;
        }
    }

    public void PauseButtonClicked()
    {
        currentState = Menu.PauseMode;
    }

    public void PlayButtonClicked()
    {
        currentState = Menu.PlayMode;
    }
}
