using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagerScript : MonoBehaviour
{
    public static LevelManagerScript Instance {get; private set;}
    public GameObject playerPrefab;

    public GameObject eraserPrefab;

    public GameObject endLevelUIPrefab;

    public int[] requiredScores;

    List<GameObject> players;
    Vector2 spawnLoc;

    int level;

    int rewinds;

    bool subscribedToPlayer;

    AsyncOperation asyncLoadLevel;



    void Awake()
    {
        if(Instance == null){
            Instance = this;
            InitializeSingleton();
            DontDestroyOnLoad(gameObject);
            PlayerPrefs.DeleteAll();
        } else {
            Destroy(gameObject);
        }
        
    }

    //is called when the game starts
    //setups this singleton
    void InitializeSingleton(){

        if(!subscribedToPlayer){
            PlayerController.OnPlayerRewind += OnPlayerRewind;
            PlayerController.OnPlayerFinish += OnPlayerReachLevelFinish;
            subscribedToPlayer = true;
        }
    }

    //should be called after the level has been loaded
    //finds the spawnLocation, subscribes to players listeners and spawns the first player
    void NextLevel(){
        spawnLoc = GameObject.Find("PlayerSpawn").transform.position;
        rewinds = 0;

        players = new List<GameObject>();
        players.Add(Instantiate(playerPrefab, spawnLoc, Quaternion.identity));
    }


    //triggers when player presses the rewind button
    void OnPlayerRewind(){
        StopCoroutine("SpawnPlayers");
        rewinds++;

        foreach(GameObject player in players){  //spawn erasers
            Instantiate(eraserPrefab, player.transform.position, Quaternion.identity);
        }
        
        GameObject newPlayer = Instantiate(playerPrefab, spawnLoc, Quaternion.identity);
        players.Add(newPlayer);

        StartCoroutine("SpawnPlayers");
    }

    //triggers when player touches the level end game object
    void OnPlayerReachLevelFinish(){

        EndLevelUIScript uiScript = ShowUI();
        uiScript.SetScore(GetScore(requiredScores[level - 1], rewinds));
        SaveToPlayerPrefsAndShowHighscore(uiScript);
    }

    EndLevelUIScript ShowUI(){
        return Instantiate(endLevelUIPrefab).GetComponent<EndLevelUIScript>();
    }

    void SaveToPlayerPrefsAndShowHighscore(EndLevelUIScript script){
        int previousScore = PlayerPrefs.GetInt(level.ToString(), 0);

        if(previousScore < GetScore(requiredScores[level - 1], rewinds)){
            PlayerPrefs.SetInt(level + "", GetScore(requiredScores[level - 1], rewinds));
            script.ShowNewHighScore();
        }
    }

    //load a level and call NextLevel
    IEnumerator LoadLevelAsync(int level){
        asyncLoadLevel = SceneManager.LoadSceneAsync("Level"+ level);
        while(!asyncLoadLevel.isDone) yield return null;
        NextLevel();
    }
    public void LoadNextLevel(){
        StartCoroutine("LoadLevelAsync", ++level);
    }

    public void LoadLevel(int level){
        this.level = level;
        StartCoroutine("LoadLevelAsync", level);
    }

    public void LoadMainMenu(){
        SceneManager.LoadScene("MainMenuScene");
    }

    //spawns all players periodically on the spawnLoc
    IEnumerator SpawnPlayers(){
        foreach(GameObject player in players){  
            player.SetActive(false);    //deactivate all players
        }

        yield return new WaitForSeconds(eraserPrefab.GetComponent<EraserScript>().duration); //wait for the duration of the erase animation
        TimerScript.Instance.SetTime(0f); //reset the movement clock

        foreach(GameObject player in players){  //periodically spawn all players
            player.SetActive(true);
            player.GetComponent<PlayerMovement>().Reset();
            player.transform.position = spawnLoc;
            yield return new WaitForSeconds(0.5f);
        }
    }

    int GetScore(int req, int rewinds){
        if(rewinds <= req) return 3;

        if(rewinds - 1 == req) return 2;

        return 1;
    }
}
