using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SteeringBehaviours
{
    public class AIAgent : MonoBehaviour
    {
        public NavMeshAgent agent;
        private Vector3 point;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (point.magnitude > 0)
                {
                    agent.SetDestination(point);
                }
            }
        }

        public void SetTarget(Vector3 point)
        {
            this.point = point;
        }
    }
}
