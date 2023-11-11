using System;
using UnityEngine;

namespace Enemy
{
    public class ViewCone : MonoBehaviour
    {
        [SerializeField] private LayerMask targetLayer;
        [SerializeField] [Range(.5f, 5f)] private float range = 1;
        [SerializeField] [Range(0f, 180f)] private float viewDegree = 135f;

        private Action<GameObject> onTargetDeteted;
        private Action onTargetLost;

        private bool isDetectingTarget;
        
        public void Setup(Action<GameObject> targetDetectedCallback, Action targetLostCallback)
        {
            onTargetDeteted += targetDetectedCallback;
            onTargetLost += targetLostCallback;
        }
        
        private void Update()
        {
            if (isDetectingTarget == false)
            {
                //TODO: Raycast multiple rays every 10 degrees
                //TODO: If found target layer
                onTargetDeteted.Invoke(null);
                isDetectingTarget = true;
            }
            else
            {
                //TODO: Raycast multiple rays
                //TODO: If lost target
                onTargetLost.Invoke();
                isDetectingTarget = false;
            }
        }
    }
}