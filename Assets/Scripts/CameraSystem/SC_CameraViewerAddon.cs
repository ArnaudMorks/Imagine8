
// [Summary] (By Wessel)
//
// This script is in charge of changing camera views it main purpose
// is to expand functionality of camera raycaster.
//

using UnityEngine;

namespace CameraSystem
{
    [RequireComponent(typeof(SC_CameraRaycaster))]
    public class SC_CameraViewerAddon : MonoBehaviour
    {
        // Changeable
        [SerializeField] private SC_CameraView _mainView; // Requires manual input!

        // Currents
        private SC_CameraView _currentView;
        private GameObject _currentInterfaceOverlay;

        // My camera raycaster
        private SC_CameraRaycaster _cameraRaycaster;


        // ----------------- Functions -----------------


        #region OnEnable Functions

        // Changes that happen on enable.
        private void OnEnable()
        {
            AssignToCameraManager();
            TryAssignCameraRaycaster();
            SubscribeOnHitViewTrigger();
            ReturnToMainView();

            // Expand..
        }

        // Assign this camera viewer addon to the camera manager.
        private void AssignToCameraManager() => SC_CameraManager.Instance.AssignSceneCameraViewerAddon(this);

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
            UnSubscribeOnHitViewTrigger();
            UnAssignFromCameraManager();

            // Expand..
        }

        // Assign this camera viewer addon to the camera manager.
        private void UnAssignFromCameraManager() => SC_CameraManager.Instance.AssignSceneCameraViewerAddon(this);

        #endregion

        #region Subscription Functions

        // On hit view trigger subscription.
        private void SubscribeOnHitViewTrigger() => _cameraRaycaster.OnHitViewTrigger += HitViewTriggerResult;
        private void UnSubscribeOnHitViewTrigger() => _cameraRaycaster.OnHitViewTrigger -= HitViewTriggerResult;

        // On hit view trigger result.
        private void HitViewTriggerResult(GameObject hitObject)
        {
            if (_currentView != null)
            {
                if (_currentView.GetViewType() != SC_CameraViewTypeEnum.MAIN)
                    return;

                _currentView = hitObject.GetComponent<SC_CameraView>();
                MoveToView(_currentView.GetPositionerObject().transform);
                SC_StampManager stampManager = FindFirstObjectByType<SC_StampManager>();
                ActivateInterfaceViewOverlay();
            }
        }

        #endregion

        #region Main Camera functions

        // Moves the main camera to a given transform.
        private void MoveToView(Transform cameraToThis)
        {
            Camera.main.transform.position = cameraToThis.position;
            Camera.main.transform.rotation = cameraToThis.rotation;
        }

        #endregion

        #region ViewInterface Functions

        // Activate the current interface view overlay.
        private void ActivateInterfaceViewOverlay()
        {
            if (_currentView != null)
                _currentInterfaceOverlay = _currentView.GetInterfaceObject();

            if (_currentInterfaceOverlay != null)
                _currentInterfaceOverlay.gameObject.SetActive(true);
        }

        // Deactivate the current interface view overlay.
        private void DeactivateInterfaceViewOverlay()
        {
            if (_currentInterfaceOverlay != null)
                _currentInterfaceOverlay.gameObject.SetActive(false);
        }

        #endregion

        #region Manager Command Functions

        // Manager commands you to go back to the main view.
        public void ReturnToMainView()
        {
            if (_mainView == null)
            {
                Debug.LogError("You need to assign a main view!");
                return;
            }

            DeactivateInterfaceViewOverlay();

            _currentView = _mainView;
            MoveToView(_mainView.GetPositionerObject().transform);
            ActivateInterfaceViewOverlay();
        }

        #endregion
    }
}