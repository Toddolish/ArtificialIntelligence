using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris
{
    public class Group : MonoBehaviour
    {

        #region Variables
        public float fallInterval = 1.0f;
        public float holdDuration = 1.0f;
        public float fastInterval = .25f;

        private float holdTimer = 0f;
        private float fallTimer = 0f;
        private bool isFallingFaster = false;
        private bool isSpacePressed = false;
        private Spawner spawner;
        #endregion

        bool IsBlockOwner(int x, int y)
        {
            Grid grid = Grid.Instance;
            // If grid element exists at index AND
            // It belongs to this current group
            return grid.data[x, y] != null &&
                   grid.data[x, y].parent != transform;
        }

        // Detect if group's position is valid in relation to Grid
        bool IsValidGridPos()
        {
            // Get grid instance to variable
            Grid grid = Grid.Instance;

            // Loop through all children in group
            foreach (Transform child in transform)
            {
                // Round the child's position
                Vector2 v = grid.RoundVec2(child.position);
                // Not inside border?
                if (!grid.InsideBorder(v))
                    return false;

                // Truncate position
                int x = (int)v.x;
                int y = (int)v.y;
                // If cell is NOT empty AND not part of same group
                if (IsBlockOwner(x, y))
                    return false;
            }

            // All other cases return true! Which means it's a valid position
            return true;
        }
        // Remove all elements in grid (set them to null) and re-add the new positions
        void UpdateGrid()
        {
            // Get instance of grid to variable
            Grid grid = Grid.Instance;

            // Remove old children from grid
            for (int x = 0; x < grid.width; x++)
            {
                for (int y = 0; y < grid.height; y++)
                {
                    if (IsBlockOwner(x, y))
                    {
                        // Remove it from grid
                        grid.data[x, y] = null;
                    }
                }
            }

            // Add new children positions to grid
            foreach (Transform child in transform)
            {
                // Round the child's position
                Vector2 v = grid.RoundVec2(child.position);
                // Truncate position
                int x = (int)v.x;
                int y = (int)v.y;
                // Set the coordinate to child
                grid.data[x, y] = child;
                child.position = v;
            }
        }
        void MoveLeftOrRight()
        {
            // Direction to move
            Vector3 moveDir = Vector3.zero;
            // Going left?
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                // Move left
                moveDir = Vector3.left;
            }
            // Going right?
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                // Move right
                moveDir = Vector3.right;
            }
            // Is there movement?
            if (moveDir.magnitude > 0)
            {
                // Move the group in that direction
                transform.position += moveDir;
                // See if valid
                if (IsValidGridPos())
                {
                    // It's valid, update the grid
                    UpdateGrid();
                }
                else
                {
                    // It's NOT valid, revert
                    transform.position += -moveDir;
                }
            }
        }
        void MoveUpOrDown()
        {
            // Up arrow pressed?
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                // Rotate around 90 degrees
                transform.Rotate(0, 0, 90);
                // See if valid
                if (IsValidGridPos())
                {
                    // It's valid, update grid
                    UpdateGrid();
                }
                // otherwise
                else
                {
                    // It's NOT valid, revert
                    transform.Rotate(0, 0, -90);
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                // Modify position
                transform.position += Vector3.down;
                // See if valid
                if (IsValidGridPos())
                {
                    // It's valid, update grid
                    UpdateGrid();
                }
                else
                {
                    // It's NOT valid, revert
                    transform.position += Vector3.up;
                }

                // Increase hold timer
                holdTimer += Time.deltaTime;
                // If hold timer reaches duration
                if (holdTimer >= holdDuration)
                {
                    isFallingFaster = true;
                }
            }

            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                isFallingFaster = false;
                holdTimer = 0;
            }
        }
        void DetectFullRow()
        {
            // Clear any rows that are filled
            Grid.Instance.DeleteFullRows();
            // Spawn the next group
            spawner.SpawnNext();
            // Disable script
            enabled = false;
        }
        void Fall()
        {
            // Modify position
            transform.position += Vector3.down;
            // See if valid
            if (IsValidGridPos())
            {
                // It's valid, update grid
                UpdateGrid();
            }
            else
            {
                // It's NOT valid, revert.
                transform.position += Vector3.up;
                // Detect full row
                DetectFullRow();
            }
        }
        // Use this for initialization
        void Start()
        {
            // Find the current spawner in the scene
            spawner = FindObjectOfType<Spawner>();
            // Check if null
            if (spawner == null)
            {
                // Display error
                Debug.LogError("Spawner does not exist in the current scene!");
            }
        }

        // Update is called once per frame
        void Update()
        {
            // Check if space is pressed
            //isSpacePressed = Input.GetKeyDown(KeyCode.Space);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isSpacePressed = true;
            }

            // If space is pressed
            if (isSpacePressed)
            {
                // Get the group to fall straight away
                Fall();
            }
            else  // Otherwise
            {
                // Move Left, Right, Up or Down
                MoveLeftOrRight();
                MoveUpOrDown();

                // Modify speed based on bool (isFallingFaster)
                //float currentInterval = fallInterval;
                //if (isFallingFaster)
                //{
                //    currentInterval = fastInterval;
                //}
                // Ternary Operators
                float currentInterval = isFallingFaster ? fastInterval : fallInterval;



                // Increase fall timer
                fallTimer += Time.deltaTime;

                // If the timer reaches the currentInterval
                if (fallTimer >= currentInterval)
                {
                    // Get group to fall
                    Fall();
                    // Reset timer
                    fallTimer = 0;
                }
            }
        }
    }
}
