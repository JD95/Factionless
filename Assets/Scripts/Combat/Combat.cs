using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using CharacterState_Effects = Effect_Management.CharacterState_Effects;

using Utility;

/*
 * 	Handles all combat functionality. Location of Health and other combat data
 * 	like attack range and speed.
 * 
 */
public delegate void Listener();

public class Combat : MonoBehaviour {
	public Slider healthBar;
    private static System.Random random = new System.Random();

    public bool hero;
    public bool hostile;
    public bool is_ai;

	public float health;
	private float oldHealth;

	public float maxHealth;

    public float mana;
	
	public float damage = 0.0F;

	public bool selectable = true;
	public bool dead = false;

    public string targetName;
	public GameObject target;

	public bool isRanged = false;

    // Base Stats
    public double baseAttackRange;
    public double baseAttackSpeed;
    public double baseHealthRegen;
    public double baseDodgeRate;
    public double baseManaRegen;
    public double baseMaxMana;

	// Clocks
	public double basicAttackCoolDown = 0;
    private double regenClock = 0;
	
	private Character character;
    public Stats stats;

    public List<Listener> attackListeners = new List<Listener>();
    public List<Listener> healListeners = new List<Listener>();

    public List<GameObject> inRangeEnemies = new List<GameObject>();

    void onAttacked()
    {
        foreach (var listener in attackListeners) listener();
    }

    void onHealed()
    {
        foreach (var listener in healListeners) listener();
    }

	// Use this for initialization
	void Start () {
		basicAttackCoolDown = 0;
		character = GetComponent<Character>();
        stats = new Stats();
	}

	// Update is called once per frame
	void Update () {

        autoAttackCD();
        if (hero) { regen(); }

		stats.effects.stepTime();
		updateHealth();

        inRangeEnemies.RemoveAll(item => item == null);
        if (hostile && target == null) changeTarget();
        
    }

    // ----------------------------------------------------------------------------

    void autoAttackCD()
    {
        if (basicAttackCoolDown > 0)
        {
            basicAttackCoolDown -= Time.deltaTime * 1;
        }
    }

	void regen()
    {

        var _maxMana = maxMana(); // Calculate this here to avoid having to call function twice

        if (regenClock > 0) { regenClock -= Time.deltaTime * 1; return; }

        if(health < maxHealth) // Regen Health
        {
            var regen = (float)stats.effects.getChangesFor(attribute.HPReg).applyTo(baseHealthRegen);
            health += regen;
        } else if (health > maxHealth){ health = maxHealth; }

        if (mana < _maxMana) // Regen Mana
        {
            var regen = (float)stats.effects.getChangesFor(attribute.MPReg).applyTo(baseManaRegen);
            mana += regen;
        } else if (mana > _maxMana) { mana =_maxMana; }

        regenClock = 5.0;
    }

	public bool beenDamaged()
	{
		return oldHealth > health;
	}

	public void updateHealth()
	{
        health = (float)stats.effects.getChangesFor(attribute.HP).applyTo(health);

        if(health <= 0)
        {
            die();
        }

		oldHealth = health;
	}

	public float healthPercent()
	{
		return health / maxHealth;
	}
	
	public double attackSpeed()
	{
        return stats.effects.getChangesFor(attribute.AS).applyTo(baseAttackSpeed);
	}

    public double attackRange()
    {
        return stats.effects.getChangesFor(attribute.AR).applyTo(baseAttackRange);
    }

    public float attackDamage()
    {
        return (float) stats.effects.getChangesFor(attribute.AD).applyTo(damage);
    }

    public float dodgeRate()
    {
        return (float)stats.effects.getChangesFor(attribute.DO).applyTo(baseDodgeRate);
    }

    public float maxMana()
    {
        return (float)stats.effects.getChangesFor(attribute.MaxMP).applyTo(baseMaxMana);
    }

    public int level()
    {
        return stats.level;
    }

	public bool targetWithin_AttackRange()
	{
		if (target != null && transform != null)
			return Vector3.Distance(target.transform.position, transform.position) <= baseAttackRange;
		else return false;
	}

    // ----------------------------------------------------------------------------

    // Character takes physical damage
    public void recieve_Damage_Physical(float amount)
	{
        onAttacked();

        double chanceToHit   = random.NextDouble();
        double chanceToDodge = dodgeRate();

        if (chanceToHit > chanceToDodge)
        {
            recieve_Damage_Direct((float)stats.effects.getChangesFor(attribute.ARM).applyTo(amount));
        }
	}

