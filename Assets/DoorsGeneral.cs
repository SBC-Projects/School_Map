using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsGeneral : MonoBehaviour
{
    private Animator _animator = null;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        _animator.SetBool("IsOpen", true);
    }

    void OnTriggerExit(Collider collider)
    {
        _animator.SetBool("IsOpen", false);
    }
}