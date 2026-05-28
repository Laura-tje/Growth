using UnityEngine;

public class UIManager : MonoBehaviour
{
    private enum Menu
    {
        StartScreen,
        PlayMode,
        PauseMode,
        OptionsMode,
        EndScreen,
    }

    private Menu currentState;
    
    private GameObject PauseMenu;
    private GameObject OptionsMenu;
    private GameObject BeginScreen;
    private GameObject EndScreen;
    
    private GameObject PauseButton;

    
    
    
    void Start()
    {
        PauseMenu =  GameObject.Find("PauseMenu");
        BeginScreen = GameObject.Find("BeginScreen");
        EndScreen = GameObject.Find("EndScreen");
        OptionsMenu = GameObject.Find("OptionsMenu");
        
        PauseButton = GameObject.Find("PauseButton");

        
        currentState = Menu.StartScreen;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case Menu.StartScreen:
                BeginScreen.SetActive(true);
                PauseMenu.SetActive(false);
                EndScreen.SetActive(false);
                
                PauseButton.SetActive(false);
                break;
            case Menu.PlayMode:
                BeginScreen.SetActive(false);
                PauseMenu.SetActive(false);
                EndScreen.SetActive(false);
                
                PauseButton.SetActive(true);
                break;
            case Menu.PauseMode:
                BeginScreen.SetActive(false);
                PauseMenu.SetActive(true);
                OptionsMenu.SetActive(false);
                EndScreen.SetActive(false);
                
                PauseButton.SetActive(false);
                break;
            case Menu.OptionsMode:
                BeginScreen.SetActive(false);
                PauseMenu.SetActive(false);
                OptionsMenu.SetActive(true);
                EndScreen.SetActive(false);
                
                PauseButton.SetActive(false);
                break;
            case Menu.EndScreen:
                BeginScreen.SetActive(false);
                PauseMenu.SetActive(false);
                EndScreen.SetActive(true);
                
                PauseButton.SetActive(false);
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
