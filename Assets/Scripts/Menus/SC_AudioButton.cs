using System.Collections.Generic;
using UnityEngine;

public class SC_AudioButton : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Sprite _activeSprite;
    [SerializeField] private Sprite _deactiveSprite;

    [SerializeField] private List<AudioSource> _audioSources;

    public void ToggleAudio()
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            if (audioSource == null) continue;

            SetSprite(audioSource.mute);
            audioSource.mute = !audioSource.mute;
        }
    }

    private void SetSprite(bool audioState)
    {
        if (audioState)
            _renderer.sprite = _activeSprite;
        else
            _renderer.sprite = _deactiveSprite;
    }
}
