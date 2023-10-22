using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    StateManager _state;
    // Start is called before the first frame update
    void Start()
    {
        _state = new StateManager();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
