
// [Summary] (By Wessel)
//
// This script is in charge of interacting with items,
// it is to expand functionality of camera raycaster.
//

using UnityEngine;

namespace CameraSystem
{
    [RequireComponent(typeof(SC_CameraRaycaster))]
    public class SC_CameraItemInteractAddon : MonoBehaviour
    {
        // My camera raycaster
        private SC_CameraRaycaster _cameraRaycaster;


        // ----------------- Functions -----------------


        #region OnEnable Functions

        // Changes that happen on enable.
        private void OnEnable()
        {
            AssignToCameraManager();
            TryAssignCameraRaycaster();
            SubscribeOnHitItemTrigger();

            // Expand..
        }

        // Assign this camera item interact addon to the camera manager.
        private void AssignToCameraManager() => SC_CameraManager.Instance.AssignSceneCameraItemInteractAddon(this);

        // Auto-assign the camera raycaster.
        private void TryAssignCameraRaycaster()
        {
            if (this.TryGetComponent(out SC_CameraRaycaster camRaycaster))
                _cameraRaycaster = camRaycaster;
            else
                Debug.LogError("Unable to get my _cameraRaycaster component!");
        }

        #endregion

        #region OnDisable Functions

        // Changes that happen on disable.
        private void OnDisable()
        {
            UnSubscribeOnHitItemTrigger();
            UnAssignFromCameraManager();

            // Expand..
        }

        // Assign this camera item interact addon to the camera manager.
        private void UnAssignFromCameraManager() => SC_CameraManager.Instance.AssignSceneCameraItemInteractAddon(this);

        #endregion

        #region Subscription Functions

        // On hit item trigger subscription.
        private void SubscribeOnHitItemTrigger() => _cameraRaycaster.OnHitItemTrigger += HitItemTriggerResult;
        private void UnSubscribeOnHitItemTrigger() => _cameraRaycaster.OnHitItemTrigger -= HitItemTriggerResult;

        // On hit item trigger result.
        private void HitItemTriggerResult(GameObject hitObject)
        {
            if (hitObject.TryGetComponent(out SC_TemporaryItemInteractReceiver hitReceiver))
                hitReceiver.ReceiveItemInteractHit(); // Send signal to item to let it know it has been hit!
            else
                Debug.LogWarning("Hit a item without a SC_TemporaryItemInteractReceiver component, can't send signal");
        }

        #endregion
    }
}