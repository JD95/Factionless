using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Linq;

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
    private RuntimeAnimatorController ac;

    public int running_State = Animator.StringToHash("Running");
    public int attacking_State = Animator.StringToHash("Attacking");
    public int dead_State = Animator.StringToHash("Dead");

    private Combat combatData;
	public Transform bloodPrefab;
	public Animations currentAnimation = Animations.idle;
	public ParticlePre particles;

    public float attack_animation_time;

	public Effect_Management.CharacterState_Manager characterState;
    public Effect_Management.Graphics_Manager graphics = new Effect_Management.Graphics_Manager();

	void Awake ()
	{
		if(!isBase && !isHero){
			charID = numchars++;
			gameObject.name = "char" + charID;
		}

        anim = GetComponent<Animator>();


        combatData = GetComponent<Combat>();
        characterState = new Effect_Management.CharacterState_Manager(gameObject);
        
    }

	void Update ()
	{
		// Damage has been done to character, so make blood
		if (combatData.beenDamaged()) {
            particles.playEffect(Particle.Bleed);
			GetComponent<AudioSource>().Play();
		}

		characterState.stepTime();
        graphics.stepTime();
	}

    public void setAnimation_State(int id, bool state)
    {
        if (!isBase)
            anim.SetBool(id, state);
    }

    public void triggerAnimation_state(int id)
    {
        if (anim != null)
            anim.SetTrigger(id);
    }

    public float getAttack_Animation_Length()
    {
        return attack_animation_time;
    }

	public Vector3 getPosition()
	{
		return transform.position;
	}
}
