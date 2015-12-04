using UnityEngine;
using UnityEngine.UI;

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using Utility;

public enum Champions { Chadi = 0, Cliburn = 1, Gao = 2, Shaffer = 3};

public class GameManager : Photon.MonoBehaviour
{
    public const int WAVE_INTERVAL = 30;
    private float time = 30;
    public string currentHero;

    public NetworkManager network;
    public GameObject ChampionSelect;

	// Hero Spawn Locations
	public GameObject[] redspawn;
	public GameObject[] bluespawn;

    public GameObject innerPath;
    public GameObject outterPath;

    public GameObject EndGameText;

    public Bottom_Bar bottomBar;

    public Player_Target player_target;

    public GameObject HUD;
    public GameObject champDisplay;
	
	public static bool paused = true;

	private int charNumber = 0;
	
 	private bool init = false;
	public bool gameOver = false;

    public int[] champSelect;
    public int readyPlayers = 0;

	void Start() {
		paused = true;
        champSelect = new int[8];
        //playerStats = _playerStats.GetComponent<StatsManager>();

        // Connect to server
	}
	
	public void InitGame() {
		paused = false;
		init = true;
        spawnPlayers();
        champDisplay.SetActive(false);
	}
	
    public void selectChampion(Champions selection, int playerId)
    {
        Debug.Log("Player " + playerId.ToString() + " has selected " + selection.ToString());

        bool selectionIsFree = champSelect[(int)selection] == 0;

        if (selectionIsFree)
        {
            // Clear any previous selection for this player
            for (int i = 0; i < champSelect.Length; i++)
                if (champSelect[i] == playerId) champSelect[i] = 0;

            // Put in the new selection
            champSelect[(int)selection] = playerId;
        }
    }

    public void playerIsReady()
    {
        Debug.Log("A player is ready!");
        Debug.Log("Connected Characters = " + NetworkManager.playerCount.ToString());

        if(++readyPlayers == NetworkManager.playerCount)
        {
            Debug.Log("All players are ready!");
            ChampionSelect.SetActive(false);
            HUD.SetActive(true);
            InitGame();
        }
    }

    public void spawnPlayers()
    {

        for (int i = 0; i < champSelect.Length; i++)
        {
            if (champSelect[i] != 0) { SpawnPlayer(intToName(i), champSelect[i]); }
        }

    }

    public string intToName(int selection)
    {
        switch (selection)
        {
            case 0:  return "Chadi";
            case 1:  return "Cliburn";
            case 2:  return "Gao";
            case 3:  return "Shaffer";
            default: return "Shaffer";
        }
    }

	public void SpawnPlayer (string prefabName, int playerId)
	{
        if (network.playerID == playerId)
        {
            GameObject mySpawn = bluespawn[UnityEngine.Random.Range(0, bluespawn.Length)];
            GameObject myPlayer = PhotonNetwork.Instantiate(prefabName, mySpawn.transform.position, mySpawn.transform.rotation, 0);
            myPlayer.name = "player";
            enablePlayer(myPlayer);
            //playerStats.Init();

            GameObject.Find("Main Camera").GetComponent<CameraControl>().SetTarget(myPlayer.transform);
        }
	}

	void enablePlayer(GameObject player)
	{
		// No one else can control our character except for us!
		player.GetComponent<Hero>().enabled = true;
        player.GetComponent<Abilities>().enabled = true;
		player.GetComponent<Character>().enabled = true;
		player.GetComponent<AudioSource>().enabled = true;
		player.GetComponent<NetworkCharacter>().enabled = true;

        player_target.attachPlayer(player);
        bottomBar.abilites = player.GetComponent<Abilities>();
        bottomBar.GetComponentInChildren<Sync_PlayerHealth>().combat = player.GetComponent<Combat>();
        bottomBar.GetComponentInChildren<Sync_PlayerMana>().combat = player.GetComponent<Combat>();

    }

	public void Update ()
	{
        // Display Menu
        spawnWaves();

	}

	private void spawnWaves()
	{

		if (init) {
            if (time >= WAVE_INTERVAL)
            {
                spawnCreepWave("Creep_TeamA", 3, "Creep(ranged)_TeamA", 1, bluespawn);
                spawnCreepWave("Creep_TeamB", 3, "Creep(ranged)_TeamB", 1, redspawn);
				
				time = 0;
			}
			time += Time.deltaTime;
		}
	}

    delegate void spawner(string type);
    private void spawnCreepWave(string meleeCreep, int meleeAmount, string rangedCreep, int rangedAmount, GameObject[] spawnPoints)
    {
        int waveCount =  meleeAmount + rangedAmount;

        for (int i = 0; i < waveCount; i++)
        {
            string   spawnName  = spawnPoints[i % spawnPoints.Length].name;
            Transform spawnLoc  = GameObject.Find(spawnName).transform;
            spawner  spawn      = (x) => { SpawnCreep(x, spawnLoc); };

            if (i < meleeAmount) spawn(meleeCreep); 
			else spawn(rangedCreep);
        }
    }


    List<Transform> setObjectivePath(GameObject unit)
    {
        var objectivePath = innerPath.GetComponent<LaneRoute>().objectives.ToList(); // (laneAssignment == 0) ? innerPath.objectives.ToList() : outterPath.objectives.ToList();

        if (unit.CompareTag(TeamLogic.TeamA)) objectivePath.Reverse();

        objectivePath.RemoveAt(0); // Do not travel to own nexus

        return objectivePath;
    }

    private void SpawnCreep(String prefab, Transform spawn) {

        var creep = PhotonNetwork.Instantiate(prefab, spawn.position, spawn.rotation, 0);

        creep.GetComponent<Character>().charID = charNumber++;
        creep.GetComponent<Soldier>().objectivePath = setObjectivePath(creep);
	}
	
    public void end_game(string winner)
    {
        var text = EndGameText.GetComponent<Text>();

        EndGameText.SetActive (true);
        text.text += winner;

        Time.timeScale = 0;
    }
}
