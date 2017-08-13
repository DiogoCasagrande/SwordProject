using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneTrigBot : MonoBehaviour {
    PushStone botRef;

    public int cont;

    void Awake()
    {
        botRef = GetComponentInParent<PushStone>();
        cont = 0;
    }

    void Update()
    {
            if (cont > 0)
                botRef.bot = true;
            else botRef.bot = false;
    }
    void FixedUpdate() {
        if (cont > 0)
            botRef.bot = true;
        else botRef.bot = false;
    }

    void LateUpdate() {
        if (cont > 0)
            botRef.bot = true;
        else botRef.bot = false;
    }

    void OnTriggerEnter(Collider other)
    {
        cont++;
    }

    void OnTriggerStay(Collider other)
    {

    }

    void OnTriggerExit()
    {
        if (cont > 0)
            cont--;
    }
}
