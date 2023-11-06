using MyStateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private ViewCone _viewCone;
        [SerializeField] private NavMeshAgent _agent;

        private StateMachine stateMachine;
        private Transform detectedTarget;
        private State searchState, patrolState, distractState, followState;
        
        private void Awake()
        {
            var enemyStateMachineGraph = new Graph<State>();
            CreateState();
            CreateStateMachineGraph();
            stateMachine = new StateMachine(enemyStateMachineGraph, patrolState);
            
            _viewCone.Setup(HandleTargetDetection, HandleTargetLost);

            void CreateState()
            {
                searchState = new State(EnterSearchState, () => { }, () => { });
                patrolState = new State(EnterPatrolState, () => { }, () => { });
                distractState = new State(EnterDistractState, () => { }, () => { });
                followState = new State(EnterFollowState, UpdateFollowState, () => { });
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
                enemyStateMachineGraph.AddEdge(distractState, patrolState);
            }
        }

        private void HandleTargetDetection(GameObject target)
        {
            detectedTarget = target.transform;
            // TODO: Set agent target to target
        }

        private void HandleTargetLost()
        {
            detectedTarget = null;
            // TODO: Move to search state
        }
        
        void EnterPatrolState()
        {
            
        }
        
        void EnterSearchState()
        {
            
        }
        
        void EnterDistractState()
        {
            
        }
        
        void EnterFollowState()
        {
            
        }
        
        void UpdateFollowState()
        {
            
        }
        
        void UpdatePatrolState()
        {
            
        }
        
        void UpdateSearchState()
        {
            
        }

        void ExitPatrolState()
        {
            
        }
        
        void ExitDistractState()
        {
            
        }
        
        void ExitSearchState()
        {
            
        }
    }
}