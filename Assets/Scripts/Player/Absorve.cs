using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Absorve : MonoBehaviour {
    public GameObject orbManager;
    public Absorved [] orbs;
    public bool ab;

    public GameObject brace;
    public Transform braceTrans;
    public Collider braceCol;

    void Awake() {
        orbManager = GameObject.FindGameObjectWithTag("OrbManager");
    }

    void Start () {
        brace = GameObject.FindGameObjectWithTag("Brace");
        braceTrans = brace.transform;
        braceCol = brace.GetComponent<Collider>();
	}
	
	void Update () {
        orbs = orbManager.GetComponentsInChildren<Absorved>();
        ab = Input.GetButton("Fire2");
        
        if (ab)
            StartCoroutine(OrbsCall());

    }
    IEnumerator OrbsCall() {
        
        foreach (Absorved call in orbs) {
            call.Abs(braceTrans);
        }

        yield return null;
    }
    void OnTriggerEnter(Collider other) {

        if (other.tag == "Orb" && ab) {
            Destroy(other.gameObject);
        }
    }
}
