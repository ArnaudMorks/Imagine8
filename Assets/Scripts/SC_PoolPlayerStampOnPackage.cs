using UnityEngine;

public class SC_PoolPlayerStampOnPackage : MonoBehaviour
{
	[SerializeField] private int g_poolSize;

	[SerializeField] private SC_StampOnPackage g_stampOnPackagePrefab = null;
	[SerializeField] private SC_StampOnPackage[] g_stampsOnPackage = null;

	[SerializeField] private int g_activeOverride;


	private void Start()
	{
		MakeStampsOnPackage();
	}

	private void MakeStampsOnPackage()
	{
		g_stampsOnPackage = new SC_StampOnPackage[g_poolSize];

		for (int i = 0; i < g_poolSize; i++)
		{
			//Create new bullet
			SC_StampOnPackage newStampOnPackage = Instantiate<SC_StampOnPackage>(g_stampOnPackagePrefab);

			//Parent object
			newStampOnPackage.transform.parent = gameObject.transform;

			g_stampsOnPackage[i] = newStampOnPackage;
		}
		DisableVisualStamps();
	}


	public void DisableVisualStamps()
	{
		for (int i = 0; i < g_poolSize; i++)
		{
			g_stampsOnPackage[i].gameObject.SetActive(false);
		}
	}

	public void ActivateStampVisual(Sprite thisSprite, Color thisColor, Vector3 thisposition)
	{
		SC_StampOnPackage availableStampOnPackage = null;

		for (int i = 0; i < g_stampsOnPackage.Length; i++)
		{
			if (g_stampsOnPackage[i].isActiveAndEnabled == false)
			{
				availableStampOnPackage = g_stampsOnPackage[i];
				break;
			}
		}

		if (availableStampOnPackage == null)
		{
			availableStampOnPackage = g_stampsOnPackage[g_activeOverride];

			if (g_activeOverride >= g_stampsOnPackage.Length)
				g_activeOverride = 0;
			else
				g_activeOverride++;
		}

		availableStampOnPackage.transform.position = thisposition;

		availableStampOnPackage.VisualStampOnPackage(thisSprite, thisColor);
		availableStampOnPackage.gameObject.SetActive(true);
		//return availableShooterEnemies;
	}

}
