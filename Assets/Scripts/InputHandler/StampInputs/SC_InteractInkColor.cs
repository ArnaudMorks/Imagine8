
// [Summary] (By Arnaud)
//
// This script is a spesific use copied from "SC_TemporaryInteractTest"
//


using CameraSystem;
using UnityEngine;

[RequireComponent(typeof(SC_TemporaryItemInteractReceiver))]
public class SC_InteractInkColor : MonoBehaviour
{
	// My item interact receiver
	private SC_TemporaryItemInteractReceiver g_itemInteractReceiver;

	[SerializeField] private SC_StampColorEnum g_thisStampColor;

	[SerializeField] private SC_StampManager g_stampManagerScript;


	// ----------------- Functions -----------------


	#region OnEnable Functions

	// Changes that happen on enable.
	private void OnEnable()
	{
		TryAssignItemInteractReceiver();
		SubscribeOnReceivedHit();

		// Expand..
	}

	// Auto-assign the item interact receiver.
	private void TryAssignItemInteractReceiver()
	{
		if (this.TryGetComponent(out SC_TemporaryItemInteractReceiver itemInteractReceiver))
			g_itemInteractReceiver = itemInteractReceiver;
		else
		Debug.LogWarning("Unable to get my _itemInteractReceiver component!");
	}


	#endregion

	#region OnDisable Functions

	// Changes that happen on disable.
	private void OnDisable()
	{
		UnSubscribeOnReceivedHit();

		// Expand..
	}

	#endregion

	#region Subscription Functions

	// On received hit subscription.
	private void SubscribeOnReceivedHit() => g_itemInteractReceiver.OnReceivedHit += ReceivedResult;
	private void UnSubscribeOnReceivedHit() => g_itemInteractReceiver.OnReceivedHit -= ReceivedResult;

	// On received hit result.
	private void ReceivedResult()
	{
		Debug.Log("I received the signal all the way down here!" + this.gameObject.name);
		SetStampColorInManager();
	}

	private void SetStampColorInManager()
	{
		print("Setting ink color in StampManager");
		g_stampManagerScript.TryGettingSpecificColor(g_thisStampColor);
	}

	#endregion
}
