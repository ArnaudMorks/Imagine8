using UnityEngine;

public class SC_StampManager : MonoBehaviour
{
	[SerializeField] private SC_StampStateEnum g_stampState;
	[SerializeField] private SC_StampColorEnum g_StampColorEnum;

	[SerializeField] private SC_MovingStamp g_movingStampScript;

	[SerializeField] private float g_gettingColorTime;


	/*//set color in "SC_MovingStamp"
	private void SetStampVisualColor()
	{
		g_movingStampScript.MovingStampColorEnum = g_StampColorEnum;	//change to function in "SC_MovingStamp"
	}*/

	//Specific function to invoke
	private void SetToHasInk()
	{
		g_stampState = SC_StampStateEnum.HAS_INK;
	}


	//triggers when clicking on an ink color while holding the stamp over said color
	public void TryGettingSpecificColor(SC_StampColorEnum stampColorEnum)
	{
		if (g_stampState == SC_StampStateEnum.OVER_INK_MOVE)
		{
			g_StampColorEnum = stampColorEnum;
			g_stampState = SC_StampStateEnum.GETTING_INK;

			SetMoveStampState(SC_StampStateEnum.GETTING_INK);
			g_movingStampScript.SetVisualColorStamp(stampColorEnum);	//add timer based on animation later
			Invoke("SetToHasInk", g_gettingColorTime);
		}
	}

	public void SetMoveStampState(SC_StampStateEnum stampState)	//maybe "private" later
	{
		g_movingStampScript.MovingStampState = stampState;
	}

}
