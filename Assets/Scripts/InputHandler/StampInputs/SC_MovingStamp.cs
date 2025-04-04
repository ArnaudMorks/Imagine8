using UnityEngine;
using UnityEngine.InputSystem;


//Moves the stamp over a certain area based on the stamp state
public class SC_MovingStamp : MonoBehaviour
{
	[SerializeField] private SC_StampStateEnum g_stampState;
	public SC_StampStateEnum MovingStampState		//maybe in function later
	{
		get { return g_stampState; }
		set { g_stampState = value; }
	}

	[SerializeField] private LayerMask g_itemTriggersLayerMask;
	[SerializeField] private float g_rayMaxDistance;
	[SerializeField] private float g_mousePosition;

	//"Vector2.y" meant for "transform.position.z"
	[SerializeField] private Vector2 g_inkMinXZPosition;
	[SerializeField] private Vector2 g_inkMaxXZPosition;

	[SerializeField] private Vector2 g_deskMinXZPosition;
	[SerializeField] private Vector2 g_deskMaxXZPosition;

	//"0" = over ink, "1" = over desk
	[SerializeField] private GameObject[] g_snapPositions;


	private void Update()
	{
		if (g_stampState == SC_StampStateEnum.OVER_INK_MOVE)
			CheckAndSetStampPosition(g_rayMaxDistance, g_inkMinXZPosition, g_inkMaxXZPosition);
		else if (g_stampState == SC_StampStateEnum.HAS_INK)
			CheckAndSetStampPosition(g_rayMaxDistance, g_deskMinXZPosition, g_deskMaxXZPosition);

	}


	private void CheckAndSetStampPosition(float rayMaxDistance, Vector2 minXZPosition,
		Vector2 maxXZPosition)
	{
		Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
		if (Physics.Raycast(ray, out RaycastHit raycastHit, rayMaxDistance, g_itemTriggersLayerMask))
		{
				//print(raycastHit.point);

			if (raycastHit.point.x > minXZPosition.x && raycastHit.point.x < maxXZPosition.x
				&& raycastHit.point.z > minXZPosition.y && raycastHit.point.z < maxXZPosition.y)
			{
				transform.position = new Vector3(raycastHit.point.x, transform.position.y,
					raycastHit.point.z);
			}
		}

	}


	public void TrySnapToPosition()
	{
		if (g_stampState == SC_StampStateEnum.OVER_INK_MOVE)
			transform.position = g_snapPositions[0].transform.position;
		else if (g_stampState == SC_StampStateEnum.HAS_INK)
			transform.position = g_snapPositions[1].transform.position;
	}

}
