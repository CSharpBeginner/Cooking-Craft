using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class Client : MonoBehaviour
{
    [SerializeField] private float _stoppingRadius;
    [SerializeField] private float _intervalOfCheck;

    private static int _lastAvoidencePriority;

    private NavMeshAgent _navMeshAgent;
    private Vector3 _target;
    private int _avoidancePriority;
    private Animator _animator;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _avoidancePriority = _lastAvoidencePriority++;
        _navMeshAgent.avoidancePriority = _avoidancePriority;
        Move();
    }

    public void SetTarget(Vector3 target)
    {
        _target = target;
    }

    private void Stop()
    {
        _navMeshAgent.SetDestination(transform.position);
        _animator.SetBool(ClientAnimator.Params.IsWalk, false);
    }

    private void Move()
    {
        _navMeshAgent.SetDestination(_target);
        _animator.SetBool(ClientAnimator.Params.IsWalk, true);
        StartCoroutine(Walk());
    }

    private IEnumerator Walk()
    {
        var waining = new WaitForSeconds(_intervalOfCheck);
        Vector3 previousPosition = _target;

        while ((previousPosition - transform.position).magnitude > _stoppingRadius * _stoppingRadius)
        {
            previousPosition = transform.position;
            yield return waining;
        }

        Stop();
    }
}
