using TimeSystem;
using UnityEngine;

public class SC_PauzeMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauzeMenu;

    private SC_InputHandler _inputHandler;
    private SC_TimeManager _timeManager;


    private void OnEnable()
    {
        TryToAssignInputHandler();
        TryToAssignTimeManager();
        SubscribeToOnEscapeKey();
    }

    private void OnDisable()
    {
        UnsubscribeToOnEscapeKey();
    }

    private void TryToAssignInputHandler()
    {
        if (SC_InputHandler.Instance == null)
        {
            Debug.LogWarning("InputHandler instance is missing!");
            return;
        }

        _inputHandler = SC_InputHandler.Instance;
    }

    private void TryToAssignTimeManager()
    {
        if (SC_TimeManager.Instance == null)
        {
            Debug.LogWarning("TimeManager instance is missing!");
            return;
        }

        _timeManager = SC_TimeManager.Instance;
    }

    private void SubscribeToOnEscapeKey() => _inputHandler.OnEscapeKey += TogglePauzeMenu;

    private void UnsubscribeToOnEscapeKey() => _inputHandler.OnEscapeKey += TogglePauzeMenu;

    private void TogglePauzeMenu()
    {
        if (_pauzeMenu == null) return;

        bool toggleState = !_pauzeMenu.activeSelf;

        _pauzeMenu.SetActive(toggleState);
        _timeManager.RunningIngameTime(!toggleState);
    }
}
