
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SC_TemporaryMouseInputManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset _playerControls;

    private InputAction _mousePointerAction;
    private InputAction _mouseClickAction;

    public Action<Vector2> OnLeftMouseClick;

    // Singleton reference
    public static SC_TemporaryMouseInputManager Instance { get; private set; } // Uppercase?


    // ----------------- Functions -----------------


    void Awake()
    {
        Singleton();

        var actionMap = _playerControls.FindActionMap("Player");
        _mousePointerAction = actionMap.FindAction("MousePointer");
        _mouseClickAction = actionMap.FindAction("LeftMouseClick");

        _mousePointerAction.Enable();
        _mouseClickAction.Enable();

        _mouseClickAction.performed += context =>
        {
            Vector2 mouseScreenPosition = _mousePointerAction.ReadValue<Vector2>();
            OnLeftMouseClick?.Invoke(mouseScreenPosition);
        };
    }

    // Sets up singleton logic.
    private void Singleton()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Persist across scenes.
        }
        else
        {
            Destroy(gameObject);  // Destroy duplicate instances.
        }
    }

    void Update()
    {
        /*
        Vector2 mouseScreenPosition = _mousePositionAction.ReadValue<Vector2>();
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(
            new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, Camera.main.nearClipPlane)
        );

        Debug.Log($"Mouse Screen Position: {mouseScreenPosition}");
        Debug.Log($"Mouse World Position: {mouseWorldPosition}");
        */
    }
}