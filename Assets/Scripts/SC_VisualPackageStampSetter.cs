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

	[SerializeField] private float g_yPosition;

	private Sprite SymbolToImageSetter(PackageStampIcon iconStampEnum)
	{
		Sprite currentSprite;

		int i = 0;		//default so it always returns a value
		switch (iconStampEnum)
		{
			case PackageStampIcon.SQUARE:
				i = 0;
				break;
			case PackageStampIcon.STAR:
				i = 1;
				break;
			case PackageStampIcon.CIRCLE:
				i = 2;
				break;
			case PackageStampIcon.TRIANGLE:
				i = 3;
				break;
			case PackageStampIcon.ASTERRISK:
				break;
			case PackageStampIcon.DIAMOND:
				i = 4;
				break;
			case PackageStampIcon.CRESENT:
				i = 5;
				break;
			case PackageStampIcon.BOWIE:
				i = 6;
				break;
			case PackageStampIcon.HEART:
				i = 7;
				break;
			case PackageStampIcon.FLAKE:
				i = 8;
				break;
			default:
				break;
		}
		currentSprite = g_allStampedIconSprites[i];

		return currentSprite;
	}


	public void MakeVisualIconOnPackage(int spriteArrayLocation, int colorArrayLocation, 
		Vector3 movingStampPosition)
	{
		g_poolStamps.ActivateStampVisual(g_allStampedIconSprites[spriteArrayLocation],
			g_visualColors[colorArrayLocation],
			new Vector3(movingStampPosition.x, g_yPosition, movingStampPosition.z));
	}

}
