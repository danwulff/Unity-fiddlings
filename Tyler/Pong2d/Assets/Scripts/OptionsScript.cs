using UnityEngine;
using System.Collections;

public class OptionsScript : MonoBehaviour {

    public static bool isVSAI = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Toggle_VSAI(bool value)
    {
        isVSAI = value;
    }
}
