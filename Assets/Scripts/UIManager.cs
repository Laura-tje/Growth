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
    
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject OptionsMenu;
    [SerializeField] GameObject BeginScreen;
    [SerializeField] GameObject EndScreen;
    [SerializeField] GameObject PauseButton;

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
        currentState = Menu.EndScreen;
        UpdateUI();
    }

    private void UpdateUI()
    {
        BeginScreen.SetActive(currentState == Menu.StartScreen);
        PauseMenu.SetActive(currentState == Menu.PauseMode);
        OptionsMenu.SetActive(currentState == Menu.OptionsMode);
        EndScreen.SetActive(currentState == Menu.EndScreen);
        
        PauseButton.SetActive(currentState == Menu.PlayMode);
    }
}
