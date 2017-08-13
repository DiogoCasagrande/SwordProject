using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneTrigZPos : MonoBehaviour {

    PushStone botRef;

    public int cont;
    // Use this for initialization
    void Awake()
    {
        botRef = GetComponentInParent<PushStone>();
        cont = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (cont > 0)
            botRef.zPos = true;
        else botRef.zPos = false;
    }
    void FixedUpdate()
    {
        if (cont > 0)
            botRef.zPos = true;
        else botRef.zPos = false;
    }

    void LateUpdate()
    {
        if (cont > 0)
            botRef.zPos = true;
        else botRef.zPos = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Floor")
            cont++;
    }

    void OnTriggerStay(Collider other)
    {

    }

    void OnTriggerExit(Collider other)
    {
        if (cont > 0 && other.tag != "Floor")
            cont--;
    }
}
