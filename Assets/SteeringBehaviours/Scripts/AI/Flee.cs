using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteeringBehaviours
{
    public class Flee : SteeringBehaviour
    {
        [Header("Fleeing")]
        public Transform target;

        public override Vector3 GetForce()
        {
            return base.GetForce();
        }

    }
}
