using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlls : MonoBehaviour
{
    [Header("Player Controlls")]
    [SerializeField] private PlayerInput inputSystem;

    private InputActionMap _currentMap;

    private InputAction _moveAction;
    public Vector3 move { get; private set; }
    [SerializeField] private float M_Speed;

    private void Awake()
    {
        _currentMap = inputSystem.currentActionMap;
        _moveAction = _currentMap.FindAction("Move");
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        move = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            transform.Translate(move * M_Speed * Time.deltaTime);
           // transform.GetComponent<Rigidbody>().AddForce()
    }
}
