using UnityEngine;

namespace TimeSystem
{
    [RequireComponent(typeof(SC_TimeEvent))]
    public class SC_SoundQueue : MonoBehaviour
    {
        [SerializeField] private AudioSource _Source;
        private float lastTimeFrame;

        private SC_TimeManager _manager;

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
                this.gameObject.GetComponent<SC_TimeEvent>().OnTimeEventComplete += PlayAudio;
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

        // Time event result logic.
        private void PlayAudio()
        {
            if (_manager.GetCurrentTime == lastTimeFrame) return;
            lastTimeFrame = _manager.GetCurrentTime;

            _Source.Play();
        }

        // Tell the time manager to stop or run time.
        private void PauseTime() => _manager.RunningIngameTime(false);
        public void ResumeTime() => _manager.RunningIngameTime(true);
    }
}

