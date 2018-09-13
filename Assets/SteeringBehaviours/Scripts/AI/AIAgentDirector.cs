using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SteeringBehaviours
{
    public class AIAgentDirector : MonoBehaviour
    {
        public AIAgent agent;
        public Transform placeHolderPoint;
        private void OnDrawGizmos()
        {
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(camRay.origin, camRay.origin + camRay.direction * 1000f);
        }
        void FixedUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Try to get seek component agent
                Seek seek = agent.GetComponent<Seek>();
                // if seek is not null
                if (seek)
                {
                    Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(camRay, out hit, 1000f))
                    {
                        // update the transforms position
                        placeHolderPoint.position = hit.point;
                        // update seek's target(which you might not need to do)
                        seek.target = placeHolderPoint;
                    }
                }
            }
        }
    }
}
