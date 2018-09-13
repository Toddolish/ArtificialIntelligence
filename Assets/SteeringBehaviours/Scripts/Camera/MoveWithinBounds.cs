using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithinBounds : MonoBehaviour
{
    //Ctrl + K + D ---- clean code
    //Alt + Shift ---- type multiple lines
    public float moveSpeed = 15f;
    public float zoomSpeed = 60f;
    public CameraBounds camBounds;

    void LateUpdate()
    {
        //move camera left and right
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");
        Vector3 inputDir = new Vector3(inputH, 0, inputV);

        // position += (direction x speed) x time.deltaTime
        transform.position += inputDir * moveSpeed * Time.deltaTime;

        // zoom the camera in and out
        float inputScroll = Input.GetAxis("Mouse ScrollWheel");
        //+= appedning position
        transform.position += transform.forward * inputScroll * zoomSpeed * Time.deltaTime;

        //cap the position to stay within camera bounds
        transform.position = camBounds.GetAdjustedPosition(transform.position);
    }
}
