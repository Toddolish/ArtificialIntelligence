using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris
{
    public class Spawner : MonoBehaviour
    {
        // List of groups used in the game
        public GameObject[] groups;
        // Reference to next element that spawns
        public int nextIndex = 0;

        public void SpawnNext()
        {
            /// Check if the game is over
            // Spawn next group
            Instantiate(groups[nextIndex], transform.position, Quaternion.identity);
            // Get the next index randomly
            nextIndex = Random.Range(0, groups.Length);
            // Remove any empty parents
        }
        void Start()
        {
            // Run initial spawn
            SpawnNext();
        }
    }
}
