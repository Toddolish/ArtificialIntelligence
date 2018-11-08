using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoHome
{
    public class PlayerController : MonoBehaviour
    {
        public Rigidbody rigid;
        public float maxVelocity = 20f;
        public float speed = 10f;


        // Contrain the velocity per update
        // Collect item on trigger enter
        private void OnTriggerEnter(Collider other)
        {
            // Try getting collectable component from other collider
            Collectable collectable = other.GetComponent<Collectable>();
            // If its not null
            if(collectable)
            {
                // Collect the item
                collectable.Collect();
            }

        }
        // Input method for movement

        public void Move(float inputH, float inputV)
        {
            // Generate direction from input (horizontal and vertical)
            Vector3 Direction = new Vector3(inputH, 0, inputV);
            // Set velocity to direction with speed
            rigid.velocity = Direction * speed;
        }
    }
}
