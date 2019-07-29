using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftDeathTriggerPosition : MonoBehaviour
{
    private Camera m_targetCamera;

    void Awake ()
    {
        m_targetCamera = Camera.main;
        var leftCameraPos = m_targetCamera.ScreenToWorldPoint(Vector3.zero);
        var objectWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        var newPosX = leftCameraPos.x + objectWidth / 2f;
        transform.position = new Vector3(newPosX, transform.position.y, transform.position.z);
    }
	
}
