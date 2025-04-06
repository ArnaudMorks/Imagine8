using CameraSystem;
using UnityEngine;

public class SC_DestinationLever : MonoBehaviour
{
    [SerializeField] private PackageDestination _packageDestination;
    [SerializeField] private Animator _animator;

    private SC_TemporaryItemInteractReceiver _itemInteractReceiver;
    private SC_SendingManager _sendingManager;


    private void Awake()
    {
        _sendingManager = FindFirstObjectByType<SC_SendingManager>();

        TryAssignItemInteractReceiver();
        _itemInteractReceiver.OnReceivedHit += TrySetDestination;
    }

    /// <summary>
    /// Tries to set the package destination if there is a package in the Scene.
    /// </summary>
    public void TrySetDestination()
    {

        if (_sendingManager == null) return;

        _sendingManager.TrySendPackage(_packageDestination);
        SC_CameraManager.Instance.ViewerToMainView();
        _animator.Play("LeverPullanim");
    }

    private void TryAssignItemInteractReceiver()
    {
        if (this.TryGetComponent(out SC_TemporaryItemInteractReceiver itemInteractReceiver))
            _itemInteractReceiver = itemInteractReceiver;
        else
            Debug.LogWarning("Unable to get my _itemInteractReceiver component!");
    }
}
