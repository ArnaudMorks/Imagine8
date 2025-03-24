
// [Summary] (By Wessel)
//
// This script is in charge of creating raycast and turning that
// into actions that can easily be used by other scripts.
//

using System;
using UnityEngine;

namespace CameraSystem
{
    public class SC_CameraRaycaster : MonoBehaviour
    {
        // Changeable
        [SerializeField] private LayerMask _viewTriggersLayerMask;
        [SerializeField] private LayerMask _itemTriggersLayerMask;
        [SerializeField] private float _rayMaxDistance = 100f;
        [SerializeField] private bool _pointerIsOnInterface;
        
        // Actions
        public Action<GameObject> OnHitViewTrigger;
        public Action<GameObject> OnHitItemTrigger;

        // Input handler
        private SC_InputHandler _inputHandler;

        // Event handler
        private SC_TemporaryInterfaceEventHandler _interfaceEventHandler;


        // ----------------- Functions -----------------


        #region OnEnable Functions

        // Changes that happen on enable.
        private void OnEnable()
        {
            TryToAssignInputHandler();
            TryToAssignInterfaceEventHandler();
            AssignToCameraManager();
            SubscribeOnPointerOverInterface();
            SubscribeOnLeftMouseClick();

            // Expand..
        }

        // Tries to get the input handler singleton.
        private void TryToAssignInputHandler()
        {
            if (SC_InputHandler.Instance == null)
            {
                Debug.LogWarning("InputHandler instance is missing!");
                return;
            }

            _inputHandler = SC_InputHandler.Instance;
        }

        // Tries to get the interface event handler instance.
        private void TryToAssignInterfaceEventHandler()
        {
            if (SC_TemporaryInterfaceEventHandler.Instance == null)
            {
                Debug.LogWarning("InterfaceEventHandler instance is missing!");
                return;
            }

            _interfaceEventHandler = SC_TemporaryInterfaceEventHandler.Instance;
        }

        // Assign this camera raycaster to the camera manager.
        private void AssignToCameraManager() => SC_CameraManager.Instance.AssignSceneCameraRaycaster(this);

        #endregion

        #region OnDisable Functions

        // Changes that happen on disable.
        private void OnDisable()
        {
            UnAssignFromCameraManager();
            UnSubscribeOnLeftMouseClick();
            UnSubscribeOnPointerOverInterface();

            // Expand..
        }

        // Assign this camera raycaster to the camera manager.
        private void UnAssignFromCameraManager() => SC_CameraManager.Instance.AssignSceneCameraRaycaster(this);

        #endregion

        #region Subscription Functions

        // On pointer over interface subscription.
        private void SubscribeOnPointerOverInterface() => _interfaceEventHandler.OnPointerOverUI += OnPointerOverInterfaceResult;
        private void UnSubscribeOnPointerOverInterface() => _interfaceEventHandler.OnPointerOverUI -= OnPointerOverInterfaceResult;
        private void OnPointerOverInterfaceResult(bool direction) => _pointerIsOnInterface = direction;

        // On left mouse click subscription.
        private void SubscribeOnLeftMouseClick() => _inputHandler.OnLeftMouseClick += OnLeftMouseClickResult;
        private void UnSubscribeOnLeftMouseClick() => _inputHandler.OnLeftMouseClick -= OnLeftMouseClickResult;
        private void OnLeftMouseClickResult(Vector2 position)
        {
            if (_pointerIsOnInterface == true)
                return;

            Ray ray = Camera.main.ScreenPointToRay(position);
            if (Physics.Raycast(ray, out RaycastHit hitTwo, _rayMaxDistance, _itemTriggersLayerMask))
            {
                if (hitTwo.collider.isTrigger)
                {
                    OnHitItemTrigger?.Invoke(hitTwo.rigidbody.gameObject);
                    return; // Don't go further if hit item!
                }
            }

            if (Physics.Raycast(ray, out RaycastHit hit, _rayMaxDistance, _viewTriggersLayerMask))
            {
                if (hit.collider.isTrigger)
                    OnHitViewTrigger?.Invoke(hit.rigidbody.gameObject);
            }
        }

        #endregion

    }
}