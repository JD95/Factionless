using UnityEngine;
using System.Collections;

public class Stick_On_Target : MonoBehaviour {

	public Transform target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = target.position;
	}
}
