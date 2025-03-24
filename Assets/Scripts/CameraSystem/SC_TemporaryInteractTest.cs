
// [Summary] (By Wessel)
//
// This script is for testing interactions.
//


using CameraSystem;
using UnityEngine;

[RequireComponent(typeof(SC_TemporaryItemInteractReceiver))]
public class SC_TemporaryInteractTest : MonoBehaviour
{
    // My item interact receiver
    private SC_TemporaryItemInteractReceiver _itemInteractReceiver;


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
            _itemInteractReceiver = itemInteractReceiver;
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
    private void SubscribeOnReceivedHit() => _itemInteractReceiver.OnReceivedHit += ReceivedResult;
    private void UnSubscribeOnReceivedHit() => _itemInteractReceiver.OnReceivedHit -= ReceivedResult;

    // On received hit result.
    private void ReceivedResult()
    {
        Debug.Log("I received the signal all the way down here!" + this.gameObject.name);
    }

    #endregion
}
