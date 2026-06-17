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
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        playerInput = GameObject.Find("Player").GetComponent<PlayerInput>();

        currentState = Menu.StartScreen;
        UpdateUI();
    }

    void Update()
    {
        bool justFound = false;

        if (eventSystem == null)
        {
            GameObject EventSystem = GameObject.Find("EventSystem");
            if (EventSystem != null)
            {
                eventSystem = EventSystem.GetComponent<EventSystem>();
                justFound = true;
            }
        }

        if (playerInput == null)
        {
            GameObject player = GameObject.Find("Player");
            if (player != null)
            {
                playerInput = player.GetComponent<PlayerInput>();
                justFound = true;
            }
        }

        if (justFound)
        {
            UpdateUI(); // pas nu kan de selectie en action map correct gezet worden
        }
    }
    
    
    
    public void PauseButtonClicked()
    {
        Sound_Manage_III.Instance._Play_Sound(1);
        currentState = Menu.PauseMode;
        UpdateUI();
    }

    public void PlayButtonClicked()
    {
        Sound_Manage_III.Instance._Play_Sound(1);
        currentState = Menu.PlayMode;
        UpdateUI();
    }

    public void RestartButtonClicked()
    {
        Sound_Manage_III.Instance._Play_Sound(1);
        currentState = Menu.StartScreen;
        UpdateUI();
    }

    public void OptionsButtonClicked()
    {
        Sound_Manage_III.Instance._Play_Sound(1);
        currentState = Menu.OptionsMode;
        UpdateUI();
    }

    public void QuitButtonClicked()
    {
        Sound_Manage_III.Instance._Play_Sound(1);
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
                Debug.Log("player");
            }
            else
            {
                playerInput.SwitchCurrentActionMap("UI");
                Debug.Log("UI");
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
