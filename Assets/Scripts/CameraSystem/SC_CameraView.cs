
// [Summary] (By Wessel)
//
// This script is in charge of holding camera view data,
// that way other scripts can get that information easily.
//

using UnityEngine;

namespace CameraSystem
{
    public class SC_CameraView : MonoBehaviour
    {
        [SerializeField] private SC_CameraViewTypeEnum _viewType;
        [SerializeField] private GameObject _interfaceObject;
        [SerializeField] private GameObject _positionerObject;
        [SerializeField] private GameObject _triggerObject;


        // ----------------- Functions -----------------


        #region OnEnable Functions

        // Changes that happen on enable.
        private void OnEnable()
        {
            ChildrenSafetyCheck();
            TryAssignPositionerObject();
            TryAssignTriggerObject();

            // Expand..
        }

        // Safety check to see if it has enough children.
        private void ChildrenSafetyCheck()
        {
            if (transform.childCount < 2)
            {
                Debug.LogError("CameraView Parent is missing a Positioner Child and/or Trigger Child!");
                return;
            }
        }

        // Tries to assign the trigger object as the first child.
        private void TryAssignPositionerObject()
        {
            if (_positionerObject != null)
                return;

            if (transform.GetChild(0).gameObject.name != "Positioner")
            {
                Debug.LogError("The first child isn't called Positioner, it should!");
                return;
            }

            _positionerObject = transform.GetChild(0).gameObject;
        }

        // Tries to assign the trigger object as the second child.
        private void TryAssignTriggerObject()
        {
            if (_triggerObject != null)
                return;

            if (transform.GetChild(1).gameObject.name != "Trigger")
            {
                Debug.LogError("The second child isn't called Trigger, it should!");
                return;
            }

            _triggerObject = transform.GetChild(1).gameObject;
        }

        #endregion

        #region Get Functions

        // Allows other script to get information about this view.
        public SC_CameraViewTypeEnum GetViewType() => _viewType;
        public GameObject GetInterfaceObject() => _interfaceObject;
        public GameObject GetPositionerObject() => _positionerObject;
        public GameObject GetTriggerObject() => _triggerObject;

        #endregion

    }
}