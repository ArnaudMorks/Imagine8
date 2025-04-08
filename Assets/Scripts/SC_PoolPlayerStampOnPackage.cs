using UnityEngine;

public class SC_PoolPlayerStampOnPackage : MonoBehaviour
{
	[SerializeField] private int g_poolSize;

	[SerializeField] private SC_StampOnPackage g_stampOnPackagePrefab = null;
	[SerializeField] private SC_StampOnPackage[] g_stampsOnPackage = null;


	private void Start()
	{
		MakeEnemies();
	}

	private void MakeEnemies()
	{
		g_stampsOnPackage = new SC_StampOnPackage[g_poolSize];

		for (int i = 0; i < g_poolSize; i++)
		{
			//Create new bullet
			SC_StampOnPackage newStampOnPackage = Instantiate<SC_StampOnPackage>(g_stampOnPackagePrefab);

			//Paren object
			newStampOnPackage.transform.parent = gameObject.transform;

			//Deactivate it
			newStampOnPackage.gameObject.SetActive(false);

			g_stampsOnPackage[i] = newStampOnPackage;
		}
	}

}
