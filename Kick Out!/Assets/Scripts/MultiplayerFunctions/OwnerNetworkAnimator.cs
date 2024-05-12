using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode.Components;
using Unity.Netcode;

public class OwnerNetworkAnimator : NetworkAnimator
{
    protected override bool OnIsServerAuthoritative()
    {
        return false;
    }

}