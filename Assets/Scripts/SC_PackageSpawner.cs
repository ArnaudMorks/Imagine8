using System;
using System.Linq;
using UnityEngine;

public class SC_PackageSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _startPosition;
    [SerializeField] private GameObject _packageList;
    private SC_Package[] _packages;

    private int _currentPackage = 0;
    private GameObject _lastPackage;

    public Action OnSpawnedPackage;
    public Action<GameObject> OnDeSpawnedPackage;
    public Action OnFinishList;

    private void Awake() => _packages = _packageList.GetComponentsInChildren<SC_Package>();

    //REMOVE WHEN THERE IS A GAME LOOP
    public void Start() => TrySpawnPackage();

    /// <summary>
    /// Tries to spawn the package and de-spawnes the older package.
    /// </summary>
    /// <returns> Returns success of spawning the package.</returns>
    public bool TrySpawnPackage()
    {
        if (_lastPackage != null) DeSpawnPackage();

        if (_packages.Length <= _currentPackage)
        {
            StopSpawning();
            return false;
        }

        SpawnPackage();
        return true;
    }

    /// <summary>
    /// Gets the current package that is used inside of the game
    /// </summary>
    /// <returns> Reference to current package script.</returns>
    public SC_Package GetCurrentPackage()
    {
        if (_packages.Length <= _currentPackage) return _packages.Last();
        return _packages[_currentPackage];
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
        OnDeSpawnedPackage?.Invoke(_lastPackage);

        Destroy(_lastPackage);
    }

    private void StopSpawning()
    {
        OnFinishList?.Invoke();
        Debug.LogWarning("SPAWNER: No more Packages");
    }
}