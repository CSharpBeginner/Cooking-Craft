using UnityEngine;

public class Order : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private void Start()
    {
        transform.LookAt(2 * transform.position - _camera.transform.position);
    }
}
