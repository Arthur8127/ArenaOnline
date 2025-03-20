using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Color color;
    [SerializeField] private Vector3 size;
    [SerializeField] private Vector3 offset;
    



    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawCube(transform.position + offset, size);
       
    }
}
