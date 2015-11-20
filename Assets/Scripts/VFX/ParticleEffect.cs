using UnityEngine;
using System.Collections;

public class ParticleEffect : MonoBehaviour {

	private const float default_duration = 10;

	private float life = 0;

	void Start () {

		// Effect should be off by default
		GetComponent<ParticleSystem>().Stop();
	}

	public void playParticle(float duration = default_duration)
	{
		GetComponent<ParticleSystem>().Play();  // these visuals are a second long, my solution was to loop them for the input time
	}

	public void stopParticle()
	{
		GetComponent<ParticleSystem>().Stop();
	}
}
