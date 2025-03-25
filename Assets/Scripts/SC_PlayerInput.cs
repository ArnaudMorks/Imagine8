using UnityEngine;
using UnityEngine.InputSystem;

public class SC_PlayerInput : MonoBehaviour
{
	[SerializeField] private Collider g_stampOverInkTriggerArea;
	[SerializeField] private Transform g_stampOverInkTransform;


	private void Update()
	{
		if (Mouse.current.leftButton.isPressed)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (g_stampOverInkTriggerArea.Raycast(ray, out hit, 100.0f))
			{
				g_stampOverInkTransform.position = ray.GetPoint(100.0f);
			}
		}

	}



}
