using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SC_PackageList))]
public class SC_PackageSpawner : MonoBehaviour
{
    [SerializeField] private Vector3 _startPosition;

    private SC_PackageList _packageList;
    private List<SC_Package> _packages;

    private int _currentPackage = 0;
    private GameObject _lastPackage;

    public Action OnSpawnedPackage;
    public Action OnDeSpawnedPackage;
    public Action OnFinishList;

    private void Awake()
    {
        _packageList = GetComponent<SC_PackageList>();
        _packages = _packageList.GetPackages();
    }

    ///REMOVE ONES THERE IS A GAME LOOP
    private void Start() => SpawnPackage();

    /// <summary>
    /// Tries to spawn the package and de-spawnes the older package.
    /// </summary>
    public void TrySpawnPackage()
    {
        if (_lastPackage != null) DeSpawnPackage();

        if (_packages.Count >= _currentPackage)
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
    }
}