using UnityEngine;

public class SC_HolderStampManager : MonoBehaviour
{
	[SerializeField] private SC_HolderStampEnum g_holderStampState;

	[SerializeField] private SC_InteractStampPickup[] g_interactStampPickups;

	//All seperate variables to make it visable in inspector
	[SerializeField] private PackageStampIcon[] g_0ColumnStampIcons;
	[SerializeField] private PackageStampIcon[] g_1ColumnStampIcons;
	[SerializeField] private PackageStampIcon[] g_2ColumnStampIcons;
	[SerializeField] private PackageStampIcon[] g_3ColumnStampIcons;
	[SerializeField] private PackageStampIcon[] g_4ColumnStampIcons;
	[SerializeField] private PackageStampIcon[] g_5ColumnStampIcons;


	private void CheckCurrentState(SC_HolderStampEnum currentHolderStampState)
	{
		switch (currentHolderStampState)
		{
			case SC_HolderStampEnum.ZERO:
				SetCurrentIcons(g_0ColumnStampIcons);
				break;
			case SC_HolderStampEnum.ONE:
				SetCurrentIcons(g_1ColumnStampIcons);
				break;
			case SC_HolderStampEnum.TWO:
				SetCurrentIcons(g_2ColumnStampIcons);
				break;
			case SC_HolderStampEnum.THREE:
				SetCurrentIcons(g_3ColumnStampIcons);
				break;
			case SC_HolderStampEnum.FOUR:
				SetCurrentIcons(g_4ColumnStampIcons);
				break;
			case SC_HolderStampEnum.FIVE:
				SetCurrentIcons(g_5ColumnStampIcons);
				break;
			default:
				break;
		}
	}

	private void SetCurrentIcons(PackageStampIcon[] currentColumnStampIcons)
	{
		print("Setting current icons");
		for (int i = 0; i < g_interactStampPickups.Length; i++)
		{
			g_interactStampPickups[i].ThisStampIconState = currentColumnStampIcons[i];
		}
	}


	//"0" = to left side, "1" = to right side
	//Turns the 
	public void TurnHolderStamp(int direction)
	{
		if (direction == 0)
		{
			if (g_holderStampState != SC_HolderStampEnum.FIVE)
				g_holderStampState++;
			else
				g_holderStampState = SC_HolderStampEnum.ZERO;
		}
		else if (direction == 1)
		{
			if (g_holderStampState != SC_HolderStampEnum.ZERO)
				g_holderStampState--;
			else
				g_holderStampState = SC_HolderStampEnum.FIVE;
		}

		CheckCurrentState(g_holderStampState);
	}

}
