
// [Summary] (By Wessel)
//
// This script is for testing action subscriptions.
//

using System;
using UnityEngine;

public class SC_InputTester : MonoBehaviour
{
    // Changeable
    [Header("Debugging")]
    [SerializeField] private string _inputText = "Input Received";

    // My input handler
    private SC_InputHandler _inputHandler;


    // ----------------- Functions -----------------


    #region OnEnable Functions

    // On enable changes.
    private void OnEnable()
    {
        TryToGetTimeManager();

        if (_inputHandler != null)
            _inputHandler.OnLeftMouseClick += InputResult; // Rename this for testing other actions!

        //Expand..
    }

    // Tries to get the input handler singleton.
    private void TryToGetTimeManager()
    {
        if (SC_InputHandler.Instance == null)
        {
            Debug.LogWarning("InputHandler instance is missing!");
            return;
        }

        _inputHandler = SC_InputHandler.Instance;
    }

    #endregion

    #region OnDisable Functions

    // On enable changes.
    private void OnDisable()
    {
        TryToGetTimeManager();

        if (_inputHandler != null) // Bug Potential, be careful!
            _inputHandler.OnLeftMouseClick -= InputResult; // Rename this for testing other actions!

        //Expand..
    }

    #endregion

    #region Subscription Functions

    // The input result.
    private void InputResult()
    {
        Debug.Log(_inputText + " (" + this.gameObject.name + ")");

        // Expand..
    }

    #endregion

}