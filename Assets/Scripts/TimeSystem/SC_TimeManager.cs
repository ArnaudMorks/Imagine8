
// [Summary] (By Wessel)
//
// This script is in charge of keeping track of time and updating it as it progresses,
// serving as a nice central basis for other scripts to operate on.
//

using System;
using UnityEngine;

namespace TimeSystem
{
    public class SC_TimeManager : MonoBehaviour
    {
        // Changeable
        [Header("Update Variables")]
        [SerializeField, Tooltip("The multiplication of time every second.")] 
        private float _timeMultiplier = 1;
        [SerializeField, Tooltip("Is the time allowed to update?")]
        private bool _timeIsRunning = false;

        // Debugging
        [Header("Debugging Variables")]
        [SerializeField, Tooltip("Do you want to show in game time?")]
        private bool _consoleShowInGameTime = false;
        [SerializeField, Tooltip("Do you want to show game session time?")]
        private bool _consoleShowGameSessionTime = false;
        [SerializeField, Tooltip("Do you want to show level session time?")]
        private bool _consoleShowLevelSessionTime = false;
        [SerializeField, Tooltip("Do you want to show actions?")]
        private bool _consoleShowActions = false;

        // Singleton reference
        public static SC_TimeManager Instance { get; private set; }

        // Current Time
        private float _currentInGameTime;

        // Timed-Events
        private float _nextSecondMark = 0f;
        private float _nextMinuteMark = 0f;
        private float _nextHourMark = 0f;
        private float _nextDayMark = 0f;

        // Timed-Events
        private bool _skippedFirstSecondMark = false;
        private bool _skippedFirstMinuteMark = false;
        private bool _skippedFirstHourMark = false;
        private bool _skippedFirstDayMark = false;

        // Actions
        public Action OnSecondPassed;
        public Action OnMinutePassed;
        public Action OnHourPassed;
        public Action OnDayPassed;

        // Time Rules
        private const int SECONDS_PER_MINUTE = 60; // The amount of seconds per minute.
        private const int MINUTES_PER_HOUR = 60; // The amount of minutes per hour.
        private const int HOURS_PER_DAY = 24; // The amount of hours per day.


        // ----------------- Functions -----------------


        #region Debugging Functions

        // Is for showing time in console.
        private void DebuggingChecks()
        {
            if (_consoleShowInGameTime)
                Debug.Log("In-Game Time is: " + GetTimeToStringHMS(_currentInGameTime));

            if (_consoleShowGameSessionTime)
                Debug.Log("Game Session Time is: " + GetTimeToStringHMS(Time.realtimeSinceStartup));

            if (_consoleShowLevelSessionTime)
                Debug.Log("Level Session Time is: " + GetTimeToStringHMS(Time.timeSinceLevelLoad));
        }

        #endregion

        #region Awake Functions

        // Absolute first thing this script does.
        private void Awake()
        {
            Singleton();

            // Expand..
        }

