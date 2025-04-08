using UnityEngine;

public class SC_StampOnPackage : MonoBehaviour
{
	[SerializeField] private SpriteRenderer g_spriteRenderer;


	public void VisualStampOnPackage(Sprite thisSprite, Color thisColor)
	{
		g_spriteRenderer.sprite = thisSprite;
		g_spriteRenderer.color = thisColor;
	}

}
