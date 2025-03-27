using UnityEngine;

public class SC_StampManager : MonoBehaviour
{
	[SerializeField] private SC_StampStateEnum g_stampState;
	[SerializeField] private SC_MovingStamp g_movingStampScript;


	public void SetStampState(SC_StampStateEnum stampState)
	{
		g_movingStampScript.StampState = stampState;
	}

}
