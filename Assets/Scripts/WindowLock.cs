using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowLock : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player triggered");
        }
    }
}
