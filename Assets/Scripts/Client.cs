using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Client : MonoBehaviour
{
    [SerializeField] private List<Transform> _targets;

    private static int _lastAvoidencePriority;
    private NavMeshAgent _navMeshAgent;
    private Vector3 _target;
    private int _avoidancePriority;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _avoidancePriority = _lastAvoidencePriority++;
        _target = GetRandomTarget();
        _navMeshAgent.avoidancePriority = _avoidancePriority;
    }

    private Vector3 GetRandomTarget()
    {
        return _targets[Random.Range(0, _targets.Count)].position;
    }

    private void FixedUpdate()
    {
        _navMeshAgent.SetDestination(_target);
    }
}
