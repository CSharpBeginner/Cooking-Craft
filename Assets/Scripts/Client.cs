using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Client : MonoBehaviour
{
    [SerializeField] private List<FinalContainer> _targets;

    private static int _lastAvoidencePriority;
    private NavMeshAgent _navMeshAgent;
    private FinalContainer _target;
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
        _navMeshAgent.SetDestination(_target.transform.position);
    }

    private FinalContainer GetRandomTarget()
    {
        return _targets[Random.Range(0, _targets.Count)];
    }
}
