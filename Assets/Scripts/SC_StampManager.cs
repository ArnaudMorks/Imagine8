using UnityEngine;

//LOT OF REPEATING CODE; IMPROVE LATER
public class SC_StampManager : MonoBehaviour
{
	//Takes all stamp pickup hitboxes from high to low
	[SerializeField] private SC_InteractStampPickup[] g_interactStampPickups;

	[SerializeField] private SC_StampStateEnum g_stampState;
	[SerializeField] private SC_StampSymbolEnum g_symbolStampState;
	[SerializeField] private SC_StampColorEnum g_colorStampEnum;

	[SerializeField] private SC_MovingStamp g_movingStampScript;
	[SerializeField] private SC_VisualStamp g_visualStampScript;

	[SerializeField] private float g_gettingColorTime;



	//Specific function to invoke
	private void SetToHasInk()
	{
		g_stampState = SC_StampStateEnum.HAS_INK;
	}
	public void SetMoveStampState(SC_StampStateEnum stampState)	//maybe "private" later
	{
		g_movingStampScript.MovingStampState = stampState;
	}

	public void TryGettingSymbol(SC_StampSymbolEnum symbolStampState)
	{
		if (g_stampState == SC_StampStateEnum.NOT_HOLDING)
		{
			//Disable visual of grabbed stamp LATER
			g_symbolStampState = symbolStampState;
			g_stampState = SC_StampStateEnum.OVER_INK_MOVE;

			SetMoveStampState(g_stampState);
			g_visualStampScript.EnableStampVisual(symbolStampState);
		}
	}

	//triggers when clicking on an ink color while holding the stamp over said color
	public void TryGettingSpecificColor(SC_StampColorEnum stampColorEnum)
	{
		if (g_stampState == SC_StampStateEnum.OVER_INK_MOVE)
		{
			g_colorStampEnum = stampColorEnum;
			g_stampState = SC_StampStateEnum.GETTING_INK;

			SetMoveStampState(g_stampState);
			g_visualStampScript.SetVisualColorStamp(stampColorEnum);	//add timer based on animation later
			Invoke("SetToHasInk", g_gettingColorTime);
		}
	}

	//triggers when clicking on a package while there is ink on the stamp
	public void TryFinishStamp()
	{
		if (g_stampState == SC_StampStateEnum.HAS_INK)
		{
			//stamp on package FOR LATER
			print("Put a " + g_colorStampEnum + " " + g_symbolStampState + " on the package.");
			g_stampState = SC_StampStateEnum.NOT_HOLDING;

			SetMoveStampState(g_stampState);	//Maybe unnecessary everywhere
			g_visualStampScript.DisableStampVisual();
		}
	}


}
