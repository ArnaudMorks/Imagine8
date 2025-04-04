
// [Summary] (By Arnaud)
//
// This script picks up a spesific stamp when clicked WHILE no stamp is being held yet.
// If a stamp is being held, it puts the stamp back.
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
	public PackageStampIcon ThisStampIconState
	{
		get { return g_thisStampIconState; }
		set { g_thisStampIconState = value; }
	}

	[SerializeField] private bool g_thisCanPickupStamp;
	public bool ThisCanPickupStamp
	{
		get { return g_thisCanPickupStamp; }
		set { g_thisCanPickupStamp = value; }
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
	}

	#endregion

	#region Subscription Functions

	// On received hit subscription.
	private void SubscribeOnReceivedHit() => g_itemInteractReceiver.OnReceivedHit += ReceivedResult;
	private void UnSubscribeOnReceivedHit() => g_itemInteractReceiver.OnReceivedHit -= ReceivedResult;

	// On received hit result.
	private void ReceivedResult()
	{
		SetSymbol();
	}

	private void SetSymbol()
	{
		//try grabbing stamp, or putting stamp back if holding a stamp already
		g_stampManagerScript.TryGettingSymbol(g_thisStampIconState, g_thisCanPickupStamp);
	}

	#endregion
}
