using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Character otherCharecter))
        {
            otherCharecter.Die();
        }
    }
}
