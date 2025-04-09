using UnityEngine;

public class SC_VisualHolderStamps : MonoBehaviour
{
	[SerializeField] private GameObject[] g_visualStampsOnHolder;

	//The stamp that isn't on the holder anymore, and can be put back
	[SerializeField] private GameObject g_currentlyPickedUpStamp;


	//Activated from "SC_StampManager" when a stamp is picked up
	public void DisableStampOnHolder(PackageStampIcon currentStampIcon)
	{
		int i = 0;
		switch (currentStampIcon)
		{
			case PackageStampIcon.SQUARE:
				i = 9;
				break;
			case PackageStampIcon.STAR:
				i = 1;
				break;
			case PackageStampIcon.CIRCLE:
				i = 2;
				break;
			case PackageStampIcon.TRIANGLE:
				i = 7;
				break;
			case PackageStampIcon.ASTERRISK:
				print("No asterrisk yet");
				return;
			case PackageStampIcon.DIAMOND:
				i = 3;
				break;
			case PackageStampIcon.CRESENT:
				i = 4;
				break;
			case PackageStampIcon.BOWIE:
				i = 5;
				break;
			case PackageStampIcon.HEART:
				i = 6;
				break;
			case PackageStampIcon.FLAKE:
				i = 8;
				break;
			default:
				return;
		}

		g_currentlyPickedUpStamp = g_visualStampsOnHolder[i];
		g_currentlyPickedUpStamp.SetActive(false);
	}

	public void EnableStampOnHolder()
	{
		g_currentlyPickedUpStamp.SetActive(true);
	}

}
