using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour {
    public Transform target;

    float distanceWait;
    float smoothing;
    float distance;

    // Use this for initialization
    void Start () {
        target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        distanceWait = 20f;
        smoothing = Time.deltaTime;
        distance = Vector3.Distance(target.position, transform.position) * distanceWait;
        smoothing = Mathf.Lerp(smoothing*distance,smoothing,Time.deltaTime);
        Vector3 targetCamPos = target.position;
        transform.position = Vector3.Lerp(transform.position,targetCamPos,smoothing*Time.deltaTime*5);
	}
}
