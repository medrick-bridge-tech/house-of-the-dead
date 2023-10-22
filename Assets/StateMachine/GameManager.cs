using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    StateMachine _stateManager;
    // Start is called before the first frame update
    void Start()
    {
        _stateManager = new StateMachine();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
