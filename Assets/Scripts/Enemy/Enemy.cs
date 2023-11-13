using System;
using System.Collections;
using DG.Tweening;
using MyStateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private ViewCone viewCone;
        [SerializeField] private Plate plate;
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Animator animator;
        
        private StateMachine enemyStateMachine;
        private Transform detectedTarget;
        private GameObject plateGameObj;
        private State searchState, patrolState, distractState, followState;
        private float runSpeed = 2.2f, walkSpeed = 0.5f;

        private void Awake()
        {
            var enemyStateMachineGraph = new Graph<State>();
            CreateState();
            CreateStateMachineGraph();
            enemyStateMachine = new StateMachine(enemyStateMachineGraph, patrolState);
            enemyStateMachine.StartMachine();
            
            viewCone.Setup(HandleTargetDetection, HandleTargetLost);
            plate.SetUp(HandleDistraction);
            
            void CreateState()
            {
                searchState = new State(EnterSearchState, () => { }, () => { });
                patrolState = new State(EnterPatrolState, () => { }, ExitPatrolState);
                distractState = new State(EnterDistractState, UpdateDistractState, () => { });
                followState = new State(() => { }, UpdateFollowState, () => { });
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
            animator.SetBool("HasDetected", true);
            animator.SetBool("HasLost", false);
            detectedTarget = target.transform;
            StartCoroutine(StartFollowing());
        }

        private IEnumerator StartFollowing()
        {
            agent.speed = 0;
            yield return new WaitForSeconds(0.8f);
            enemyStateMachine.Transition(followState);
        }
        private void HandleTargetLost()
        {
            detectedTarget = null;
            animator.SetBool("HasLost", true);
            animator.SetBool("HasDetected", false);
            
            if (enemyStateMachine.CurrentState == followState)
                enemyStateMachine.Transition(searchState);
        }

        private void HandleDistraction(GameObject target)
        {
            if (enemyStateMachine.CurrentState != followState)
            {
                detectedTarget = target.transform;
                enemyStateMachine.Transition(distractState);
            }
        }

        private void Update()
        {
            enemyStateMachine.Update();
        }
        
        void EnterDistractState()
        {
            plateGameObj = new GameObject("plateObj");
            plateGameObj.transform.position = detectedTarget.position;
            agent.SetDestination(plateGameObj.transform.position);
            agent.isStopped = false;
            agent.speed = runSpeed;
        }

        void EnterSearchState()
        {
            StartCoroutine(KeepSearching());
        }

        private IEnumerator KeepSearching()
        {
            agent.speed = 0;
            
            yield return new WaitForSeconds(5);
            if (enemyStateMachine.CurrentState == searchState)
                enemyStateMachine.Transition(patrolState);
        }

        void EnterPatrolState()
        {
            agent.speed = walkSpeed;
            agent.isStopped = false;
        }
        

        void ExitPatrolState()
        {
            agent.isStopped = true;
        }

        void UpdateDistractState()
        {
            float distance = Vector3.Distance(transform.position, plateGameObj.transform.transform.position);
            
            if (distance < agent.stoppingDistance)
            {
                plate.ResetPlate();
                enemyStateMachine.Transition(searchState);
                plateGameObj = null;
            }

        }

        void UpdateFollowState()
        {
            if (detectedTarget != null)
            {
                float distance = Vector3.Distance(transform.position, detectedTarget.position);
                if (distance > agent.stoppingDistance)
                {
                    transform.LookAt(detectedTarget);
                    agent.isStopped = false;
                    agent.speed = runSpeed;
                    agent.SetDestination(detectedTarget.position);
                }
            }
        }
    }
}