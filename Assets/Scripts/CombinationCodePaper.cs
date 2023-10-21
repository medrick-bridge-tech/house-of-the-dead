using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationCodePaper : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //first should check if locker door opens or not
        if (other.tag == "Player")
        {
            Debug.Log("Player triggered");
        }
    }
}
