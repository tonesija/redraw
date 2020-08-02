using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerScript : MonoBehaviour
{
    public static LevelManagerScript Instance {get; private set;}
    public GameObject playerPrefab;

    List<GameObject> players;
    Vector2 spawnLoc;


    void Awake()
    {
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
        
    }

    void Start(){
        NextLevel();
    }

    //should be called after the player touches the LevelEnd game object
    void NextLevel(){

        spawnLoc = GameObject.Find("PlayerSpawn").transform.position;

        PlayerController.OnPlayerRewind += OnPlayerRewind;
        PlayerController.OnPlayerFinish += OnPlayerReachLevelFinish;

        players = new List<GameObject>();
        players.Add(Instantiate(playerPrefab, spawnLoc, Quaternion.identity));
    }


    //triggers when player presses the rewind button
    void OnPlayerRewind(){
        GameObject newPlayer = Instantiate(playerPrefab, spawnLoc, Quaternion.identity);
        players.Add(newPlayer);

        StartCoroutine("SpawnPlayers");
    }

    //triggers when player touches the level end game object
    void OnPlayerReachLevelFinish(){
        print("LEVEL FINISHED");
    }


    //spawns all players periodically on the spawnLoc
    IEnumerator SpawnPlayers(){
        foreach(GameObject player in players){
            player.SetActive(false);
        }
        foreach(GameObject player in players){
            player.SetActive(true);
            player.GetComponent<PlayerMovement>().Reset();
            player.transform.position = spawnLoc;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
