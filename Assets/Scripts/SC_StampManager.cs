using CameraSystem;
using UnityEngine;

//LOT OF REPEATING CODE; IMPROVE LATER
public class SC_StampManager : MonoBehaviour
{
    //Takes all stamp pickup hitboxes from high to low
    [SerializeField] private SC_InteractStampPickup[] g_interactStampPickups;

    [SerializeField] private SC_StampStateEnum g_stampState;
    [SerializeField] private PackageStampIcon g_iconPackageStamp;
    [SerializeField] private PackageStampColor g_colorStampEnum;

    [SerializeField] private SC_MovingStamp g_movingStampScript;
    [SerializeField] private SC_VisualStamp g_visualStampScript;
    [SerializeField] private SC_VisualHolderStamps g_visualHolderScript;

    [SerializeField] private SC_VisualPackageStampSetter g_visualPackageStampSetter;
    [SerializeField] private SC_Package g_latestPackage;

    [Header("Timers")]
    [SerializeField] private float g_timeInkStamping;
    [SerializeField] private float g_timeOnPackageStamping;

    [Header("Parcel View")]
    [SerializeField] private SC_CameraView g_parcelView;

    [Header("Main View")]
    [SerializeField] private SC_CameraView g_mainView;

    [Header("Audio")]
    [SerializeField] private AudioSource g_audioSource;

    //Specific function to invoke because of timer
    private void SetToHasInk()
    {
        g_stampState = SC_StampStateEnum.HAS_INK;
        SetMoveStampState(g_stampState);
        g_visualStampScript.SetCurrentAnimationStamp(0);

        SC_CameraManager.Instance.VieverToNewView(g_parcelView);
    }

    private void SetOnPackageStamp()
    {
        //Check in case lever is pulled to soon
        if (g_latestPackage != null)
        {
            //Adds color and icon to the package
            g_latestPackage.AddStamp(g_colorStampEnum, g_iconPackageStamp);

            g_visualPackageStampSetter.MakeVisualIconOnPackage((int)g_iconPackageStamp,
                (int)g_colorStampEnum, g_movingStampScript.gameObject.transform.position,
                g_latestPackage.gameObject.transform.position);

        }

        SC_CameraManager.Instance.VieverToNewView(g_mainView);

        TryFinishStamp();
    }

    private void TryFinishStamp()
    {
        if (g_stampState != SC_StampStateEnum.NOT_HOLDING)
        {
            //stamp on package FOR LATER
            g_stampState = SC_StampStateEnum.NOT_HOLDING;

            SetMoveStampState(g_stampState);

            //Enables and disables correct visuals
            g_visualHolderScript.EnableStampOnHolder();
            g_visualStampScript.DisableStampVisual();
        }
    }


    private void SetMoveStampState(SC_StampStateEnum stampState)
    {
        g_movingStampScript.MovingStampState = stampState;
        g_movingStampScript.TrySnapToPosition();
    }


    //Tries to grab a stamp, and put's a stamp back when already holding one (when possible)
    public void TryGettingSymbol(PackageStampIcon iconStampState, bool canPickupStamp)
    {
        if (g_stampState == SC_StampStateEnum.NOT_HOLDING && canPickupStamp)
        {
            g_iconPackageStamp = iconStampState;
            g_stampState = SC_StampStateEnum.OVER_INK_MOVE;

            SetMoveStampState(g_stampState);
            //g_visualStampScript.SetCurrentAnimationStamp(g_stampState, g_colorStampEnum);

            //Disables and enables correct visuals
            g_visualHolderScript.DisableStampOnHolder(iconStampState);
            g_visualStampScript.EnableStampVisual(iconStampState);
        }
        else if (g_stampState != SC_StampStateEnum.GETTING_INK
            && g_stampState != SC_StampStateEnum.STAMP_ON_PACKAGE)
        {
            //put stamp back if holding one
            TryFinishStamp();
        }
    }

    //triggers when clicking on an ink color while holding the stamp over said color
    public void TryGettingSpecificColor(PackageStampColor stampColorEnum)
    {
        if (g_stampState == SC_StampStateEnum.OVER_INK_MOVE)
        {
            if (g_audioSource != null)
                g_audioSource.Play();

            g_colorStampEnum = stampColorEnum;
            g_stampState = SC_StampStateEnum.GETTING_INK;

            SetMoveStampState(g_stampState);
            g_movingStampScript.TrySetCurrentColorPosition(stampColorEnum);
            g_visualStampScript.SetCurrentInk(stampColorEnum);
            Invoke("SetToHasInk", g_timeInkStamping);
        }
    }

    //triggers when clicking on a package while there is ink on the stamp
    public void TryOnPackageStamp(SC_Package currentPackage)
    {
        if (g_stampState == SC_StampStateEnum.HAS_INK)
        {
            if (g_audioSource != null)
                g_audioSource.Play();

            g_stampState = SC_StampStateEnum.STAMP_ON_PACKAGE;
            g_latestPackage = currentPackage;
            g_visualStampScript.SetCurrentAnimationStamp(1);
            g_movingStampScript.MovingStampState = g_stampState;
            Invoke("SetOnPackageStamp", g_timeOnPackageStamping);
        }
    }


}
