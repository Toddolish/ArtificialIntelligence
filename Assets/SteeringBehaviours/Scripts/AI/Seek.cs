using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteeringBehaviours
{
    public class Seek : SteeringBehaviour
    {
        [Header("Seeking")]
        public Transform target;
        public float stoppingDistance;

        public override Vector3 GetForce()
        {
            // Get direction (velocity) to target
            Vector3 direction = target.position - owner.transform.position;
            // Normalize Velocity (remove the magnitute part of vector)
            direction.Normalize();
            return direction * owner.maxSpeed; // return velocity (direction)
        }
    }
}
