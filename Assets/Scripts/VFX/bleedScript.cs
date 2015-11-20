﻿using UnityEngine;
using System.Collections;

public class bleedScript : MonoBehaviour {

    float life = 0;
	// Use this for initialization
	void Start () {
        GetComponent<ParticleSystem>().Stop();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

	public void play()
	{
		playBleed (10);
	}

    void playBleed(float input)
    {
        life += Time.deltaTime;
        if(life >= input)
        {
            GetComponent<ParticleSystem>().Play();  // these visuals are a second long, my solution was to loop them for the input time
        }
        
    }

    void stopVis()
    {
        GetComponent<ParticleSystem>().Stop();
    }
}
