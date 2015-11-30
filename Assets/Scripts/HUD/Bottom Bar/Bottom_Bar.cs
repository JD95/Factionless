using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Bottom_Bar : MonoBehaviour {

    public Abilities abilites;

    public Image q_cooldown;
    public Image w_cooldown;
    public Image e_cooldown;
    public Image r_cooldown;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        q_cooldown.fillAmount = abilites.q_current_cooldown / abilites.w_Cooldowns[abilites.q_Level];
        w_cooldown.fillAmount = abilites.w_current_cooldown / abilites.w_Cooldowns[abilites.w_Level];
        e_cooldown.fillAmount = abilites.e_current_cooldown / abilites.e_Cooldowns[abilites.e_Level];
        r_cooldown.fillAmount = abilites.r_current_cooldown / abilites.r_Cooldowns[abilites.r_Level];
    }
}
