using UnityEngine;
using UnityEngine.InputSystem;

public class SC_PlayerInput : MonoBehaviour
{
	private const int SCREEN_WIDTH = 1920;
	private const int SCREEN_HEIGHT = 1080;
	[SerializeField] private Vector3 g_mousePosition;
	[SerializeField] private bool g_hasStamp = false;		//change to ENUM system later

	//assumes stamps are a grid with 4 points
	//put "InkPlayField" positions in the "g_stampPositions" array in the correct place
	//"0" = left up, "1" = right up, "2" = left down, "3" = right down
	[SerializeField] private Vector2[] g_mouseStampPositions;

	//put "X" and "Z" world positions in the array, DOES NOT affect "Y" position
	//this will set the boundries to move the "StampColorPickPivot"
	//"0" = left up, "1" = right up, "2" = left down, "3" = right down
	[SerializeField] private Vector3[] g_worldStampPositions;
	[SerializeField] private float g_allWorldYStampPosition;


	private void Start()
	{
		g_mouseStampPositions = SetStampCanvasToMousePositions(g_mouseStampPositions);
	}

	private void Update()
	{
		g_mousePosition = Mouse.current.position.ReadValue();
		print(g_mousePosition);

		

	}


	Vector2[] SetStampCanvasToMousePositions(Vector2[] stampPositions)
	{
		//canvas width position is opposite of mouse position, therefore this for loop
		//with canvas height "0" is in the middle of the screen
		for (int i = 0; i < 4; i++)		//"4" because there are four stamp positions
		{
			stampPositions[i].x += SCREEN_WIDTH;
			stampPositions[i].y += SCREEN_HEIGHT / 2;
		}

		return stampPositions;
	}

}
