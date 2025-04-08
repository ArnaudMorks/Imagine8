using System.Collections;
using UnityEngine;

public class DeliveryLight : MonoBehaviour
{
    [SerializeField] private float _LightDuration = 1;

    [Header("Materials")]
    [SerializeField] private Material _correctMaterial;
    [SerializeField] private Material _incorrectMaterial;

    [Header("Sounds")]
    [SerializeField] private AudioClip _correctClip;
    [SerializeField] private AudioClip _incorrectClip;

    private Material _defaultMaterial;
    private MeshRenderer _meshRenderer;
    private AudioSource _audioSource;

    private SC_SendingManager _sendingManager;


    private void Awake()
    {
        _sendingManager = FindFirstObjectByType<SC_SendingManager>();
        _sendingManager.OnPackageSend += TrySetLightMaterial;

        SetupMeshRenderer();
        _audioSource = TryGetAudioSource();
    }

    private void TrySetLightMaterial(SC_Package package) => StartCoroutine(SetLightMaterial(package, _LightDuration));
    private IEnumerator SetLightMaterial(SC_Package package, float amountTime)
    {
        if (package.CheckFlags())
        {
            _meshRenderer.material = _correctMaterial;
            PlayAudioSource(_correctClip);
        }
        else
        {
            _meshRenderer.material = _incorrectMaterial;
            PlayAudioSource(_incorrectClip);
        }

        yield return new WaitForSeconds(amountTime);
        _meshRenderer.material = _defaultMaterial;
    }

    private void SetupMeshRenderer()
    {
        _meshRenderer = TryGetMeshRenderer();

        if (_meshRenderer != null)
            _defaultMaterial = GetDefautlMaterial(_meshRenderer);
    }

    private void PlayAudioSource(AudioClip clip)
    {
        if (_audioSource != null)
        {
            _audioSource.clip = clip;
            _audioSource.Play();
        }
    }

    private MeshRenderer TryGetMeshRenderer()
    {
        if (this.transform.TryGetComponent(out _meshRenderer) != true)
        {
            Debug.LogError("The object doesn't have a mesh renderer, it should!");
            return null;
        }

        return _meshRenderer;
    }

    private AudioSource TryGetAudioSource()
    {
        if (this.transform.TryGetComponent(out _audioSource) != true)
        {
            Debug.LogError("The object doesn't have a audio source, it should!");
            return null;
        }

        return _audioSource;
    }

    private Material GetDefautlMaterial(MeshRenderer meshRenderer) => meshRenderer.material;
}
