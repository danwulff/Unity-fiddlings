using UnityEngine;
using System.Collections;

public class BillboardScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// ALWAYS point towards main camera
	void Update () {
        if (Camera.main != null)
        {
            transform.LookAt(Camera.main.transform);
        }
	}
}
