using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishController : MonoBehaviour
{
    [SerializeField] private List<Transform> _positions = new List<Transform>();
    
    private int _busyIndex = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Character otherCharecter))
        {
            otherCharecter.transform.parent = null;
            otherCharecter.Finish();
            LeanTween.move(otherCharecter.gameObject, _positions[_busyIndex], 0.3f);
            _busyIndex++;
        }
    }
}
