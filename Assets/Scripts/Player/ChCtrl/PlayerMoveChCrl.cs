using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]
public class PlayerMoveChCrl : MonoBehaviour
{
    public int lixo;
    //CharacterControler 
    //idle Rad 0.26
    //Push Rad 1
    CharacterController chCtrl;

    public float speed;
    public float jumpSpeed;
    public float fallingSpeed;
    public float ladderSpeed;

    public float pushPower;

    float x;
    float z;

    //flags
    public byte flagTakeDamage;
    public byte flagClimbLadder;
    public byte flagClimb;
    public byte flagPush;
    public byte flagInteract;
    public byte flagMove;


    //timers
    public float timerJump;

    //maxTimers
    public float maxTimerJump;

    //Components
    Camera mainCam;
    Animator anim;

    //Interact
    public float interactSpeed;
    public byte startInteract;

    //aux
    public float grav;

    //Climb Aux
    public byte climb;
    public byte unableToClimb;

    public bool top1;
    public bool top2;

    public bool mid1;
    public bool mid2;

    //Push Aux
    ControllerColliderHit stoneHit;
    PushStone stone;

    //Ladder Aux
    public int currentPointLadder = 1;
    public float reachDistance;
    public float timeStepLadder;
    public Vector3 [] wayStep;

    //check's
        //Ground
    public Transform [] groundCheckObj;
    public bool groundCheck;
    public bool isGroun;
        //Climb
    public Transform[] checkClimbTopObj;
    public bool checkClimb;
    RaycastHit hitClimb;


    //bool testes
    void Awake() {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        chCtrl = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        groundCheckObj = GameObject.Find("GroundCheck").GetComponentsInChildren<Transform>();
        checkClimbTopObj = GameObject.Find("Climb").GetComponentsInChildren<Transform>();
    }

    void Start() {
        flagMove = 1;
        //grav = 40;
        timerJump = 0;
    }

    void Update() {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");
        CheckGround();
        CheckClimb();

        if (!groundCheck)
            timerJump += Time.deltaTime;
        if (flagClimbLadder == 0)
            anim.speed = 1;
        if (x != 0 || z != 0)
            anim.SetInteger("move", 1);
        else {
            anim.SetInteger("move", 0);
        }
    }

    void FixedUpdate() {

        if (flagTakeDamage == 1)
            TakeDamage();
        else {
            if (flagInteract == 1)
                Interact();
            else if (flagClimb == 1)
                Climb();
            else if (flagPush == 1)
                Push();
            else if (flagClimbLadder == 1)
                ClimbLadder();
            else if (flagMove == 1)
                Move(x, z);
        }
    }

    public IEnumerator WaitForInteract(Vector3[] way,float time, float inSpeed,string animVar) {
        flagInteract = 1;
        wayStep = way;
        interactSpeed = inSpeed;
        yield return new WaitForSeconds(time);
        if(animVar!=null)
            anim.SetBool(animVar, false);
        startInteract = 1;
    }

    void TakeDamage() { }

    void ClimbLadder() {

        if ((z > 0))
        {
            ladderSpeed += Time.deltaTime;
            if (ladderSpeed > timeStepLadder)
                ladderSpeed = 0;
            anim.SetBool("LadderUp", true);
            anim.SetBool("LadderDown", false);
            anim.speed = 1;
            chCtrl.Move(transform.up * z * ladderSpeed * Time.deltaTime);
            
        }
        else if (z < 0)
        {
            ladderSpeed += Time.deltaTime;
            if (ladderSpeed > timeStepLadder)
                ladderSpeed = 0;
            anim.SetBool("LadderUp", false);
            anim.SetBool("LadderDown", true);
            anim.speed = 1;
            chCtrl.Move(transform.up * z * ladderSpeed * Time.deltaTime);
        }
        else if (!isGroun&& (z == 0))
        {
            anim.speed = 0;
        }

        if (Input.GetButton("Fire1")||(((chCtrl.collisionFlags & CollisionFlags.Below) != 0) && z < 0))
        {
            flagClimbLadder = 0;
            flagMove = 1;
            anim.SetBool("LadderUp", false);
            anim.SetBool("LadderDown", false);
            anim.speed = 1;
        }
    }

