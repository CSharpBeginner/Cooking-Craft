using UnityEngine;

public class SquareOfSpawn : MonoBehaviour
{
    [SerializeField] private Vector3 _size;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, _size);
    }

    public Vector3 GetRandomPosition()
    {
        Vector3 result = transform.position + new Vector3(Random.Range(-_size.x / 2, -_size.x / 2), 0, Random.Range(-_size.z / 2, -_size.z / 2));
        return result;
    }
}
