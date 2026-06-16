using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem; // Nodig voor PlayerInput

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
    
    [Header("Menu Panels")]
    [SerializeField] private GameObject BeginScreen;
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject OptionsMenu;
    [SerializeField] private GameObject FadeBackground;
    [SerializeField] private GameObject PauseButton;
    
    [Header("First Selected Buttons (Joystick)")]
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject resumeButton;
    [SerializeField] private GameObject soundToggle;
    
    [Header("References")]
    private EventSystem eventSystem;
    private PlayerInput playerInput;

    public static UIManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } 
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        currentState = Menu.StartScreen;
        UpdateUI();
    }

    void Update()
    {
        if (eventSystem == null)
        {
            eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        }

        if (playerInput == null)
        {
            playerInput = GameObject.Find("Player").GetComponent<PlayerInput>();
        }
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
        Debug.Log("Game Afgesloten");
        Application.Quit();
    }

    private void UpdateUI()
    {
        // 1. Schakel de juiste UI schermen in of uit
        BeginScreen.SetActive(currentState == Menu.StartScreen);
        PauseMenu.SetActive(currentState == Menu.PauseMode);
        FadeBackground.SetActive(currentState == Menu.PauseMode || currentState == Menu.OptionsMode);
        OptionsMenu.SetActive(currentState == Menu.OptionsMode);
        PauseButton.SetActive(currentState == Menu.PlayMode);

        // 2. Wissel tussen Player en UI input (voorkomt bewegen tijdens pauze)
        if (playerInput != null)
        {
            if (currentState == Menu.PlayMode)
            {
                playerInput.SwitchCurrentActionMap("Player");
            }
            else
            {
                playerInput.SwitchCurrentActionMap("UI");
            }
        }

        // 3. Reset de huidige selectie (cruciaal voor Unity EventSystem + Joystick)
        if (eventSystem != null)
        {
            eventSystem.SetSelectedGameObject(null);

            // 4. Forceer de joystick-focus naar de juiste knop voor deze state
            switch (currentState)
            {
                case Menu.StartScreen:
                    eventSystem.SetSelectedGameObject(startButton);
                    break;
                case Menu.PlayMode:
                    eventSystem.SetSelectedGameObject(pauseButton);
                    break;
                case Menu.PauseMode:
                    eventSystem.SetSelectedGameObject(resumeButton);
                    break;
                case Menu.OptionsMode:
                    eventSystem.SetSelectedGameObject(soundToggle);
                    break;
            }
        }
    }
}
