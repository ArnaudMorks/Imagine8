using UnityEngine;
using UnityEngine.InputSystem;

public class SC_MovingStamp : MonoBehaviour
{
	[SerializeField] private SC_StampStateEnum g_stampState;
	public SC_StampStateEnum StampState
	{
		get { return g_stampState; }
		set { g_stampState = value; }
	}

	[SerializeField] private LayerMask g_itemTriggersLayerMask;
	[SerializeField] private float g_rayMaxDistance;
	[SerializeField] private float g_mousePosition;

	//"Vector2.y" meant for "transform.position.z"
	[SerializeField] private Vector2 g_minXZPosition;
	[SerializeField] private Vector2 g_maxXZPosition;


	private void Update()
	{
		if (g_stampState == SC_StampStateEnum.OVER_INK_MOVE)
			CheckAndSetStampPosition(g_rayMaxDistance, g_minXZPosition, g_maxXZPosition);

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


}
