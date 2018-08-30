using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    public Vector3 size = new Vector3(50f, 0f, 20f);
    public Vector3 GetAdjustedPosition(Vector3 incomingPos)
    {
        //Get bounds position
        Vector3 pos = transform.position;
        Vector3 halfSize = size * 0.5f;
        #region posZ
        if (incomingPos.z > pos.z + halfSize.z)
        {
            incomingPos.z = pos.z + halfSize.z;
        }
        if (incomingPos.z < pos.z - halfSize.z)
        {
            incomingPos.z = pos.z - halfSize.z;
        }
        #endregion
        #region posX
        if (incomingPos.x > pos.x + halfSize.z)
        {
            incomingPos.x = pos.x + halfSize.x;
        }
        if (incomingPos.x < pos.x - halfSize.x)
        {
            incomingPos.x = pos.x - halfSize.x;
        }
        #endregion
        #region posY
        if (incomingPos.y > pos.y + halfSize.y)
        {
            incomingPos.y = pos.y + halfSize.y;
        }
        if (incomingPos.y < pos.y - halfSize.y)
        {
            incomingPos.y = pos.y - halfSize.y;
        }
        #endregion
        return incomingPos;
    }
    void Start()
    {

    }
    
    void Update()
    {

    }
}
