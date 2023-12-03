using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    [SerializeField] private float _time;

    private void Start()
    {
        Destroy(gameObject, _time);
    }
}
