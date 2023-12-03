using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Material _activeMat;
    [SerializeField] private Material _inactiveMat;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Animator _animator;
    [SerializeField] private bool _isActive = true;

    [Space]
    [Header("VFX")]
    [SerializeField] private GameObject _activeParticle;
    [SerializeField] private GameObject _dieParticle;

    private Rigidbody _rigidbody;

    public bool IsActive
    {
        get => _isActive;
        set
        {
            if (value)
            {
                _renderer.material = _activeMat;
                _animator.Play("Walk");
               
            }
            else
            {
                _renderer.material = _inactiveMat;
                _animator.Play("Idle");
            }

            _isActive = value;
        }
    }

    private void Awake()
    {
        IsActive = _isActive;
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    public void Activate()
    {
        IsActive = true;
        var vfx = Instantiate(_activeParticle, transform);
        vfx.transform.localPosition = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Character otherCharecter))
        {
            if (!otherCharecter.IsActive)
            {
                otherCharecter.Activate();
                otherCharecter.transform.parent = transform.parent;
            }
        }
    }


    [ContextMenu("Die")]
    public void Die()
    {
        transform.parent = null;
        _rigidbody.isKinematic = false;
        var vfx = Instantiate(_dieParticle, transform.position, Quaternion.identity);
        Destroy(gameObject, 2);
    }

    public void Finish()
    {
        _animator.Play("Dance");
    }
}
