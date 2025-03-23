
// [Summary] (By Wessel)
//
// This is a temporary script that is in charge of handling interface events,
// by turning them into easy to use actions. IMPORTANT, this must be placed
// on a canvas object to register anything at all, hence the RequireComponent!
//

using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Canvas))]
public class SC_TemporaryInterfaceEventHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Anti-duplicate protection
    public static SC_TemporaryInterfaceEventHandler Instance { get; private set; }

    // Actions
    public Action<bool> OnPointerOverUI;


    // ----------------- Functions -----------------


    #region OnEnable Functions

    // Changes that happen on enable.
    private void OnEnable()
    {
        DuplicateReminder();

        //Expand..
    }

    // Allows easy access, while stopping duplicates.
    private void DuplicateReminder()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogWarning("Multiple SC_TemporaryInterfaceEventHandler instances found. Only the first one will be used.");
    }

    #endregion

    #region OnDisable Functions

    // Changes that happen on disable.
    private void OnDisable()
    {
        RemoveAsInstance();

        //Expand..
    }

    // Just incase you disable it.
    private void RemoveAsInstance()
    {
        if (Instance == this)
        {
            Debug.LogWarning("I don't recommend disabling the SC_TemporaryInterfaceEventHandler handler.");
            Instance = null;
        }
    }

    #endregion

    #region OnPointer Functions

    // Allows other scripts to easily access on pointer data.
    public void OnPointerEnter(PointerEventData eventData) => OnPointerOverUI?.Invoke(true);
    public void OnPointerExit(PointerEventData eventData) => OnPointerOverUI?.Invoke(false);

    #endregion
}
