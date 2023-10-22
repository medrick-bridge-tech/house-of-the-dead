using UnityEngine;

public class MoveableCamera : MonoBehaviour
{
    [SerializeField] private float minZ, maxZ;
    [SerializeField] private Transform playerTransform;
    private void Update()
    {
        Vector3 playerPosition = playerTransform.position;
        
        float clampedZ = Mathf.Clamp(playerPosition.z, minZ, maxZ);
        
        transform.position = new Vector3(transform.position.x, transform.position.y, clampedZ);
    }
}    