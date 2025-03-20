
// [Summary] (By Wessel)
//
// This a time event that keeps track of time and then does a action,
// which other scripts can easily use, it's also very modular!
//

// [To-Do]
// 1. Add start times set & get functions?
// 2. Add remaining times set & get functions?
//

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TimeSystem
{
    public class SC_TimeEvent : MonoBehaviour
    {
        // Changeable
        [Header("Time Variables")]
        [SerializeField, Range(0, 59)] private int _startSeconds;
        [SerializeField, Range(0, 59)] private int _startMinutes;
        [SerializeField, Range(0, 23)] private int _startHours;
        [SerializeField, Range(0, 6)] private int _startDays;

        [Header("Behaviour Variables")]
        [SerializeField] private bool _isStartOnEnable = false;
        [SerializeField] private bool _isDisableOnceDone = false;
        [SerializeField] private bool _isLooping = false;

        // Debugging
        [Header("Debugging Variables")]
        [SerializeField, Tooltip("Do you want to show subscription moments?")] 
        private bool _consoleShowSubscriptions = false;
        [SerializeField, Tooltip("Do you want to show notify once finished?")] 
        private bool _consoleShowFinished = false;
        [SerializeField, Tooltip("Do you want to show notify when resetting a loop?")] 
        private bool _consoleShowLooping = false;

        // My manager
        private SC_TimeManager _manager;

        // Remaining time
        private int _secondsRemaining = 0;
        private int _minutesRemaining = 0;
        private int _hoursRemaining = 0;
        private int _daysRemaining = 0;

        // Is doing something?
        private bool _isActive = false;

        // Actions
        public Action OnTimeEventComplete;

        // Subscription dictionary
        private Dictionary<SC_TimeTypeEnum, (bool isSubscribed, Action subscribeAction, Action unsubscribeAction)> _subscriptions;


        // ----------------- Functions -----------------


        #region Awake Functions

        // Absolute first thing this script does.
        private void Awake()
        {
            CreateDictionaryOptions();

            //Expand..
        }

        #endregion

        #region OnEnable Functions

        // On enable changes.
        private void OnEnable()
        {
            TryToGetTimeManager();

            if (_isStartOnEnable)
                StartTimeEvent();
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
            _isActive = false;

            UnsubscribeAll();
        }

        #endregion

        #region Subscription Functions

        // Adds all the dictionary options.
        private void CreateDictionaryOptions()
        {
            _subscriptions = new Dictionary<SC_TimeTypeEnum, (bool, Action, Action)>{
            { SC_TimeTypeEnum.SECOND, (false, SubscribeToOnSecond, UnsubscribeFromOnSecond) },
            { SC_TimeTypeEnum.MINUTE, (false, SubscribeToOnMinute, UnsubscribeFromOnMinute) },
            { SC_TimeTypeEnum.HOUR,   (false, SubscribeToOnHour,   UnsubscribeFromOnHour) },
            { SC_TimeTypeEnum.DAY,    (false, SubscribeToOnDay,    UnsubscribeFromOnDay) }};
        }

        // Tries to unsubscribe from all.
        private void UnsubscribeAll()
        {
            foreach (var key in _subscriptions.Keys.ToList()) // .ToList() prevents modification errors during iteration.
            {
                if (_subscriptions[key].isSubscribed) // Check if already subscribed.
                {
                    _subscriptions[key].unsubscribeAction.Invoke(); // Invoke the unsubscribe action.
                    _subscriptions[key] = (false, _subscriptions[key].subscribeAction, _subscriptions[key].unsubscribeAction); // Reset flag.
                }
            }
        }

        // Tries to subscribe based on given type.
        public void Subscribe(SC_TimeTypeEnum type)
        {
            var sub = _subscriptions[type];
            if (!sub.isSubscribed)
            {
                sub.subscribeAction.Invoke();
                _subscriptions[type] = (true, sub.subscribeAction, sub.unsubscribeAction);
            }
            else
            {
                Debug.Log(type + " : is already subscribed, this shouldn't be happening!");
            }
        }

        // Tries to unsubscribe based on given type.
        public void Unsubscribe(SC_TimeTypeEnum type)
        {
            var sub = _subscriptions[type];
            if (sub.isSubscribed)
            {
                sub.unsubscribeAction.Invoke();
                _subscriptions[type] = (false, sub.subscribeAction, sub.unsubscribeAction);
            }
            else
            {
                Debug.LogWarning(type + " : is already Unsubscribed, this shouldn't be happening!");
            }
        }

        // On second subscription.
        private void SubscribeToOnSecond() => _manager.OnSecondPassed += OnSecondPassedWaiter;
        private void UnsubscribeFromOnSecond() => _manager.OnSecondPassed -= OnSecondPassedWaiter;

        // On minute subscription.
        private void SubscribeToOnMinute() => _manager.OnMinutePassed += OnMinutePassedWaiter;
        private void UnsubscribeFromOnMinute() => _manager.OnMinutePassed -= OnMinutePassedWaiter;

        // On hour subscription.
        private void SubscribeToOnHour() => _manager.OnHourPassed += OnHourPassedWaiter;
        private void UnsubscribeFromOnHour() => _manager.OnHourPassed -= OnHourPassedWaiter;

        // On day subscription.
        private void SubscribeToOnDay() => _manager.OnDayPassed += OnDayPassedWaiter;
        private void UnsubscribeFromOnDay() => _manager.OnDayPassed -= OnDayPassedWaiter;

        #endregion

        #region Waiter Functions

        // Convert a subscription signal into something usable.
        private void OnSecondPassedWaiter() => WaitForCorrectTime(SC_TimeTypeEnum.SECOND, ref _secondsRemaining);
        private void OnMinutePassedWaiter() => WaitForCorrectTime(SC_TimeTypeEnum.MINUTE, ref _minutesRemaining);
        private void OnHourPassedWaiter() => WaitForCorrectTime(SC_TimeTypeEnum.HOUR, ref _hoursRemaining);
        private void OnDayPassedWaiter() => WaitForCorrectTime(SC_TimeTypeEnum.DAY, ref _daysRemaining);

        // Modify remaining time.
        private void WaitForCorrectTime(SC_TimeTypeEnum type, ref int timeRemaining)
        {
            if (!_subscriptions[type].isSubscribed)
                return; // Prevents further decrements if unsubscribed.

            timeRemaining -= 1;

            if (timeRemaining <= 0)
            {
                if (timeRemaining < 0)
                {
                    Debug.LogWarning("Corrected Negative Value, this shouldn't be happening!");
                    timeRemaining = 0;
                }

                Unsubscribe(type);
                CheckConditions();
            }
        }

        #endregion

        #region Behaviour Functions

        // Tries to set the remaining time as start time.
        private void StartToRemainingTime()
        {
            _secondsRemaining = _startSeconds;
            _minutesRemaining = _startMinutes;
            _hoursRemaining = _startHours;
            _daysRemaining = _startDays;

            if (_startSeconds <= 0 && _startMinutes <= 0 && _startHours <= 0 && _startDays <= 0)
            {
                Debug.LogWarning("Starting Time Variables are all 0, IsLooping is switched to false!");
                _isLooping = false;
                _isActive = false;
            }
        }

        // Checks what to subscribe to.
        private void CheckConditions()
        {
            if (_daysRemaining > 0)
            {
                if (_consoleShowSubscriptions)
                    Debug.Log("[DEBUG] Subscribing to Day: " + this.gameObject.name);
                Subscribe(SC_TimeTypeEnum.DAY);
                return;
            }
            else if (_hoursRemaining > 0)
            {
                if (_consoleShowSubscriptions)
                    Debug.Log("[DEBUG] Subscribing to Hour: " + this.gameObject.name);
                Subscribe(SC_TimeTypeEnum.HOUR);
                return;
            }
            else if (_minutesRemaining > 0)
            {
                if (_consoleShowSubscriptions)
                    Debug.Log("[DEBUG] Subscribing to Minute: " + this.gameObject.name);
                Subscribe(SC_TimeTypeEnum.MINUTE);
                return;
            }
            else if (_secondsRemaining > 0)
            {
                if (_consoleShowSubscriptions)
                    Debug.Log("[DEBUG] Subscribing to Second: " + this.gameObject.name);
                Subscribe(SC_TimeTypeEnum.SECOND);
                return;
            }

            // Finally time for the event!
            OnTimeEventComplete?.Invoke();

            // Try to Reset?
            TryToRestartTimeEvent();
        }

        // Tries to restart this time event.
        private void TryToRestartTimeEvent()
        {
            if (_startSeconds <= 0 && _startMinutes <= 0 && _startHours <= 0 && _startDays <= 0)
            {
                Debug.LogWarning("Starting Time Variables are all 0, loopEvent is switched to false!");
                _isLooping = false;
            }

            if (!_isLooping)
            {
                _isActive = false;
            }
            else
            {
                _isActive = true;

                if (_consoleShowLooping)
                    Debug.Log("[DEBUG] Loop is being reset: " + this.gameObject.name);

                StartToRemainingTime();
                CheckConditions();
            }

            if (_isActive == false)
            {
                if (_consoleShowFinished)
                    Debug.Log("[DEBUG] TimeEvent is finished: " + this.gameObject.name);

                if (_isDisableOnceDone)
                    this.gameObject.SetActive(false);
            }
        }

        #endregion

        #region Public Set Functions

        // Starts the time event manually.
        public void StartTimeEvent()
        {
            if (_isActive == true)
            {
                Debug.LogWarning("Time Event is already active, check if active first!");
                return;
            }

            _isActive = true;

            StartToRemainingTime();
            CheckConditions();
        }

        // Set a bool values.
        public void SetIsStartOnEnableValue(bool direction) => _isStartOnEnable = direction;
        public void SetIsDisableOnceDoneValue(bool direction) => _isDisableOnceDone = direction;
        public void SetIsLoopingValue(bool direction) => _isLooping = direction;
        public void SetIsActiveValue(bool direction) => _isActive = direction;

        #endregion

        #region Public Get Functions

        // Get a bool status.
        public bool GetIsStartOnEnableStatus() => _isStartOnEnable;
        public bool GetIsDisableOnceDoneStatus() => _isDisableOnceDone;
        public bool GetIsLoopingStatus() => _isLooping;
        public bool GetIsActiveStatus() => _isActive;

        #endregion
    }
}