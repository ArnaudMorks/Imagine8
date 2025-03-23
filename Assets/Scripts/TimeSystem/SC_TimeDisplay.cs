
// [Summary] (By Wessel)
//
// This a script for simply displaying a time on a tmp component.
//

using TMPro;
using UnityEngine;

namespace TimeSystem
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class SC_TimeDisplay : MonoBehaviour
    {
        // Changeable
        [SerializeField] private SC_TimeFormatTypeEnum _timeDisplayFormat;

        // My manager
        private SC_TimeManager _manager;

        // My text
        private TextMeshProUGUI _timeText;


        // ----------------- Functions -----------------


        #region Start Functions

        // First thing this script does.
        private void Start()
        {
            GetTextMeshProComponent();

            //Expand..
        }

        // Subscribes to the time event script on this game object.
        private void GetTextMeshProComponent()
        {
            if (TryGetComponent<TextMeshProUGUI>(out var tmpComponent))
            {
                _timeText = GetComponent<TextMeshProUGUI>();
                _timeText.enabled = false;
            }
            else
            {
                Debug.LogError("Has no TextMeshProUGUI Component!");
            }
        }

        #endregion

        #region OnEnable Functions

        // On enable changes.
        private void OnEnable()
        {
            TryToGetTimeManager();

            SubscribeToTimeManager();

            // Expand..
        }

        // Tries to get the time manager singleton.
        private void TryToGetTimeManager()
        {
            if (SC_TimeManager.Instance == null)
            {
                Debug.LogWarning("TimeManager instance is missing!");
                return;
            }

            _manager = SC_TimeManager.Instance;
        }

        #endregion

        #region OnDisable Functions

        // On disable changes.
        private void OnDisable()
        {
            UnSubscribeToTimeManager();

            // Expand..
        }

        #endregion

        #region Time Subscription Functions

        private void SubscribeToTimeManager() => _manager.OnSecondPassed += UpdateTime;
        private void UnSubscribeToTimeManager() => _manager.OnSecondPassed -= UpdateTime;

        void UpdateTime()
        {
            SC_TimeManager manager = SC_TimeManager.Instance;

            switch (_timeDisplayFormat)
            {
                case SC_TimeFormatTypeEnum.HMS:
                    _timeText.text = manager.GetTimeToStringHMS(manager.GetCurrentTime);
                    break;
                case SC_TimeFormatTypeEnum.HM:
                    _timeText.text = manager.GetTimeToStringHM(manager.GetCurrentTime);
                    break;
            }

            if (_timeText.enabled == false)
                _timeText.enabled = true;
        }

        #endregion

    }
}