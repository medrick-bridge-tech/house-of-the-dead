using System;
using UnityEngine;

namespace Enemy
{
    public class ViewCone : MonoBehaviour
    {
        [SerializeField] private LayerMask targetLayer;
        [SerializeField] [Range(.5f, 5f)] private float range = 3;
        [SerializeField] [Range(0f, 180f)] private float viewDegree = 135;

        private Action<GameObject> onTargetDetected;
        private Action onTargetLost;

        private bool isTargetDetected;
        private int startAngle, lastAngle;

        private void Start()
        {
            CalculateStartAndLastAngles();
        }

        public void Setup(Action<GameObject> targetDetectedCallback, Action targetLostCallback)
        {
            onTargetDetected += targetDetectedCallback;
            onTargetLost += targetLostCallback;
        }
        
        private void Update()
        {
            if (!isTargetDetected)
                LookForTarget();
            
            else
                CheckTargetLost();
        }
        
        private void CalculateStartAndLastAngles()
        {
            startAngle = Mathf.RoundToInt((180 - viewDegree) / 2);
            lastAngle = startAngle + Mathf.RoundToInt(viewDegree) - Mathf.RoundToInt(viewDegree % 10);
        }

        private void LookForTarget()
        {
            RaycastHit hit;
            for (int rayAngle = startAngle; rayAngle <= lastAngle; rayAngle += 10)
            {
                Vector3 direction = GetDirection(rayAngle);
                Ray ray = new Ray(transform.position, direction);
                    
                if (Physics.Raycast(ray, out hit, range, targetLayer))
                {
                    onTargetDetected.Invoke(hit.transform.gameObject);
                    isTargetDetected = true;
                    break;
                }
            }
        }

        private void CheckTargetLost()
        {
            bool checkAllRay = false;
            RaycastHit hit;
            
            for (int rayAngle = startAngle; rayAngle <= lastAngle; rayAngle += 10)
            {
                Vector3 direction = GetDirection(rayAngle);
                Ray ray = new Ray(transform.position, direction);
                    
                if (Physics.Raycast(ray, out hit, range, targetLayer))
                {
                    checkAllRay = true;
                    isTargetDetected = true;
                }
                else if (rayAngle == lastAngle && checkAllRay == false)
                {
                    isTargetDetected = false;
                    onTargetLost.Invoke();
                }
            }
        }
        
        private Vector3 GetDirection(float degree)
        {
            float radianAngle = Mathf.Deg2Rad * degree;
            Vector3 direction = transform.rotation * new Vector3(Mathf.Cos(radianAngle), 0f, Mathf.Sin(radianAngle));
            return direction;
        }
    }
}