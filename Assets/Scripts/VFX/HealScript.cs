using UnityEngine;
using System.Collections;

public class HealScript : MonoBehaviour {

	private float life;
	
	// Use this for initialization
	void Start () {
		GetComponent<ParticleSystem>().Stop();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void play()
	{
		playHeal ();
	}
	
	void playHeal(float input = 10)
	{
		life += Time.deltaTime;
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
