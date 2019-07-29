using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(SmoothCamera2D))]
public class DeathCameraController : MonoBehaviour
{
    [SerializeField] private Vector3 m_cameraMovingSpeedPerSecond;
    [SerializeField] private float m_maxTargetDistance;

    private SmoothCamera2D m_smoothCamera;
    private Transform m_target;

    void Awake()
    {
       m_smoothCamera = GetComponent<SmoothCamera2D>();
       
    }
    private void Start()
    {
        m_target = GameObject.FindGameObjectWithTag("Character").GetComponent<Transform>();
    }
    void Update ()
	{
	    transform.Translate(m_cameraMovingSpeedPerSecond * Time.deltaTime);
	    m_smoothCamera.SyncX = m_target.position.x - transform.position.x > m_maxTargetDistance;
    }
}
