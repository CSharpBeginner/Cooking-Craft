using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] private Inputer _inputer;
    [SerializeField] private float _rotationSpeed;

    private Rigidbody _rigidbody;
    private Animator _animator;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Vector3 direction = RotateTo90degres(_inputer.Direction);
        Rotate(direction);
        Move(direction);
    }

    private Vector3 RotateTo90degres(Vector3 vector)
    {
        return new Vector3(vector.x, vector.z, vector.y);
    }

    private void Move(Vector3 direction)
    {
        _rigidbody.velocity = _maxSpeed * direction * _inputer.Share;
        _animator.SetFloat(CharacterAnimator.Params.Speed, _inputer.Share);
    }

    private void Rotate(Vector3 direction)
    {
        Quaternion targetQuaternion = Quaternion.FromToRotation(Vector3.forward, direction);
        float targetAngle = Mathf.Sign(direction.x) * Quaternion.Angle(Quaternion.identity, targetQuaternion);
        float angle = Mathf.MoveTowardsAngle(transform.localEulerAngles.y, targetAngle, _rotationSpeed * Time.deltaTime);
        transform.localEulerAngles = new Vector3(0f, angle, 0f);
    }
}
