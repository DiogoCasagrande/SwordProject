using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveRBody : MonoBehaviour {

    public float speed;
    public Vector3 velocidade;

    float x;
    float z;

    //flags
    byte flagCanMove;
    byte flagTakeDamage;
    byte flagClimb;
    byte flagInteract;
    byte flagPush;

    Rigidbody rBody;
    Camera mainCam;
    Animator anim;

    void Awake() {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rBody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();       
    }

    void Start() {
        flagCanMove = 1;
    }

    void Update() {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate() {
        if (flagTakeDamage == 1)
            TakeDamage();
        else {
            if (flagPush == 1)
                Push();
            else if (flagInteract == 1)
                Interact();
            else if (flagClimb == 1)
                Climb();
            else if (flagCanMove == 1)
                Move(x, z);
        } 
    }

    void Move(float x, float z) {
        Vector3 des = mainCam.transform.TransformDirection(new Vector3(x, 0, z));
        des.y = 0;
        transform.LookAt(transform.position+des);
        rBody.velocity = new Vector3(des.x * speed * Time.deltaTime, rBody.velocity.y, des.z * speed * Time.deltaTime);

        velocidade = des;
        if (x != 0 || z != 0)
            anim.SetInteger("move", 1);
        else
            anim.SetInteger("move", 0);
    }

    void Climb() { }

    void TakeDamage() { }

    void Interact() { }

    void Push() { }

}