        // Sets up singleton logic.
        private void Singleton()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);  // Persist across scenes.
            }
            else
            {
                Destroy(gameObject);  // Destroy duplicate instances.
            }
        }

        #endregion

        #region Update Functions

        // Updates when allowed to.
        private void Update()
        {
            if (!_timeIsRunning)
                return;

            UpdateInGameTime();

            UpdateTimeEvents();

            DebuggingChecks();

            // Expand..
        }

        // Time update logic.
        private void UpdateInGameTime() => _currentInGameTime += Time.deltaTime * _timeMultiplier;

        // Checks the time event actions.
        private void UpdateTimeEvents()
        {
            if (_currentInGameTime >= _nextSecondMark)
            {
                if (OnSecondPassed != null)
                {
                    if (_skippedFirstSecondMark == false)
                    {
                        _skippedFirstSecondMark = true;
                        return;
                    }

                    if (_consoleShowActions)
                        Debug.Log($"[DEBUG] Triggered: SECOND | Action: {OnSecondPassed.Method.Name}");
                    OnSecondPassed.Invoke();
                }
                _nextSecondMark += 1f;
            }

            if (_currentInGameTime >= _nextMinuteMark)
            {
                if (OnMinutePassed != null && _nextMinuteMark > 0)
                {
                    if (_skippedFirstMinuteMark == false)
                    {
                        _skippedFirstMinuteMark = true;
                        return;
                    }

                    if (_consoleShowActions)
                        Debug.Log($"[DEBUG] Triggered: MINUTE | Action: {OnMinutePassed.Method.Name}");
                    OnMinutePassed.Invoke();
                }
                _nextMinuteMark += SECONDS_PER_MINUTE;
            }

            if (_currentInGameTime >= _nextHourMark)
            {
                if (OnHourPassed != null && _nextHourMark > 0)
                {
                    if (_skippedFirstHourMark == false)
                    {
                        _skippedFirstHourMark = true;
                        return;
                    }

                    if (_consoleShowActions)
                        Debug.Log($"[DEBUG] Triggered: HOUR | Action: {OnHourPassed.Method.Name}");
                    OnHourPassed.Invoke();
                }
                _nextHourMark += SECONDS_PER_MINUTE * SECONDS_PER_MINUTE;
            }

            if (_currentInGameTime >= _nextDayMark)
            {
                if (OnDayPassed != null && _nextDayMark > 0)
                {
                    if (_skippedFirstDayMark == false)
                    {
                        _skippedFirstDayMark = true;
                        return;
                    }

                    if (_consoleShowActions)
                        Debug.Log($"[DEBUG] Triggered: DAY | Action: {OnDayPassed.Method.Name}");
                    OnDayPassed.Invoke();
                }
                _nextDayMark += SECONDS_PER_MINUTE * SECONDS_PER_MINUTE * HOURS_PER_DAY;
            }
        }

        #endregion

        #region Calculate Time-Based Variable Functions

        // Convert current time into seconds based on the time rules.
        private int CalculateSeconds(float time)
        {
            return Mathf.FloorToInt(time % SECONDS_PER_MINUTE);
        }

        // Convert current time into minutes based on the time rules.
        private int CalculateMinutes(float time)
        {
            return Mathf.FloorToInt(time / SECONDS_PER_MINUTE % MINUTES_PER_HOUR);
        }

        // Convert current time into hours based on the time rules.
        private int CalculateHours(float time)
        {
            return Mathf.FloorToInt(time / SECONDS_PER_MINUTE / SECONDS_PER_MINUTE % HOURS_PER_DAY);
        }

        // Convert current time into days based on the time rules.
        private int CalculateDays(float time)
        {
            return Mathf.FloorToInt(time / SECONDS_PER_MINUTE / SECONDS_PER_MINUTE / HOURS_PER_DAY);
        }

        #endregion

        #region Reset Functions

        // Changes Current Time Variables.
        private void ChamgeCurrentInGameTime(float newTime) => _currentInGameTime = newTime;
        private void ChangeCurrentSecondMark(float newTime) => _nextSecondMark = newTime;
        private void ChangeCurrentMinuteMark(float newTime) => _nextMinuteMark = newTime;
        private void ChangeCurrentHourMark(float newTime) => _nextHourMark = newTime;
        private void ChangeCurrentDayMark(float newTime) => _nextDayMark = newTime;

        #endregion

        #region Public Set Functions

        // Change if you want to run time.
        public void RunningIngameTime(bool direction) => _timeIsRunning = direction;

        // Changes the current time to a given time.
        public void ChangeInGameTime(float newTime)
        {
            ChamgeCurrentInGameTime(newTime);
            ChangeCurrentSecondMark(newTime);
            ChangeCurrentMinuteMark(newTime);
            ChangeCurrentHourMark(newTime);
            ChangeCurrentDayMark(newTime);

            _skippedFirstSecondMark = false;
            _skippedFirstMinuteMark = false;
            _skippedFirstHourMark = false;
            _skippedFirstDayMark = false;
        }

        // Completely resets the in game time.
        public void ResetInGameTime()
        {
            ChamgeCurrentInGameTime(0);
            ChangeCurrentSecondMark(0);
            ChangeCurrentMinuteMark(0);
            ChangeCurrentHourMark(0);
            ChangeCurrentDayMark(0);
        }

        #endregion

        #region Public Get Functions

        // Get Time-Related Variables.
        public float GetCurrentTime => _currentInGameTime;
        public float GetSecondsPerMinuteRule => SECONDS_PER_MINUTE; 
        public float GetMinutesPerHourRule => MINUTES_PER_HOUR; 
        public float GetHoursPerDayRule => HOURS_PER_DAY; 

        // Get the current time in form of a HMS string.
        public string GetTimeToStringHMS(float time)
        {
            string hours = CalculateHours(time).ToString("00");
            string minutes = CalculateMinutes(time).ToString("00");
            string seconds = CalculateSeconds(time).ToString("00");

            return "[" + hours + ":" + minutes + ":" + seconds + "]";
        }

        // Get the current time in form of a HM string.
        public string GetTimeToStringHM(float time)
        {
            string hours = CalculateHours(time).ToString("00");
            string minutes = CalculateMinutes(time).ToString("00");

            return "[" + hours + ":" + minutes + "]";
        }

        #endregion

    }
}