	// Character takes magic damage
	public void recieve_Damage_Magic(float amount)
	{
        onAttacked();

        recieve_Damage_Direct((float) stats.effects.getChangesFor(attribute.MR).applyTo(amount));
	}

    public void recieve_Damage_Direct(float amount)
    {
        this.health -= amount;
        if (this.health <= 0) { die(); }
    }

	// Character is healed
	public void recieve_Healing(float amount)
	{
        recieve_Healing_Direct(amount);
    }

    public void recieve_Healing_Direct(float amount)
    {
        //GetComponent<ParticlePre>.heal
        if (health + amount <= maxHealth)
        {
            this.health += amount;
        }
        else
        {
            this.health = maxHealth;
        }

		character.particles.playEffect(Particle.Heal);
    }

	// Character causes physical damage (Auto Attack)
	public void cause_Damage_Physical(Combat _target)
	{
		_target.recieve_Damage_Physical(attackDamage());
        
        if (_target.dead) stats.killScore++;
	}

	public void autoAttack()
	{
		if(target != null /*&& targetWithin_AttackRange()*/){

			transform.LookAt(target.transform);

			if(basicAttackCoolDown <= 0)
			{
				if(isRanged)
				{
					GetComponentInChildren<Projectile_Launcher>().fire(target);
					basicAttackCoolDown = attackSpeed();
				}else{

					cause_Damage_Physical(target.GetComponent<Combat>());
				}

				basicAttackCoolDown = attackSpeed();
			}
		}

	}

	private void die()
	{
		if(/*prevent_Death()*/ false)
		{
			// do something...

		}else{

			//broadcastDeath();
			//stopMovement();
			//deathAnimation();
			if(/*reverse_Death()*/ false)
			{
				//revive();
			}
			
			//if(hero) startRespawn_Timer();

            character.setAnimation_State(character.dead_State, true);
			this.dead = true;
			this.selectable = false;


            if (!character.isBase)
            {
                GetComponent<NavMeshAgent>().enabled = false;
                GetComponent<Rigidbody>().useGravity = false;
            }

            if(hero)
            {
                var respawnFunc = CharacterState_Effects.respawnHero(gameObject);
                character.characterState.addTimedEffect(respawnFunc);
            }
            else
            {

                AI test;
                if (test = GetComponent<AI>())
                {
                    var objectives = GetComponents<AI_Objective>();
                    foreach (var objective in objectives)
                    {
                        objective.enabled = false;
                    }

                    test.enabled = false;
                }

                var removeBodyFunc = CharacterState_Effects.destroyCharacterObject(gameObject);
                character.characterState.addTimedEffect(removeBodyFunc);
            }

		}

	}


    // ----------------------------------------------------------------------------

    protected bool noCurrentTarget()
    {
        return target == null;
    }

    protected bool inList(GameObject other)
    {
        return inRangeEnemies.Contains(other);
    }

    protected void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == 8) return; // Non clickable object
                                                 //Debug.Log(other.name + " is in my range!");

        if (other.name == "AI_Collider")
            return;

        Combat test;
        if (test = other.GetComponentInParent<Combat>())
        {
            //if (other.self.Equals(gameObject.transform)) return;

            if (TeamLogic.areEnemies(this.gameObject, other.gameObject) && !inList(other.gameObject))
            {

                //Debug.Log ("Adding " + other.gameObject.name);

                if (noCurrentTarget())
                {
                    //Debug.Log ("New target Selected!");
                    target = other.gameObject; // Make new target
                }

                inRangeEnemies.Add(other.gameObject);
            }
        }
    }

    protected void OnTriggerExit(Collider other)
    {

        Combat test;

        if (test = other.gameObject.GetComponentInParent<Combat>())
        {

            // If the target goes out of range change target
            if (TeamLogic.areEnemies(this.gameObject, other.gameObject) && inList(other.gameObject))
            {
                // Deselect that enemy
                if (other.gameObject == target)
                {
                    target = null;
                }

                // Remove from in rage enemies
                inRangeEnemies.Remove(other.gameObject);
            }
        }

    }

    public void changeTarget()
    {
        if (target != null)
        {
            var enemy_selectable = target.GetComponent<Combat>().selectable;

            if (!enemy_selectable) inRangeEnemies.Remove(target);
        }

        target = inRangeEnemies.Find(x => x.GetComponent<Combat>().selectable == true);
    }
}
