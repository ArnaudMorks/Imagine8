using UnityEngine;

public class SC_HolderStampManager : MonoBehaviour
{
	[SerializeField] private SC_HolderStampEnum g_holderStampState;

	[SerializeField] private SC_InteractStampPickup[] g_interactStampPickups;

	//All seperate variables (arrays) instead of a 2d array; inspector doesn't show 2d arrays.
	[SerializeField] private PackageStampIcon[] g_0ColumnStampIcons;
	[SerializeField] private PackageStampIcon[] g_1ColumnStampIcons;
	[SerializeField] private PackageStampIcon[] g_2ColumnStampIcons;
	[SerializeField] private PackageStampIcon[] g_3ColumnStampIcons;
	[SerializeField] private PackageStampIcon[] g_4ColumnStampIcons;
	[SerializeField] private PackageStampIcon[] g_5ColumnStampIcons;

	//Prevents a stamp from being picked up when clicked
	[SerializeField] private bool[] g_0ColumnHasStamp;
	[SerializeField] private bool[] g_1ColumnHasStamp;
	[SerializeField] private bool[] g_2ColumnHasStamp;
	[SerializeField] private bool[] g_3ColumnHasStamp;
	[SerializeField] private bool[] g_4ColumnHasStamp;
	[SerializeField] private bool[] g_5ColumnHasStamp;


	private void CheckCurrentState(SC_HolderStampEnum currentHolderStampState)
	{
		switch (currentHolderStampState)
		{
			case SC_HolderStampEnum.ZERO:
				SetCurrentIcons(g_0ColumnStampIcons, g_0ColumnHasStamp);
				break;
			case SC_HolderStampEnum.ONE:
				SetCurrentIcons(g_1ColumnStampIcons, g_1ColumnHasStamp);
				break;
			case SC_HolderStampEnum.TWO:
				SetCurrentIcons(g_2ColumnStampIcons, g_2ColumnHasStamp);
				break;
			case SC_HolderStampEnum.THREE:
				SetCurrentIcons(g_3ColumnStampIcons, g_3ColumnHasStamp);
				break;
			case SC_HolderStampEnum.FOUR:
				SetCurrentIcons(g_4ColumnStampIcons, g_4ColumnHasStamp);
				break;
			case SC_HolderStampEnum.FIVE:
				SetCurrentIcons(g_5ColumnStampIcons, g_5ColumnHasStamp);
				break;
			default:
				break;
		}
	}

	private void SetCurrentIcons(PackageStampIcon[] currentColumnStampIcons,
		bool[] currentCanPickupStamps)
	{
		print("Setting current icons");
		for (int i = 0; i < g_interactStampPickups.Length; i++)
		{
			g_interactStampPickups[i].ThisStampIconState = currentColumnStampIcons[i];
			g_interactStampPickups[i].ThisCanPickupStamp = currentCanPickupStamps[i];
		}
	}


	//"0" = to left side, "1" = to right side
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
