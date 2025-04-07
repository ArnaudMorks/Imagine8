
// [Summary] (By Wessel)
//
// This script is in charge of making sure the order of initialization is correct,
// simply to avoid null references and other annoyances.
//

using UnityEngine;

public class SC_TemporaryGameMaster : MonoBehaviour
{
    // Changeable
    [Header("Requires Manual Input")]
    [SerializeField] private GameObject[] _activationOrder;


    // ----------------- Functions -----------------


    #region Start Functions

    // First thing this script does.
    private void Start()
    {
        ActivateInOrder();

        // Expand..
    }

    // Activates the array in order.
    private void ActivateInOrder()
    {
        foreach (GameObject thisObject in _activationOrder)
        {
            if (thisObject == null)
                continue;

            thisObject.SetActive(true);
        }
    }

    #endregion

}