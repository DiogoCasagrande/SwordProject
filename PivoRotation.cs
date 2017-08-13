using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivoRotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("1")) {
            transform.localRotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z,transform.rotation.w);
            
        }
        else if (Input.GetKeyDown("2")) {
            transform.Rotate(new Vector3(transform.rotation.x, 45, transform.rotation.z));
        }
        else if (Input.GetKeyDown("3")) {
            transform.Rotate(new Vector3(transform.rotation.x, 90, transform.rotation.z));
        }
        else if (Input.GetKeyDown("4")) {
            transform.Rotate(new Vector3(transform.rotation.x, 180, transform.rotation.z));
        }
    }
}
