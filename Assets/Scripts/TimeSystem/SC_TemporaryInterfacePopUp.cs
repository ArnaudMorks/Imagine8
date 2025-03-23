
// [Summary] (By Wessel)
//
// This script is for showcasing how you can link it to any pop up!
//

using UnityEngine;

namespace TimeSystem
{
    [RequireComponent(typeof(SC_TimeEvent))]
    public class SC_TemporaryInterfacePopUp : MonoBehaviour
    {
        // Changeable
        [SerializeField] private GameObject _popUpObject;

        // My manager
        private SC_TimeManager _manager;


        // ----------------- Functions -----------------


        #region Start Functions

        // First thing this script does.
        private void Start()
        {
            SubscribeToTimeEvent();

            AssignTimeManager();

            //Expand..
        }

        // Subscribes to the time event script on this game object.
        private void SubscribeToTimeEvent()
        {
            if (TryGetComponent<SC_TimeEvent>(out var tmpComponent))
            {
                this.gameObject.GetComponent<SC_TimeEvent>().OnTimeEventComplete += TimeToShowPopUp;
            }
            else
            {
                Debug.LogError("Has no SC_TimeEvent Component!");
            }
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

        #endregion

        #region Time Event Subscription Functions

        // Time event result logic.
        private void TimeToShowPopUp()
        {
            if (_popUpObject != null)
            {
                _popUpObject.SetActive(true);
                PauseTime();
            }
        }

        #endregion

        #region Pause & Resume Functions

        // Tell the time manager to stop or run time.
        private void PauseTime() => _manager.RunningIngameTime(false);
        public void ResumeTime() => _manager.RunningIngameTime(true);

        #endregion

    }
}