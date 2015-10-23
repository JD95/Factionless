using UnityEngine;
using System.Collections;

public class stunScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<ParticleSystem>().Stop();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void playStun(float duration)
    {
        GetComponent<ParticleSystem>().startLifetime = duration;
        GetComponent<ParticleSystem>().Play();
    }

    void stopVis()
    {
        GetComponent<ParticleSystem>().Stop();
    }
}
