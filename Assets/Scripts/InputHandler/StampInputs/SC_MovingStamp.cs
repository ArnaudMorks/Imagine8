using UnityEngine;
using UnityEngine.InputSystem;


//Moves the stamp over a certain area based on the stamp state
public class SC_MovingStamp : MonoBehaviour
{
	[SerializeField] private SC_StampStateEnum g_stampState;
	public SC_StampStateEnum MovingStampState
	{
		get { return g_stampState; }
		set { g_stampState = value; }
	}

	[SerializeField] private LayerMask g_stampMoveTriggersLayerMask;
	[SerializeField] private float g_rayMaxDistance;
	[SerializeField] private float g_mousePosition;

	//"Vector2.y" meant for "transform.position.z"
	[SerializeField] private Vector2 g_inkMinXZPosition;
	[SerializeField] private Vector2 g_inkMaxXZPosition;

	[SerializeField] private Vector2 g_deskMinXZPosition;
	[SerializeField] private Vector2 g_deskMaxXZPosition;

	//Ink snap when getting ink
	//Location of upper left color
	[SerializeField] private Vector2 g_firstColorXZLocation;
	[SerializeField] private Vector2 g_nextColorXZOffset;

	//"0" = over ink, "1" = over desk
	[SerializeField] private GameObject[] g_snapPositions;

	[SerializeField] private float g_yInkPosition;
	[SerializeField] private float g_yDeskPosition;


	private void Update()
	{
		if (g_stampState == SC_StampStateEnum.OVER_INK_MOVE)
			CheckAndSetStampPosition(g_rayMaxDistance, g_inkMinXZPosition, g_inkMaxXZPosition, g_yInkPosition);
		else if (g_stampState == SC_StampStateEnum.HAS_INK)
			CheckAndSetStampPosition(g_rayMaxDistance, g_deskMinXZPosition, g_deskMaxXZPosition, g_yDeskPosition);

	}


	private void CheckAndSetStampPosition(float rayMaxDistance, Vector2 minXZPosition,
		Vector2 maxXZPosition, float currentYPosition)
	{
		Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
		if (Physics.Raycast(ray, out RaycastHit raycastHit, rayMaxDistance, g_stampMoveTriggersLayerMask))
		{
				//print(raycastHit.point);

			if (raycastHit.point.x > minXZPosition.x && raycastHit.point.x < maxXZPosition.x
				&& raycastHit.point.z > minXZPosition.y && raycastHit.point.z < maxXZPosition.y)
			{
				transform.position = new Vector3(raycastHit.point.x, currentYPosition,
					raycastHit.point.z);
			}
		}

	}


	public void TrySetCurrentColorPosition(PackageStampColor currentColor)
	{
		if (g_stampState != SC_StampStateEnum.GETTING_INK)
			return;

		int j = ((int)currentColor);

		//"Offset" from first color
		float currentXOffset = 0;
		float currentZOffset = 0;

		for (int i = 0; i <= j; i++)
		{
			if (i != 0)
			{
				if (i % 2 == 0)
				{
					currentZOffset += g_nextColorXZOffset.y;

					currentXOffset = 0;     //"0" because it's already at the correct position by default
				}
				else
					currentXOffset = g_nextColorXZOffset.x;
			}
		}

		transform.position = new Vector3(g_firstColorXZLocation.x + currentXOffset,
			transform.position.y, g_firstColorXZLocation.y - currentZOffset);
	}

	public void TrySnapToPosition()
	{
		if (g_stampState == SC_StampStateEnum.OVER_INK_MOVE)
			transform.position = g_snapPositions[0].transform.position;
		else if (g_stampState == SC_StampStateEnum.HAS_INK)
			transform.position = g_snapPositions[1].transform.position;
	}

}
