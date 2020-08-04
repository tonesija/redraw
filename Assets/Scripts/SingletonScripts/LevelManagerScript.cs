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

    public GameObject pauseUIPrefab;

    public int[] requiredScores;

    List<GameObject> players;
    Vector2 spawnLoc;

    int level;

    int rewinds;

    bool subscribedToPlayer;

    bool endLvlUIActive;

    AsyncOperation asyncLoadLevel;

    GameObject pauseUI;



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
            PlayerController.OnPauseBtnClick += OnPause;
            subscribedToPlayer = true;
        }
    }

    //should be called after the level has been loaded
    //finds the spawnLocation, subscribes to players listeners and spawns the first player
    void NextLevel(){
        Time.timeScale = 1;
        spawnLoc = GameObject.Find("PlayerSpawn").transform.position;
        rewinds = 0;
        endLvlUIActive = false;

        players = new List<GameObject>();
        players.Add(Instantiate(playerPrefab, spawnLoc, Quaternion.identity));
    }

    //gets called when the user presses "escape"
    void OnPause(){
        if(endLvlUIActive) return;
        if(pauseUI == null){
            pauseUI = Instantiate(pauseUIPrefab);
            Time.timeScale = 0;
            return;
        }
        
        if(pauseUI.activeSelf == false) {
            pauseUI.SetActive(true);
            Time.timeScale = 0;
        }
        else {
            pauseUI.SetActive(false);
            Time.timeScale = 1;
        }
        
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
        Time.timeScale = 0;
        endLvlUIActive = true;
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

    public int GetRewinds(){
        return rewinds;
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
