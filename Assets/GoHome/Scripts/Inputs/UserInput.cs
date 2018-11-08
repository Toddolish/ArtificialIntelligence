using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoHome
{
    public class UserInput : MonoBehaviour
    {
        public PlayerController player;
        void Update()
        {
            // Gather input from keyboard
            float inputH = Input.GetAxis("Horizontal");
            float inputV = Input.GetAxis("Vertical");
            // Tell player to move forward
            player.Move(inputH, inputV);
        }
    }
}
