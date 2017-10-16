using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ClimbSystemChCtrl : MonoBehaviour {
    public CollisionFlags colFlag;

    CharacterController chCtrl;
    //PlayerMoveCC playerMove;
    byte flag;

    void Start()
    {
        //playerMove = GetComponent<PlayerMoveCC>();
        chCtrl = GetComponent<CharacterController>();
    }
    void Update()
    {
        
        if ((chCtrl.collisionFlags & CollisionFlags.Sides) != 0)
        {
            
            flag = 1;
        }
        else
            flag = 0;
        
    }
    void OnControllerColliderHit(ControllerColliderHit other) {
        if (flag == 1 & other.collider.tag == "Obstacle")
        {
            //StartCoroutine(playerMove.PlayerClimb(other.collider));
        }
            
    }
}