    void Climb() {
        unableToClimb = 0;
        float distance;
        if (groundCheck)
            distance = hitClimb.point.y - transform.position.y;
        else
             distance = 2;
        anim.SetFloat("GrabDistance", distance);
        anim.SetBool("GrabToClimb", true);

        if (climb == 1 && hitClimb.transform != null&&(distance > 1.8))
        {
            anim.SetBool("Falling", false);
            anim.SetBool("GrabToClimb", true);
            transform.LookAt(new Vector3(hitClimb.point.x,transform.position.y,hitClimb.point.z));
            Vector3[] way = new Vector3[3];
            way[1] = transform.position;
            way[2] = new Vector3(transform.position.x, hitClimb.point.y - (chCtrl.height), transform.position.z);
            if(groundCheck)
                StartCoroutine(WaitForInteract(way,0.8f,2.5f,null));
            else {
                StartCoroutine(WaitForInteract(way, 0, 4.5f,null));
            }
            climb = 0;
        }
        else if(climb == 0 || !groundCheck) {
            if (Input.GetButtonDown("Jump")) {
                Vector3[] way = new Vector3[3];
                way[1] = transform.position;
                way[2] = hitClimb.point;
                StartCoroutine(WaitForInteract(way, 0, 2f,"GrabToClimb"));
                anim.SetBool("Falling", false);
                climb = 1;
                flagClimb = 0;
            }
            else if (Input.GetButtonDown("Fire1")) {
                anim.SetBool("Falling", true);
                anim.SetBool("GrabToClimb", false);
                climb = 0;
                flagClimb = 0;
            }
        }
        if (groundCheck&&climb == 1 && hitClimb.transform != null && (distance < 1.8)) {
            anim.SetBool("Falling", false);
            
            transform.LookAt(new Vector3(hitClimb.point.x, transform.position.y, hitClimb.point.z));
            Vector3[] way = new Vector3[3];
            way[1] = transform.position;
            way[2] = hitClimb.point;
            StartCoroutine(WaitForInteract(way, 0, 2f, "GrabToClimb"));
            flagClimb = 0;
        }
    }

    void Push() {
        if ((stoneHit != null && (x == -stoneHit.normal.x || z == -stoneHit.normal.z)&&(x != 0 || z!=0))) {
            //nao esquercer de comentar amanha
            //chCtrl.radius = 1;
            anim.SetBool("Push", true);
            stone.Pushing(-stoneHit.normal);
            transform.LookAt(transform.position + (-stoneHit.normal));
            chCtrl.SimpleMove(transform.forward * speed );
        }
        else {
            stone.Pushing(Vector3.zero);
            anim.SetBool("Push", false);
            flagPush = 0;
            flagMove = 1;
            stoneHit = null;
        }
    }

    void Interact() {
        //Ladder top
        flagMove = 0;
        float distance = Vector3.Distance(wayStep[currentPointLadder], transform.position);
        if (startInteract == 1) { 
            transform.position = Vector3.MoveTowards(transform.position,wayStep[currentPointLadder],Time.deltaTime*interactSpeed);
        }
        if (distance <= reachDistance) {
            currentPointLadder++;
        }
        if (currentPointLadder == wayStep.Length) {
            startInteract = 0;
            flagInteract = 0;
            currentPointLadder = 1;
            flagMove = 1;
            
            //zerar todos as animações de interação
            anim.SetBool("ClimbToTop",false);
            
        }
    }


