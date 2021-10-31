using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject Player;
    public float SmoothCameraStop;

    static float CameraZ = -10;
	
	void FixedUpdate ()
    {
        Vector3 TargetPos = new Vector3(Player.transform.position.x, Player.transform.position.y, CameraZ);
        transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * SmoothCameraStop);
	}
}
