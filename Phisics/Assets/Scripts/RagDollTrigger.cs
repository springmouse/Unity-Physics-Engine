using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDollTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        RagDoll ragDoll = other.gameObject.GetComponentInParent<RagDoll>();
        
        if (ragDoll != null)
        {
            ragDoll.RagdollOn = true;
        }
    }
}
