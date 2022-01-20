using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private Vector3 _size;
    public Vector3 Size => _size;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, _size);
    }
}
