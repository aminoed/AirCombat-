using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlaneDirect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
    {
		if(other.name.Contains("M1TrainingSession") || other.name.Contains("M2TrainingSession"))
        {
			Vector3[] randomDirect = {new Vector3(1, 0, -1), new Vector3(-1, 0f, -1)};

			int r = Random.Range(0, randomDirect.Length);
			other.GetComponent<Rigidbody>().velocity = randomDirect[r] * 5;
			other.transform.LookAt(randomDirect[r]);
		}
		
	}
}
