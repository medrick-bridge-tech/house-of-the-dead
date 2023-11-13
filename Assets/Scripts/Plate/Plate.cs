using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    [SerializeField] private AudioSource breakSound;
    private bool isBreaking;
    private Action<GameObject> onPlateDetected;
    private GameObject player;

    private void Awake()
    {
        player = transform.parent.gameObject;
    }

    public void SetUp(Action<GameObject> targetDetectedCallback)
    {
        onPlateDetected += targetDetectedCallback;
    }
    public void OnCollisionEnter(Collision other)
    {
        if (!isBreaking)
        {
            ThrowPlate();
            StartCoroutine(WaitForReset());
        }
    }

    private IEnumerator WaitForReset()
    {
        yield return new WaitForSeconds(10.0f);
        ResetPlate();
    }

    private void ThrowPlate()
    {
        isBreaking = true;
        onPlateDetected.Invoke(transform.gameObject);
        Rigidbody plateRigid = GetComponent<Rigidbody>();
        plateRigid.constraints = RigidbodyConstraints.FreezeAll;
        plateRigid.useGravity = false;
        transform.parent = null;
        breakSound.Play();
    }

    public void ResetPlate()
    {
        transform.parent = player.transform;
        transform.position = transform.parent.position;
        isBreaking = false;
        gameObject.SetActive(false);
    }
}
