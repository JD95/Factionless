using UnityEngine;
using System.Collections;


public enum Animations {idle, run, attack, die, gethit};

public class Character : MonoBehaviour
{
	static int numchars;

	// Map stuff
	public int charID = 0;
	public bool isHero = false;
	public bool isBase = false;

	// Character Model
    private Animator anim;
    public int running_State = Animator.StringToHash("Running");
    public int attacking_State = Animator.StringToHash("Attacking");
    public int dead_State = Animator.StringToHash("Dead");


	public Transform bloodPrefab;
	
	public Animations currentAnimation = Animations.idle;

	void Awake ()
	{
		if(!isBase && !isHero){
			charID = numchars++;
			gameObject.name = "char" + charID;
		}
	}

	void Start()
	{
        anim = GetComponent<Animator>();
	}

	void Update ()
	{
	}

    public void setAnimation_State(int id, bool state)
    {
        anim.SetBool(id, state);
    }

	public Vector3 getPosition()
	{
		return transform.position;
	}
}
