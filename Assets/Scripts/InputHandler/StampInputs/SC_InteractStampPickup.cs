
// [Summary] (By Arnaud)
//
// This script picks up a spesific stamp when clicked WHILE no stamp is being held yet.
// This script gets accessed by the stamp manager to change which icon is currently held;
// in case the stamp holder gets turned NOT MADE YET; MAYBE LOGIC CHANGES.
// Copied from "SC_TemporaryInteractTest"
//


using CameraSystem;
using UnityEngine;

[RequireComponent(typeof(SC_TemporaryItemInteractReceiver))]
public class SC_InteractStampPickup : MonoBehaviour
{
	// My item interact receiver
	private SC_TemporaryItemInteractReceiver g_itemInteractReceiver;

	[SerializeField] private PackageStampIcon g_thisStampIconState;
	public PackageStampIcon ThisStampSymbolState
	{
		get { return g_thisStampIconState; }
		set { g_thisStampIconState = value; }
	}

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
		SetSymbol();
	}

	private void SetSymbol()
	{
		print("Setting a Symbol in StampManager");
		g_stampManagerScript.TryGettingSymbol(g_thisStampIconState);
	}

	#endregion
}
