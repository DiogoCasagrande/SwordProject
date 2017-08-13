using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levitate : MonoBehaviour {

    public float x;
    public float y;
    public float z;
    public float timer;
    public float cooldown;

    Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
        cooldown = Random.Range(0.6f,1.5f);
        x = Random.Range(-0.5f,0.5f);
        y = Random.Range(-0.5f,0.5f);
        z = Random.Range(-0.5f,0.5f);
    }

    void FixedUpdate(){
        rb.AddForce(x* Time.deltaTime, y * Time.deltaTime, z * Time.deltaTime);
        timer += Time.deltaTime;
        if (timer > cooldown)
        {
            timer = 0;
            x = Random.Range(-5f, 5f);
            y = Random.Range(-5f, 5f);
            z = Random.Range(-5f, 5f);
            cooldown = Random.Range(0.6f, .9f);
        }
    }
}
