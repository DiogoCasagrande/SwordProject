using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Absorved : MonoBehaviour {
   
    private Rigidbody rb;
    private GameObject orbManager;
    void Awake() {
        orbManager = GameObject.FindGameObjectWithTag("OrbManager");
        transform.SetParent(orbManager.transform);
    }
    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    //metodo de absorvido
    public void Abs(Transform target) {
        float distance = Vector3.Distance(transform.position,target.position);
        if(distance > 10) { distance = 11; }
        if(distance< 0.5) { distance = 35; }
        rb.MovePosition((Vector3.Lerp(transform.position, target.position, Mathf.Abs(12 - distance) *(Time.smoothDeltaTime*0.4f))));
    }
}