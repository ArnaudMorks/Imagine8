using UnityEngine;

public class SC_SendingManager : MonoBehaviour
{
    private SC_Package _currentPackage;
    private SC_PackageSpawner _packageSpawner;

    private void Awake() => _packageSpawner = FindAnyObjectByType<SC_PackageSpawner>();

    /// <summary>
    /// Tries to spawn a new packages and finish the old packages if possible.
    /// </summary>
    /// <param name="destination"> The Enum that dictates the destination of the package.</param>
    /// <return> Returns success of sending a package.</return>
    public bool TrySendPackage(PackageDestination destination)
    {
        if (_packageSpawner == null) return false;

        _currentPackage = _packageSpawner.GetCurrentPackage();

        if (_currentPackage == null) return false;

        SendPackage(destination);
        return true;
    }

    private void SendPackage(PackageDestination destination)
    {
        _currentPackage.SetDestination(destination);
        _currentPackage.OnDestinationSet?.Invoke();

        bool hasNewPackage = _packageSpawner.TrySpawnPackage();

        if (!hasNewPackage)
        {
            //FUNCTIONALITY FOR ENDING STATE.
        }
    }
}