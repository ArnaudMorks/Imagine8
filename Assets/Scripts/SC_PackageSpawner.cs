using System;
using UnityEngine;

public class SC_PackageSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _startPosition;
    [SerializeField] private GameObject _packageList;
    private SC_Package[] _packages;

    private int _currentPackage = 0;
    private GameObject _lastPackage;

    public Action OnSpawnedPackage;
    public Action OnDeSpawnedPackage;
    public Action OnFinishList;

    private void Awake() => _packages = _packageList.GetComponentsInChildren<SC_Package>();

    //REMOVE WHEN THERE IS A GAME LOOP
    public void Start() => TrySpawnPackage();

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

        _lastPackage = Instantiate(_packages[_currentPackage].gameObject,
            _startPosition.transform.position, Quaternion.identity);

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