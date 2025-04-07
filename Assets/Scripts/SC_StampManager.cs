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

	[SerializeField] private float g_gettingColorTime;

	[Header("Parcel View")]
	[SerializeField] private SC_CameraView g_parcelView;


	//Specific function to invoke because of timer
	private void SetToHasInk()
	{
		g_stampState = SC_StampStateEnum.HAS_INK;
		SetMoveStampState(g_stampState);
	}

	private void TryFinishStamp()
	{
		if (g_stampState != SC_StampStateEnum.NOT_HOLDING)
		{
			//stamp on package FOR LATER
			g_stampState = SC_StampStateEnum.NOT_HOLDING;

			SetMoveStampState(g_stampState);    //Maybe unnecessary everywhere

			//Enables and disables correct visuals
			g_visualHolderScript.EnableStampOnHolder();
			g_visualStampScript.DisableStampVisual();
		}
	}


	public void SetMoveStampState(SC_StampStateEnum stampState) //maybe "private" later
	{
		g_movingStampScript.MovingStampState = stampState;
		g_movingStampScript.TrySnapToPosition();
	}

	public void TryGettingSymbol(PackageStampIcon iconStampState, bool canPickupStamp)
	{
		if (g_stampState == SC_StampStateEnum.NOT_HOLDING && canPickupStamp)
		{
			g_iconPackageStamp = iconStampState;
			g_stampState = SC_StampStateEnum.OVER_INK_MOVE;

			SetMoveStampState(g_stampState);

			//Disables and enables correct visuals
			g_visualHolderScript.DisableStampOnHolder(iconStampState);
			g_visualStampScript.EnableStampVisual(iconStampState);
		}
		else
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
			g_colorStampEnum = stampColorEnum;
			g_stampState = SC_StampStateEnum.GETTING_INK;

			SetMoveStampState(g_stampState);
			g_visualStampScript.SetVisualColorStamp(stampColorEnum);	//add timer based on animation later
			Invoke("SetToHasInk", g_gettingColorTime);

			SC_CameraManager.Instance.VieverToNewView(g_parcelView);
		}
	}

	//triggers when clicking on a package while there is ink on the stamp
	public void TryOnPackageStamp(SC_Package currentPackage)
	{
		if (g_stampState == SC_StampStateEnum.HAS_INK)
		{
			//Adds color and icon to the package
			currentPackage.AddStamp(g_colorStampEnum, g_iconPackageStamp);
			TryFinishStamp();
		}
	}


}
