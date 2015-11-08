using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayHealthBar : MonoBehaviour {

    private Combat player_combatData;
    private Image health;

    void Start()
    {
        player_combatData = GetComponentInParent<Combat>();
        health = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
		health.fillAmount = (float) player_combatData.health / player_combatData.maxHealth;
    }
}
