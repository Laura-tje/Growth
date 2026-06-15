using UnityEngine;

public class UIManager : MonoBehaviour
{
    private enum Menu
    {
        StartScreen,
        PlayMode,
        PauseMode,
        OptionsMode,
    }

    private Menu currentState;
    
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject OptionsMenu;
    [SerializeField] GameObject BeginScreen;
    [SerializeField] GameObject PauseButton;
    [SerializeField] GameObject FadeBackground;

    public static UIManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this && instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    
    void Start()
    {
        currentState = Menu.StartScreen;
    }
    
    public void PauseButtonClicked()
    {
        currentState = Menu.PauseMode;
        UpdateUI();
    }

    public void PlayButtonClicked()
    {
        currentState = Menu.PlayMode;
        UpdateUI();
    }

    public void RestartButtonClicked()
    {
        currentState = Menu.StartScreen;
        UpdateUI();
    }

    public void OptionsButtonClicked()
    {
        currentState = Menu.OptionsMode;
        UpdateUI();
    }

    public void QuitButtonClicked()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        BeginScreen.SetActive(currentState == Menu.StartScreen);
        PauseMenu.SetActive(currentState == Menu.PauseMode);
        FadeBackground.SetActive(currentState == Menu.PauseMode || currentState == Menu.OptionsMode);
        OptionsMenu.SetActive(currentState == Menu.OptionsMode);
        
        PauseButton.SetActive(currentState == Menu.PlayMode);
    }
}
