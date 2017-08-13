using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopLadder : MonoBehaviour {

    PlayerMoveChCrl player;

    public Vector3 [] fPoints;
    void Start() {

        fPoints = GeneratePoints(GetComponentsInChildren<Transform>());
    }
    void Update() {
        DrawLines();
    }

    void DrawLines() {
        for (int i = 1; i < fPoints.Length-1;i++) {
            Debug.DrawLine(fPoints[i], fPoints[i + 1]);
        }
    }

    Vector3[] GeneratePoints(Transform[] trans) {
        Vector3[] vetOut = new Vector3[trans.Length];
        for (int i = 0; i< trans.Length;i++) {
            vetOut[i] = trans[i].position;
        }
        return vetOut;
    }


    Vector3[] InvertFPoints(Vector3[] vet) {
        Vector3[] invertVet = new Vector3[vet.Length];
        int j = 1;
        for (int i = vet.Length-1; i != 0;i--) {
            invertVet[j] = vet[i];
            j++;
        }
        return invertVet;
    }

    void OnTriggerStay(Collider other) {
        if (other.tag== "Player") {
            player = other.GetComponent<PlayerMoveChCrl>();
            if (player.flagClimbLadder == 0 && (Input.GetButton("Jump"))) {
                StartCoroutine(player.WaitForInteract(fPoints,0,2,null));
                player.flagClimbLadder = 1;
                player.transform.LookAt(player.transform.position+transform.right);
            }
            if(player.flagClimbLadder == 1 && Input.GetAxisRaw("Vertical") > 0)
            {
                
                StartCoroutine(player.WaitForInteract(InvertFPoints(fPoints), 0, 1.3f,null));
                player.flagClimbLadder = 0;
                player.flagMove = 0;
                player.GetComponent<Animator>().SetBool("ClimbToTop", true);
                player.GetComponent<Animator>().SetBool("LadderUp", false);
                player.GetComponent<Animator>().SetBool("LadderDown", false);
            }
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Player")
        {

        }
    }

    void OnDrawGizmos() {
        for (int i = 1; i < fPoints.Length;i++) {
            Gizmos.DrawSphere(fPoints[i],0.1f);
        }
    }
}
