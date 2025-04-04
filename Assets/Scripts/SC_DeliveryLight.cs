using System.Collections;
using UnityEngine;

public class DeliveryLight : MonoBehaviour
{
    [SerializeField] private float _LightDuration = 1;

    [SerializeField] private Material _correctMaterial;
    [SerializeField] private Material _incorrectMaterial;
    private Material _defaultMaterial;
    private MeshRenderer _meshRenderer;

    private SC_SendingManager _sendingManager;


    private void Awake()
    {
        _sendingManager = FindFirstObjectByType<SC_SendingManager>();
        _sendingManager.OnPackageSend += TrySetLightMaterial;

        SetupMeshRenderer();
    }

    private void TrySetLightMaterial(SC_Package package) => StartCoroutine(SetLightMaterial(package, _LightDuration));
    private IEnumerator SetLightMaterial(SC_Package package, float amountTime)
    {
        if (package.CheckFlags())
            _meshRenderer.material = _correctMaterial;
        else
            _meshRenderer.material = _incorrectMaterial;

        yield return new WaitForSeconds(amountTime);
        _meshRenderer.material = _defaultMaterial;
    }

    private void SetupMeshRenderer()
    {
        _meshRenderer = TryGetMeshRenderer();

        if (_meshRenderer != null)
            _defaultMaterial = GetDefautlMaterial(_meshRenderer);
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

    private Material GetDefautlMaterial(MeshRenderer meshRenderer) => meshRenderer.material;
}
