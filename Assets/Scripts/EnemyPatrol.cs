using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private List<GameObject> _patrolPoint;
    [SerializeField] private List<AudioClip> _zombieAudioClip;
    
    private int destinationPointIndex = 0;
    private float _minDistanceToDestination = .5f;
    private NavMeshAgent _agent;
    private AudioSource _zombieAudioSource;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        _zombieAudioSource = GetComponent<AudioSource>();
        GoToNearestPoint();
        FunctionCaller.RepeatAudio(RandomSound, 5f);
    }

    private void Update()
    {
        if (!_agent.pathPending && _agent.remainingDistance < _minDistanceToDestination)
            GotoNextPoint();
    }

    private GameObject FindNearestPatrolPoint(List<GameObject> enemyPatrolPoints)
    {
        GameObject nearestPatrolPoint = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (GameObject potentialTarget in enemyPatrolPoints)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistance)
            {
                closestDistance = dSqrToTarget;
                nearestPatrolPoint = potentialTarget;
                destinationPointIndex = enemyPatrolPoints.IndexOf(potentialTarget);
            }
        }
        return nearestPatrolPoint;
    }

    private void GoToNearestPoint()
    {
        _agent.destination = FindNearestPatrolPoint(_patrolPoint).transform.position;
    }

    private void GotoNextPoint()
    {
        if (_patrolPoint.Count == 0)
            return;
        _agent.destination = _patrolPoint[destinationPointIndex].transform.position;
        destinationPointIndex = (destinationPointIndex + 1) % _patrolPoint.Count;
    }

    private void RandomSound()
    {
        _zombieAudioSource.clip = _zombieAudioClip[Random.Range(0, _zombieAudioClip.Count)];
        _zombieAudioSource.Play();
    }
}
