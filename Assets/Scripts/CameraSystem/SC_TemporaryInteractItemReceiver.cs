
// [Summary] (By Wessel)
//
// This script is in charge of receiving a interact signal,
// allow other functions on this object to subscribe to it.
//

using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class SC_TemporaryItemInteractReceiver : MonoBehaviour
{
    // Actions
    public Action OnReceivedHit;


    // ----------------- Functions -----------------


    #region Subscription Functions

    // Receive item interact hit.
    public void ReceiveItemInteractHit() => OnReceivedHit?.Invoke();

    #endregion

}
