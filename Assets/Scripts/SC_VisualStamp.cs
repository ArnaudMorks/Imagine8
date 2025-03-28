using UnityEngine;

public class SC_VisualStamp : MonoBehaviour
{
	[SerializeField] private Sprite[] g_allStampIconSprites;
	[SerializeField] private GameObject g_spriteStampObject;

	//"0" = "handle", "1" = "body", "2" = "bottom"
	[SerializeField] private GameObject[] g_interactStampObjectParts;

	[SerializeField] private Material g_currentStampColor;

	//Assumes the "g_allStampColors" is the same order as "SC_STampStateEnum"
	[SerializeField] private Material[] g_allStampColors;


	//MAKE BETTER SYSTEM LATER
	private Sprite SymbolToImageSetter(SC_StampSymbolEnum symbolStampEnum)
	{
		Sprite currentSprite;
		currentSprite = g_allStampIconSprites[0];	//default so it always returns a value

		switch (symbolStampEnum)
		{
			case SC_StampSymbolEnum.NO_SYMBOL:
				break;
			case SC_StampSymbolEnum.SQUARE:
				currentSprite = g_allStampIconSprites[0];
				break;
			case SC_StampSymbolEnum.STAR:
				currentSprite = g_allStampIconSprites[1];
				break;
			case SC_StampSymbolEnum.CIRCLE:
				currentSprite = g_allStampIconSprites[2];
				break;
			case SC_StampSymbolEnum.TRIANGLE:
				break;
			case SC_StampSymbolEnum.ASTERRISK:
				break;
			case SC_StampSymbolEnum.DIAMOND:
				currentSprite = g_allStampIconSprites[3];
				break;
			default:
				break;
		}

		return currentSprite;

	}


	public void SetVisualColorStamp(SC_StampColorEnum stampColorEnum)
	{
		g_currentStampColor = g_allStampColors[((int)stampColorEnum)];

		if (g_interactStampObjectParts[2].GetComponent<MeshRenderer>().material != null)
			g_interactStampObjectParts[2].GetComponent<MeshRenderer>().material = g_currentStampColor;
		else
			Debug.LogError("Couldn't find MeshRenderer in InteractStamp");
	}

	public void EnableStampVisual(SC_StampSymbolEnum symbolStampEnum)
	{
		//take original; FOR LATER
		SpriteRenderer spriteRenderer = g_spriteStampObject.GetComponent<SpriteRenderer>();
		spriteRenderer.enabled = true;

		//set current stamp sprite
		spriteRenderer.sprite = SymbolToImageSetter(symbolStampEnum);

		for (int i = 0; i < g_interactStampObjectParts.Length; i++)
		{
			MeshRenderer meshRenderer = g_interactStampObjectParts[i].GetComponent<MeshRenderer>();
			meshRenderer.enabled = true;
		}
	}

	public void DisableStampVisual()
	{
		//put original back; FOR LATER
		SetVisualColorStamp(SC_StampColorEnum.NO_COLOR);	//resets color to "NO_COLOR"

		SpriteRenderer spriteRenderer = g_spriteStampObject.GetComponent<SpriteRenderer>();
		spriteRenderer.enabled = false;

		for (int i = 0; i < g_interactStampObjectParts.Length; i++)
		{
			MeshRenderer meshRenderer = g_interactStampObjectParts[i].GetComponent<MeshRenderer>();
			meshRenderer.enabled = false;
		}
	}

}
