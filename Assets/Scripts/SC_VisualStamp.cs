using UnityEngine;

public class SC_VisualStamp : MonoBehaviour
{
	[SerializeField] private Animator g_animatorStamp;

	[SerializeField] private Sprite[] g_allStampIconSprites;
	[SerializeField] private GameObject g_spriteStampObject;

	//"0" = "handle", "1" = "body", "2" = "bottom"
	[SerializeField] private GameObject[] g_interactStampObjectParts;

	[SerializeField] private Material g_currentStampColor;

	//Assumes the "g_allStampColors" is the same order as "PackageStampColor"
	//"8" = grey; DOES NOT EXIST IN ENUM
	[SerializeField] private Material[] g_allStampColors;

	[SerializeField] private PackageStampColor g_currentStateColor;

	//Match with animation on ink
	[SerializeField] private float g_colorInkTime;


	//MAKE BETTER SYSTEM LATER
	private Sprite SymbolToImageSetter(PackageStampIcon iconStampEnum)
	{
		Sprite currentSprite;
		currentSprite = g_allStampIconSprites[0];	//default so it always returns a value

		switch (iconStampEnum)
		{
			case PackageStampIcon.SQUARE:
				currentSprite = g_allStampIconSprites[0];
				break;
			case PackageStampIcon.STAR:
				currentSprite = g_allStampIconSprites[1];
				break;
			case PackageStampIcon.CIRCLE:
				currentSprite = g_allStampIconSprites[2];
				break;
			case PackageStampIcon.TRIANGLE:
				currentSprite = g_allStampIconSprites[3];
				break;
			case PackageStampIcon.ASTERRISK:	//??
				break;
			case PackageStampIcon.DIAMOND:
				currentSprite = g_allStampIconSprites[4];
				break;
			case PackageStampIcon.CRESENT:
				currentSprite = g_allStampIconSprites[5];
				break;
			case PackageStampIcon.BOWIE:
				currentSprite = g_allStampIconSprites[6];
				break;
			case PackageStampIcon.HEART:
				currentSprite = g_allStampIconSprites[7];
				break;
			case PackageStampIcon.FLAKE:
				currentSprite = g_allStampIconSprites[8];
				break;
			default:
				break;
		}

		return currentSprite;

	}

	//Executed from "SetCurrentAnimationStamp" when getting ink
	private void SetVisualColorStamp()
	{
		g_currentStampColor = g_allStampColors[((int)g_currentStateColor)];

		if (g_interactStampObjectParts[2].GetComponent<MeshRenderer>().material != null)
			g_interactStampObjectParts[2].GetComponent<MeshRenderer>().material = g_currentStampColor;
		else
			Debug.LogError("Couldn't find MeshRenderer in InteractStamp");
	}


	public void SetCurrentAnimationStamp(SC_StampStateEnum stampState,
		PackageStampColor stampColorEnum)
	{
		//"stampColorEnum" only useful when gettin ink
		if (stampState == SC_StampStateEnum.GETTING_INK)
		{
			g_currentStateColor = stampColorEnum;
			Invoke("SetVisualColorStamp", g_colorInkTime);
		}

		g_animatorStamp.SetInteger("StampState", (int)stampState);
	}

	public void EnableStampVisual(PackageStampIcon iconStampEnum)
	{
		//take original; FOR LATER
		SpriteRenderer spriteRenderer = g_spriteStampObject.GetComponent<SpriteRenderer>();
		spriteRenderer.enabled = true;

		//set current stamp sprite
		spriteRenderer.sprite = SymbolToImageSetter(iconStampEnum);

		for (int i = 0; i < g_interactStampObjectParts.Length; i++)
		{
			MeshRenderer meshRenderer = g_interactStampObjectParts[i].GetComponent<MeshRenderer>();
			meshRenderer.enabled = true;
		}

		//resets to grey (NO COLOR)
		g_currentStampColor = g_allStampColors[8];
		g_interactStampObjectParts[2].GetComponent<MeshRenderer>().material = g_currentStampColor;
	}

	public void DisableStampVisual()
	{
		//put original back; FOR LATER
		SpriteRenderer spriteRenderer = g_spriteStampObject.GetComponent<SpriteRenderer>();
		spriteRenderer.enabled = false;

		for (int i = 0; i < g_interactStampObjectParts.Length; i++)
		{
			MeshRenderer meshRenderer = g_interactStampObjectParts[i].GetComponent<MeshRenderer>();
			meshRenderer.enabled = false;
		}
	}

}
