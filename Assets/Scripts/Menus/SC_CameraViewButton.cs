using CameraSystem;
using UnityEngine;
using UnityEngine.UI;

public class SC_CameraViewButton : MonoBehaviour
{
    private SC_CameraManager _cameraManager;
    private Button _button;

    private void Start()
    {
        TryToAssignCameraManager();
        TryToAssignButton();

        SubscribeOnClick();
    }

    private void TryToAssignCameraManager()
    {
        if (_cameraManager != null) return;

        if (SC_CameraManager.Instance == null)
        {
            Debug.LogWarning("SC_CameraManager instance is missing!");
            return;
        }

        _cameraManager = SC_CameraManager.Instance;
    }

    private void TryToAssignButton()
    {
        if (_button != null) return;

        if (!this.TryGetComponent<Button>(out _button))
        {
            Debug.LogWarning("Button instance is missing!");
            return;
        }
    }

    private void SubscribeOnClick() => _button.onClick.AddListener(AssignAction);
    private void UnsubscribeOnClick() => _button.onClick.RemoveListener(AssignAction);

    private void AssignAction() => _cameraManager.ViewerToMainView();
}
