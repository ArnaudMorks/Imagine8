// [Summary] (By Arnaud)
//
// This script is ONLY for setting stamps on the package placed by the Player
//

using UnityEngine;

public class SC_VisualPackageStampSetter : MonoBehaviour
{
	[SerializeField] private SC_PoolPlayerStampOnPackage g_poolStamps;

	[SerializeField] private Sprite[] g_allStampedIconSprites;
	[SerializeField] private Color[] g_visualColors;

	//Vector2.x = Vector3.x, Vector2.y = Vector3.z
	[SerializeField] private Vector2 g_minXZPositions;
	[SerializeField] private Vector2 g_maxXZPositions;

	[SerializeField] private float g_yPosition;


	//Limits the location of the stamp based on the location of the package
	private float CorrectOutOfBounds(float movingStampBoundPosition,
		float packageBoundPosition, float minLimitCheckPosition, float maxLimitCheckPosition)
	{
		float onPackageLocation;
		onPackageLocation = movingStampBoundPosition - packageBoundPosition;

		if (onPackageLocation < 0)
		{
			if (onPackageLocation < minLimitCheckPosition)
				onPackageLocation = packageBoundPosition + minLimitCheckPosition;
			else
				return movingStampBoundPosition;
		}
		else if (onPackageLocation >= 0)
		{
			if (onPackageLocation > maxLimitCheckPosition)
				onPackageLocation = packageBoundPosition + maxLimitCheckPosition;
			else
				return movingStampBoundPosition;
		}

		return onPackageLocation;
	}


	public void MakeVisualIconOnPackage(int spriteArrayLocation, int colorArrayLocation, 
		Vector3 movingStampPosition, Vector3 packagePosition)
	{

		float newXPosition = CorrectOutOfBounds(movingStampPosition.x, packagePosition.x,
			g_minXZPositions.x, g_maxXZPositions.x);
		float newZPosition = CorrectOutOfBounds(movingStampPosition.z, packagePosition.z,
			g_minXZPositions.y, g_maxXZPositions.y);

		g_poolStamps.ActivateStampVisual(g_allStampedIconSprites[spriteArrayLocation],
			g_visualColors[colorArrayLocation],
			new Vector3(newXPosition, g_yPosition, newZPosition));
	}

}
