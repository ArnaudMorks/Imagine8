using UnityEngine;

public class SC_InputSoundEffects : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;
    private AudioSource _audioSource;

    private SC_InputHandler _inputHandler;

    private void OnEnable()
    {
        TryToAssignInputHandler();
        TryToAssignAdioSource();
    }

    private void PlayAudio()
    {
        _audioSource.Play();
    }

    private void TryToAssignInputHandler()
    {
        if (SC_InputHandler.Instance == null)
        {
            Debug.LogWarning("InputHandler instance is missing!");
            return;
        }

        _inputHandler = SC_InputHandler.Instance;
    }

    private void TryToAssignAdioSource()
    {
        if (!TryGetComponent<AudioSource>(out _audioSource))
        {
            Debug.LogError("Has no AudioSource Component!");
        }
    }

    private void SubscribeToClickInput() => _inputHandler.OnEscapeKey += PlayAudio;
    private void UnSubscribeToClickInput() => _inputHandler.OnEscapeKey -= PlayAudio;
}
