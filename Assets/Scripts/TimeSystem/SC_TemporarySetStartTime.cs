
// [Summary] (By Wessel)
//
// This script is for showcasing how you can modify the start time of the time manager!
//

using TimeSystem;
using UnityEngine;

namespace TimeSystem
{
    public class SC_TemporarySetStartTime : MonoBehaviour
    {
        // Changeable
        [SerializeField] private float _startTimeInHours = 0;

        // My manager
        private SC_TimeManager _manager;

        #region Start Functions

        // Start Functions
        private void Start()
        {
            AssignTimeManager();

            ChangeTheStartTime();
        }

        // Tries to assigns the time manager.
        private void AssignTimeManager()
        {
            if (SC_TimeManager.Instance == null)
            {
                Debug.LogWarning("No SC_TimeManager Instance found!");
                return;
            }

            if (SC_TimeManager.Instance.TryGetComponent<SC_TimeManager>(out SC_TimeManager manager))
            {
                _manager = manager;
            }
        }

        // Changes the current time of the time manager.
        private void ChangeTheStartTime()
        {
            if (_manager != null)
                _manager.ChangeInGameTime(_startTimeInHours * 60 * 60);
        }

        #endregion

    }
}
