using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncPosition : MonoBehaviour
{
    public Transform SyncSource;

    public bool SyncX = false;
    public bool SyncY = false;
    public bool SyncZ = false;
	
	void Update ()
	{
	    var currentPos = transform.position;

	    transform.position = new Vector3(
	        SyncX ? SyncSource.position.x : currentPos.x,
	        SyncY ? SyncSource.position.y : currentPos.y,
	        SyncZ ? SyncSource.position.z : currentPos.z);
	}
}
