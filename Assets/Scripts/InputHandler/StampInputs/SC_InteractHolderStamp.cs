
// [Summary] (By Arnaud)
//
// This script turns the stamp holder and executes a function in "SC_HolderStampManager"
// Copied from "SC_TemporaryInteractTest"
//


using UnityEngine;

[RequireComponent(typeof(SC_TemporaryItemInteractReceiver))]
public class SC_InteractHolderStamp : MonoBehaviour
{
    // My item interact receiver
    private SC_TemporaryItemInteractReceiver g_itemInteractReceiver;

    //"0" = turn to left, "1" = turn to right
    [SerializeField] private int g_turnToDirection;

    [SerializeField] private SC_HolderStampManager g_holderStampManager;

    [Header("Audio")]
    [SerializeField] private AudioSource g_audioSource;


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
        print("Hitting");
        TurnToSideInteract();
    }

    private void TurnToSideInteract()
    {
        if (g_audioSource != null)
            g_audioSource.Play();

        g_holderStampManager.TurnHolderStamp(g_turnToDirection);
    }

    #endregion
}
