using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    [SerializeField] private AudioSource breakSound;
    private bool isBreaking = true;
    
    public void OnCollisionEnter(Collision other)
    {
        if (isBreaking)
        {
            isBreaking = false;
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
        isBreaking = true;
        gameObject.SetActive(false);
    }
}
