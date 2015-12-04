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
        q_cooldown.fillAmount = abilites.q_current_cooldown / abilites.q_Cooldowns[abilites.q_Level];
        q_cooldown.enabled =  q_cooldown.fillAmount != 0;

        w_cooldown.fillAmount = abilites.w_current_cooldown / abilites.w_Cooldowns[abilites.w_Level];
        w_cooldown.enabled = w_cooldown.fillAmount != 0;

        e_cooldown.fillAmount = abilites.e_current_cooldown / abilites.e_Cooldowns[abilites.e_Level];
        e_cooldown.enabled = e_cooldown.fillAmount != 0;

        r_cooldown.fillAmount = abilites.r_current_cooldown / abilites.r_Cooldowns[abilites.r_Level];
        r_cooldown.enabled = r_cooldown.fillAmount != 0;
    }
}
