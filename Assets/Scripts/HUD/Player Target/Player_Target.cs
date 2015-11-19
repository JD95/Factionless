using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using Utility;

public class Player_Target : MonoBehaviour {

    public GameObject player_target;

    public Image targetHealth;
    public Image targetMana;
    public Image targetPortrait;
    
    private Combat player;
    private Combat target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (player == null) return;

        if(player.target == null)
        {
            player_target.SetActive(false);
        }
        else
        {
            targetHealth.fillAmount = target.health / target.maxHealth;
            targetHealth.fillAmount = target.mana / target.maxMana();
        }
	}

    public void attachPlayer(GameObject c)
    {
        player = c.GetComponent<Combat>();
        player.GetComponent<Hero>().player_target = this;
    }

    public void changeTarget()
    {
        if (player.target == null) return;

        player_target.SetActive(true);

        target = player.target.GetComponent<Combat>();

        if(TeamLogic.areAllies(target.gameObject, player.gameObject))
        {
            targetHealth.color = new Color(0, 255, 0);
        }
        else
        {
            targetHealth.color = new Color(255, 0, 0);
        }

        if(target.maxMana() > 0)
        {
            targetMana.color = new Color(0, 0, 255);
        }
        else
        {
            targetMana.color = new Color(255, 255, 255);
        }
    }

}
