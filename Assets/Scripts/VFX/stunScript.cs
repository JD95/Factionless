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

	public void play()
	{
		playStun ();
	}

    void playStun(float duration = 5)
    {
        GetComponent<ParticleSystem>().startLifetime = duration;
        GetComponent<ParticleSystem>().Play();
    }

    void stopVis()
    {
        GetComponent<ParticleSystem>().Stop();
    }
}
