using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SteeringBehaviours
{
    public class AIAgent : MonoBehaviour
    {
        public NavMeshAgent agent;
        public Transform target;

        void Update()
        {
            agent.SetDestination(target.position);
        }
    }
}
