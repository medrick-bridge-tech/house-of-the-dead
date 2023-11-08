using System;
using System.Collections;
using MyStateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private ViewCone viewCone;
        [SerializeField] private NavMeshAgent agent;

        private StateMachine enemyStateMachine;
        private Transform detectedTarget;
        private State searchState, patrolState, distractState, followState;

        private void Awake()
        {
            var enemyStateMachineGraph = new Graph<State>();
            CreateState();
            CreateStateMachineGraph();
            enemyStateMachine = new StateMachine(enemyStateMachineGraph, patrolState);
            enemyStateMachine.StartMachine();
            
            viewCone.Setup(HandleTargetDetection, HandleTargetLost);

            void CreateState()
            {
                searchState = new State(EnterSearchState, () => { }, () => { });
                patrolState = new State(() => { }, () => { }, () => { });
                distractState = new State(() => { }, UpdateDistractState, () => { });
                followState = new State(() => { }, () => { }, () => { });
            }
            
            void CreateStateMachineGraph()
            {
                enemyStateMachineGraph.AddVertex(searchState);
                enemyStateMachineGraph.AddVertex(patrolState);
                enemyStateMachineGraph.AddVertex(distractState);
                enemyStateMachineGraph.AddVertex(followState);
                
                enemyStateMachineGraph.AddEdge(patrolState, followState);
                enemyStateMachineGraph.AddEdge(patrolState, distractState);
                enemyStateMachineGraph.AddEdge(followState, searchState);
                enemyStateMachineGraph.AddEdge(searchState, patrolState);
                enemyStateMachineGraph.AddEdge(searchState, followState);
                enemyStateMachineGraph.AddEdge(distractState, followState);
                enemyStateMachineGraph.AddEdge(distractState, searchState);
            }
        }

        private void HandleTargetDetection(GameObject target)
        {
            detectedTarget = target.transform;
            enemyStateMachine.Transition(followState);
        }

        private void HandleTargetLost()
        {
            detectedTarget = null;
            
            if (enemyStateMachine.CurrentState == followState)
                enemyStateMachine.Transition(searchState);
        }

        private void HandleDistraction(GameObject target)
        {
            detectedTarget = target.transform;
            agent.SetDestination(detectedTarget.position);
            enemyStateMachine.Transition(distractState);
        }

        private void Update()
        {
            enemyStateMachine.Update();
        }

        void EnterSearchState()
        {
            StartCoroutine(KeepSearching());
        }

        private IEnumerator KeepSearching()
        {
            yield return new WaitForSeconds(5);
            
            if (enemyStateMachine.CurrentState == searchState)
                enemyStateMachine.Transition(patrolState);
        }

        void UpdateDistractState()
        {
            float distance = Vector3.Distance(transform.position, detectedTarget.position);
            
            if (distance < agent.stoppingDistance)
                enemyStateMachine.Transition(searchState);
        }
    }
}