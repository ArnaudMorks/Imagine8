
// [Summary] (By Wessel)
//
// This script is for showcasing how you can link it to any type of script!
//

using UnityEngine;

namespace TimeSystem
{
    [RequireComponent(typeof(SC_TimeEvent))]
    public class SC_TimeTestResult : MonoBehaviour
    {
        [SerializeField] private string _MyConsoleMessage = "Yippi!";


        // ----------------- Functions -----------------


        #region Start Functions

        // First thing this script does.
        private void Start()
        {
            SubscribeToTimeEvent();

            //Expand..
        }

        // Subscribes to the time event script on this game object.
        private void SubscribeToTimeEvent()
        {
            if (TryGetComponent<SC_TimeEvent>(out var tmpComponent))
            {
                this.gameObject.GetComponent<SC_TimeEvent>().OnTimeEventComplete += ItsAboutTime;
            }
            else
            {
                Debug.LogError("Has no SC_TimeEvent Component!");
            }
        }

        #endregion

        #region Time Event Subscription Function

        // Time event result logic.
        private void ItsAboutTime() => Debug.Log(_MyConsoleMessage + " (" + this.name + ")");

        #endregion
    }
}