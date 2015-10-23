using UnityEngine;
using System.Collections;

public class slowScript : MonoBehaviour {

    private float life;

	// Use this for initialization
	void Start () {
        GetComponent<ParticleSystem>().Stop();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void playSlow(float input)
    {
        life += Time.deltaTime;
        input = 10;
        if (life >= input)
        {
            GetComponent<ParticleSystem>().Play(); // these visuals are a second long, my solution was to loop them for the input time
        }

    }

    void stopVis()
    {
        GetComponent<ParticleSystem>().Stop();
    }
}
