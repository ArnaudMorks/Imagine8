using UnityEngine;

public class SC_DestinationLever : MonoBehaviour
{
    [SerializeField] private PackageDestination _packageDestination;
    private SC_SendingManager _sendingManager;

    private void Awake() => _sendingManager = FindFirstObjectByType<SC_SendingManager>();

    /// <summary>
    /// Tries to set the package destination if there is a package in the Scene.
    /// </summary>
    public void TrySetDestination()
    {
        if (_sendingManager == null) return;

        _sendingManager.TrySendPackage(_packageDestination);
    }
}
