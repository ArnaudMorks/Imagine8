using UnityEngine;

public class SC_VisualStamp : MonoBehaviour
{
	[SerializeField] private GameObject g_bottomStampMeshRenderer;
	[SerializeField] private Material g_currentStampColor;

	//Assumes the "g_allStampColors" is the same order as "SC_STampStateEnum"
	[SerializeField] private Material[] g_allStampColors;


	public void SetVisualColorStamp(SC_StampColorEnum stampColorEnum)
	{
		g_currentStampColor = g_allStampColors[((int)stampColorEnum)];
		g_bottomStampMeshRenderer.GetComponent<MeshRenderer>().material = g_currentStampColor;
	}

}
