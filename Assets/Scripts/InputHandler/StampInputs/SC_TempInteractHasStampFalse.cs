
// [Summary] (By Arnaud)
//
// This script makes a spesific ink color interactable with the stamp, and sets the
// stamps color.
// Copied from "SC_TemporaryInteractTest"
//


using CameraSystem;
using UnityEngine;

[RequireComponent(typeof(SC_TemporaryItemInteractReceiver))]
public class SC_TempInteractHasStampFalse : MonoBehaviour
{
	// My item interact receiver
	private SC_TemporaryItemInteractReceiver g_itemInteractReceiver;

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
		print("Trying to put stamp on package");
		g_stampManagerScript.TryFinishStamp();
	}

	#endregion
}
