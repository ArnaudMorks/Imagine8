
// [Summary] (By Arnaud)
//
// This script communicates with "SC_StampManager" by activating its functions to put
// a stamp on the current package, and to set the interactable stamp to the correct state.
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
	[SerializeField] private SC_Package g_thisPackage;


	// ----------------- Functions -----------------


	#region OnEnable Functions

	// Changes that happen on enable.
	private void OnEnable()
	{
		TryAssignItemInteractReceiver();
		StampPackageScriptsObjectsAssign();
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

	private void StampPackageScriptsObjectsAssign()
	{
		g_stampManagerScript = FindFirstObjectByType<SC_StampManager>();
		g_thisPackage = GetComponentInParent<SC_Package>();
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
		PutStampOnPackage();
	}


	private void PutStampOnPackage()
	{
		g_stampManagerScript.TryOnPackageStamp(g_thisPackage);
	}

	#endregion
}