    void Move(float x, float z)
    {
        Vector3 des;
        Vector3 moveDirection = Vector3.zero;
        
        if (groundCheck)
        {
            if (checkClimb)
            {
                flagClimb = 1;
            }
            //unables
            unableToClimb = 1;
            climb = 1;

            timerJump = 0;
            fallingSpeed = 0;
            //anim
            anim.SetBool("Jump", false);
            anim.SetBool("Falling", chCtrl.velocity.y>0);

            if (x != 0 || z != 0)
            {
                des = mainCam.transform.TransformDirection(new Vector3(x, 0, z));
                des.y = 0;
                des = transform.position + des;
                transform.LookAt(des);

                moveDirection = transform.TransformDirection(Vector3.forward * speed);
                
            }
        }
        else
        {
            anim.SetBool("Falling", true);
            if (checkClimb)
            {
                flagClimb = 1;
            }
            if (timerJump < maxTimerJump)
            {
                //anim
                anim.SetBool("Jump", true);

                des = mainCam.transform.TransformDirection(new Vector3(x, 0, z));
                des.y = 0;
                des = transform.position + des;
                //transform.LookAt(des);
                moveDirection = transform.TransformDirection(Vector3.forward * speed);
                moveDirection.y += jumpSpeed;
            }
            else {
                anim.SetBool("Jump", false);
                des = mainCam.transform.TransformDirection(new Vector3(x, 0, z));
                des.y = 0;
                des = transform.position + des;
                //transform.LookAt(des);
                fallingSpeed += Time.deltaTime*1.5f;
                moveDirection = transform.TransformDirection(Vector3.forward * (speed-fallingSpeed));
            }
        }
        moveDirection.y -= grav*Time.smoothDeltaTime;
        chCtrl.Move(moveDirection*Time.deltaTime);
    }

    void CheckGround() {

        if (Physics.Linecast(groundCheckObj[0].position, groundCheckObj[1].position) |
            Physics.Linecast(groundCheckObj[0].position, groundCheckObj[2].position) |
            Physics.Linecast(groundCheckObj[0].position, groundCheckObj[3].position) |
            Physics.Linecast(groundCheckObj[0].position, groundCheckObj[4].position))
        {
            groundCheck = true;
        }
        else
            groundCheck = false;

        isGroun = chCtrl.isGrounded;
    }
    void CheckClimb() {
        Debug.DrawLine(checkClimbTopObj[1].position, checkClimbTopObj[2].position);

        if (Input.GetButtonDown("Jump") && groundCheck) {
            checkClimb = (!Physics.Linecast(checkClimbTopObj[3].position, checkClimbTopObj[1].position, 9) &&
            Physics.Linecast(checkClimbTopObj[1].position, checkClimbTopObj[2].position, out hitClimb, 9) &&
            !Physics.Linecast(checkClimbTopObj[3].position, checkClimbTopObj[4].position, 9) && unableToClimb == 1);
            print((hitClimb.point-transform.position));
        }
        else if (!groundCheck) {
            checkClimb = (!Physics.Linecast(checkClimbTopObj[3].position, checkClimbTopObj[1].position, 9) &&
            Physics.Linecast(checkClimbTopObj[1].position, checkClimbTopObj[2].position, out hitClimb, 9) &&
            !Physics.Linecast(checkClimbTopObj[3].position, checkClimbTopObj[4].position, 9) && unableToClimb == 1);
        }
    }

    void OnDrawGizmos() {
        //groundcheck
        foreach(Transform obj in groundCheckObj)
            Gizmos.DrawSphere(obj.position, 0.05f);
        //ClimbChecks
        //Top
       
        for (int i = 1; i < checkClimbTopObj.Length;i++) { 
            Gizmos.DrawSphere(checkClimbTopObj[i].position, 0.05f);
        }

    }

    void OnControllerColliderHit(ControllerColliderHit other) {
        if (other.gameObject.tag == "PushObject")
        {
            stone = other.gameObject.GetComponent<PushStone>();
            stoneHit = other;
            flagMove = 0;
            flagPush = 1;
        }
        else if (other.gameObject.tag == "Ladder" && z != 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, other.transform.position.z);
            transform.LookAt(transform.position + other.transform.right);
            flagClimbLadder = 1;
        }
    }
}