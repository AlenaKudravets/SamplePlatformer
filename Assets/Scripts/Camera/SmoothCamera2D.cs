using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera2D : MonoBehaviour
{
    private Transform m_target;

    [SerializeField] private float SmoothTime = 0;
    [SerializeField] private float MinimumSmoothDistance = 0.1f;

    public bool SyncX = false;
    public bool SyncY = false;
    public bool SyncZ = false;

    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        m_target = GameObject.FindGameObjectWithTag("Character").GetComponent<Transform>();
    }

    void Update ()
	{
	    SetCameraPosition();
	}

    private void SetCameraPosition()
    {
        var currentPos = transform.position;

        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(
                SyncX ? m_target.position.x : currentPos.x, 
                SyncY ? m_target.position.y : currentPos.y, 
                SyncZ ? m_target.position.z : currentPos.z),
            ref velocity, SmoothTime);
    }
}
