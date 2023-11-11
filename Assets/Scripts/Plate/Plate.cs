using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    [SerializeField] private AudioSource breakSound;
    private bool _breakSoundCooldown = true;
    public void OnCollisionEnter(Collision other)
    {
        if (_breakSoundCooldown)
        {
            _breakSoundCooldown = false;
            StartCoroutine(ResetPlateDelay());
        }
    }

    IEnumerator ResetPlateDelay()
    {
        breakSound.Play();
        yield return new WaitForSeconds(2.0f);
        Rigidbody plateRigid = GetComponent<Rigidbody>();
        plateRigid.constraints = RigidbodyConstraints.FreezeAll;
        plateRigid.useGravity = false;
        transform.position = transform.parent.position;
        _breakSoundCooldown = true;
        gameObject.SetActive(false);
    }
}
