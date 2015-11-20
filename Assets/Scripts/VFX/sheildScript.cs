using UnityEngine;
using System.Collections;

public class sheildScript : MonoBehaviour {

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
		playSheild ();
	}

    void playSheild(float input = 10)
    {
        life += Time.deltaTime;
        if (life >= input)      //input is the timer for the visuals
        {
            GetComponent<ParticleSystem>().Play(); // these visuals are a second long, my solution was to loop them for the input time
        }

    }

    void stopVis()
    {
        GetComponent<ParticleSystem>().Stop();
    }
}
