using UnityEngine;
using UnityEditor;
using System.Collections;

public class Arc_Control : MonoBehaviour {

	public ParticleSystem energy_ring;
	public ParticleSystem smoke_ring;

	public float rate = 1;
	public float tracker = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if (tracker < 360) {
			SerializedObject so = new SerializedObject (energy_ring);
			so.FindProperty ("ShapeModule.arc").floatValue += rate;
			so.ApplyModifiedProperties ();

			so = new SerializedObject (smoke_ring);
			so.FindProperty ("ShapeModule.arc").floatValue += rate;
			so.ApplyModifiedProperties ();
		}
	}
}
