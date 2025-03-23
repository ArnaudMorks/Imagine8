
// [Summary] (By Wessel)
//
// This script is in charge of making sure the order of initialization is correct,
// simply to avoid null references and other annoyances.
//

using UnityEngine;

namespace TimeSystem
{
    public class SC_TemporaryGameMaster : MonoBehaviour
    {

        [SerializeField] private GameObject[] _activationOrder;


        // ----------------- Functions -----------------


        #region Start Functions

        private void Start()
        {
            ActivateInOrder();

            // Expand..
        }

        private void ActivateInOrder()
        {
            foreach (GameObject thisObject in _activationOrder)
            {
                thisObject.SetActive(true);
            }
        }

        #endregion
    }
}