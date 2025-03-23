
// [Summary] (By Wessel)
//
// This script is the center of all inputs,
// by turning them into actions which more performance friendly.
//
// Great tutorial explaining most of this amazing script!
// https://youtu.be/lclDl-NGUMg
//

using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SC_InputHandler : MonoBehaviour
{
    // Changeable
    [Header("Required Asset")]
    [SerializeField, Tooltip("The Input Action Asset that's being used")]
    private InputActionAsset _playerControls;

    [Header("Required Map Reference")]
    [SerializeField, Tooltip("The Reference to the input action map name")]
    private string _actionMapName = "Player";

    [Header("Required References")]
    [SerializeField, Tooltip("The Reference to the Action name")]
    private string _leftMouseClickName = "LeftMouseClick";
    [SerializeField, Tooltip("The Reference to the Action name")]
    private string _escapeKeyName = "EscapeKey";

    // Singleton reference
    public static SC_InputHandler Instance { get; private set; }

    // Inputs
    private InputAction _leftMouseClickInput;
    private InputAction _escapeKeyInput;

    // Actions
    public Action OnLeftMouseClick;
    public Action OnEscapeKey;


    // ----------------- Functions -----------------


    #region Awake Functions

    // Absolute first thing this script does.
    private void Awake()
    {
        Singleton();
        AssignActions();
        RegisterInputActions();

        // Expand..
    }

    // Sets up singleton logic..
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

    // Assigns the actions based on the given references.
    private void AssignActions()
    {
        InputActionMap actionMap = _playerControls.FindActionMap(_actionMapName);
        _leftMouseClickInput = actionMap.FindAction(_leftMouseClickName);
        _escapeKeyInput = actionMap.FindAction(_escapeKeyName);
        // Expand Assignments..
    }

    // Register the actions like a event.
    private void RegisterInputActions()
    {
        _leftMouseClickInput.started += context => OnLeftMouseClick?.Invoke();
        _escapeKeyInput.started += context => OnEscapeKey?.Invoke();
        // Expand Invokes..
    }

    #endregion

    #region OnEnable Functions

    // On enable changes.
    private void OnEnable()
    {
        EnableAllActions();

        // Expand..
    }

    // Enables all actions.
    private void EnableAllActions()
    {
        _leftMouseClickInput.Enable();
        _escapeKeyInput.Enable();
        // Expand Inputs..
    }

    #endregion

    #region OnDisable Functions

    // On disable changes.
    private void OnDisable()
    {
        DisableAllActions();

        // Expand..
    }

    // Disables all actions.
    private void DisableAllActions()
    {
        _leftMouseClickInput.Disable();
        _escapeKeyInput.Disable();
        // Expand Inputs..
    }

    #endregion

}