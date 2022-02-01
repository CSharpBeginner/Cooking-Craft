using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class Client : MonoBehaviour
{
    [SerializeField] private List<FinalContainer> _targets;
    [SerializeField] private float _stoppingRadius;

    private static int _lastAvoidencePriority;
    private NavMeshAgent _navMeshAgent;
    private FinalContainer _target;
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
        _target = GetRandomTarget();
        _navMeshAgent.avoidancePriority = _avoidancePriority;
        Move();
    }

    private FinalContainer GetRandomTarget()
    {
        return _targets[Random.Range(0, _targets.Count)];
    }

    public void Stop()
    {
        _navMeshAgent.SetDestination(transform.position);
        _animator.SetBool(ClientAnimator.Params.IsWalk, false);
    }

    private void Move()
    {
        _navMeshAgent.SetDestination(_target.transform.position);
        _animator.SetBool(ClientAnimator.Params.IsWalk, true);
        StartCoroutine(Walk());
    }

    private IEnumerator Walk()
    {
        var waining = new WaitForFixedUpdate();

        while ((_target.transform.position - transform.position).magnitude > _stoppingRadius * _stoppingRadius)
        {
            yield return waining;
        }

        Stop();
    }
}
