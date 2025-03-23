
// [Summary] (By Wessel)
//
// This script is in charge of managing the camera system,
// it's the center of the camera system holding the ropes.
//

using UnityEngine;

namespace CameraSystem
{
    public class SC_CameraManager : MonoBehaviour
    {
        // Sub-systems
        private SC_CameraRaycaster _cameraRaycaster;
        private SC_CameraViewerAddon _cameraViewerAddon;

        // Singleton reference
        public static SC_CameraManager Instance { get; private set; }


        // ----------------- Functions -----------------


        #region Awake Functions

        // Absolute first thing this script does.
        void Awake()
        {
            Singleton();

            //Expand..
        }

        // Sets up singleton logic.
        private void Singleton()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);  // Persist across scenes.
            }
            else
            {
                Destroy(gameObject);  // Destroy duplicate instances.
            }
        }

        #endregion

        #region CameraRaycaster Functions

        // Allows the camera raycaster to assign himself, if active.
        public void AssignSceneCameraRaycaster(SC_CameraRaycaster camRaycaster)
        {
            if (_cameraRaycaster == null)
                _cameraRaycaster = camRaycaster;
            else
                Debug.LogWarning("Already have a _cameraRaycaster Assigned! Duplicates?");
        }

        // Un assigns the camera raycaster.
        public void UnAssignSceneCameraRaycaster(SC_CameraRaycaster camRaycaster)
        {
            if (_cameraRaycaster == camRaycaster)
                _cameraRaycaster = null;
            else
                Debug.LogWarning("I have a different _cameraRaycaster Assigned! Duplicates?");
        }

        #endregion

        #region CameraViewerAddon Functions

        // Allows the camera viewer addon to assign himself, if active.
        public void AssignSceneCameraViewerAddon(SC_CameraViewerAddon camViewerAddon)
        {
            if (_cameraViewerAddon == null)
                _cameraViewerAddon = camViewerAddon;
            else
                Debug.LogWarning("Already have a _cameraViewerAddon Assigned! Duplicates?");
        }

        // Un assigns the camera viewer addon.
        public void UnAssignSceneCameraViewerAddon(SC_CameraViewerAddon camViewerAddon)
        {
            if (_cameraViewerAddon == camViewerAddon)
                _cameraViewerAddon = null;
            else
                Debug.LogWarning("I have a different _cameraViewerAddon Assigned! Duplicates?");
        }

        #endregion

        #region Gateway Functions

        // Allows other higher-ups to influence this manager.
        public void ViewerToMainView() => _cameraViewerAddon?.ReturnToMainView();

        #endregion

    }
}
