using UnityEngine;
using System.Collections;

public class CameraControllerScript : MonoBehaviour {

    public GameObject player;

    private Vector3 offset;
    //private Quaternion rotationOffset;

	// Use this for initialization
	void Start () {
            offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = player.transform.position + offset;
        //transform.rotation = player.transform.rotation;

        // Vertical camera tilt
        transform.rotation = player.GetComponent<PlayerController>().head.transform.rotation;
	}
}
