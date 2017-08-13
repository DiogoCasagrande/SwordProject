using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PushStone : MonoBehaviour {
    public Transform[] ls;
    public float speed;
    public float grav;
    public bool flag;

    public Rigidbody ctrl;
    public Vector3 des;

    public bool bot;
    public bool xPos;
    public bool xNeg;
    public bool zPos;
    public bool zNeg;

    void Awake() {
        ls = gameObject.GetComponentsInChildren<Transform>();
        ctrl = GetComponent<Rigidbody>();
    }
    void Update() {
        Debug.DrawLine(ls[1].position, ls[2].position);
        Debug.DrawLine(ls[2].position, ls[3].position);
        Debug.DrawLine(ls[3].position, ls[4].position);
        Debug.DrawLine(ls[4].position, ls[1].position);
        if (!bot)
            grav = 3f;
        else
            grav = 0;
    }

    void FixedUpdate() {
        if (!bot)
            grav = 3f;
        else
            grav = 0;
        /*ctrl.MovePosition(new Vector3(transform.position.x,
            transform.position.y-grav*Time.deltaTime,
            transform.position.z));*/

        ctrl.MovePosition(new Vector3(transform.position.x + ((des.x * speed) * Time.deltaTime),
        transform.position.y - grav * Time.deltaTime,
        transform.position.z + ((des.z * speed )* Time.deltaTime)));
    }
    public void Pushing(Vector3 normal) {   
        if (Mathf.Abs(normal.x) != 1)
            normal.x = 0;
        if (Mathf.Abs(normal.z) != 1)
            normal.z = 0;

        //validações dos lados X's
        if (normal.x > 0) {
            if (xPos) {
                normal.x = 0;
            }
        }
        else if (normal.x < 0) {
            if (xNeg) {
                normal.x = 0;
            }
        }

        //validações dos lados Z's
        if (normal.z > 0) {
            if (zPos) {
                normal.z = 0;
            }
        }
        else if (normal.z < 0) {
            if (zNeg) {
                normal.z = 0;
            }
        }

        des = normal;
    }

    void OnDrawGizmos() {
        for (int i = 1; i < ls.Length; i++)
        {
            Gizmos.DrawSphere(ls[i].position,0.1f);
        }
    }
}
