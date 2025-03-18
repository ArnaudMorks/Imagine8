using System;
using UnityEngine;

public class SC_PackageSpawner : MonoBehaviour
{
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private GameObject _packageList;
    private SC_Package[] _packages;

    private int _currentPackage = 0;
    private GameObject _lastPackage;

    public Action OnSpawnedPackage;
    public Action OnDeSpawnedPackage;
    public Action OnFinishList;

    private void Awake() => _packages = _packageList.GetComponentsInChildren<SC_Package>();

    /// <summary>
    /// Tries to spawn the package and de-spawnes the older package.
    /// </summary>
    public void TrySpawnPackage()
    {
        if (_lastPackage != null) DeSpawnPackage();

        if (_packages.Length <= _currentPackage)
        {
            StopSpawning();
            return;
        }

        SpawnPackage();
    }

    private void SpawnPackage()
    {
        OnSpawnedPackage?.Invoke();

        _lastPackage = Instantiate(_packages[_currentPackage].gameObject);
        _currentPackage++;
    }

    private void DeSpawnPackage()
    {
        OnDeSpawnedPackage?.Invoke();

        Destroy(_lastPackage);
    }

    private void StopSpawning()
    {
        OnFinishList?.Invoke();
        Debug.LogError("SPAWNER: No more Packages");
    }
}