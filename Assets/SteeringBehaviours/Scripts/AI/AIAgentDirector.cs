﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteeringBehaviours
{
    public class AIAgentDirector : MonoBehaviour
    {
       public AIAgent agent;

        void Start()
        {
        }

        void FixedUpdate()
        {
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if(Physics.Raycast(camRay, out hit, 1000f))
            {
                agent.SetTarget(hit.point);
            }
        }
    }
}
