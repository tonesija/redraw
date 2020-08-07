using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagerScript : MonoBehaviour
{
    public static LevelManagerScript Instance {get; private set;}

    public float spawnDelay;
    public GameObject playerPrefab;

    public GameObject eraserPrefab;

    public GameObject endLevelUIPrefab;

    public GameObject pauseUIPrefab;

    public GameObject morphUIPrefab;
    
    public LevelInfo[] levelInfos;

    List<GameObject> players;

    public List<IResetable> resetables;
    Vector2 spawnLoc;

    int level;

    int rewinds;

    bool subscribedToPlayer;

    bool endLvlUIActive;

    AsyncOperation asyncLoadLevel;

    GameObject pauseUI;


    GameObject morphUI;



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
            resetables = new List<IResetable>();
            subscribedToPlayer = true;
        }
    }

    //should be called after the level has been loaded
    //finds the spawnLocation, subscribes to players listeners and spawns the first player
    void NextLevel(){
        Time.timeScale = 1;
        AudioManager.Instance.PlayGameMusic();
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
        AudioManager.Instance.PlayEraserSound();
        rewinds++;

        Vector2 playerOffset = new Vector2(0, players[0].GetComponent<Collider2D>().bounds.size.y / 2);
        foreach(GameObject player in players){  //spawn erasers
            Instantiate(eraserPrefab, player.transform.position + (Vector3)playerOffset, Quaternion.identity); 
        }

        foreach(SpriteRenderer s in players.ElementAt(players.Count - 1).GetComponentsInChildren<SpriteRenderer>()){
            s.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        }
        
        
        GameObject newPlayer = Instantiate(playerPrefab, spawnLoc, Quaternion.identity);
        players.Add(newPlayer);

        StartCoroutine("SpawnPlayers");
    }

    //triggers when player touches the level end game object
    void OnPlayerReachLevelFinish(){
        Time.timeScale = 0;

        EndLevelUIScript uiScript = Instantiate(endLevelUIPrefab).GetComponent<EndLevelUIScript>();
        if(level == levelInfos.Length) uiScript.DisableNextLvlBtn();
        SaveToPlayerPrefsAndShowHighscore(uiScript);
        uiScript.gameObject.SetActive(false);
        StartCoroutine("ShowUI", uiScript);
        
        AudioManager.Instance.PlayLevelFinishedSound();

       
    }

    IEnumerator WaitForSeconds (float seconds) {
        float startTime = Time.realtimeSinceStartup; 
        while (Time.realtimeSinceStartup-startTime < seconds) {
            yield return null;
        }
    }

    IEnumerator ShowUI(EndLevelUIScript script){
        yield return StartCoroutine("WaitForSeconds", 0.9f);
        script.gameObject.SetActive(true);
        endLvlUIActive = true;
        script.SetScore(GetScore(levelInfos[level - 1].requiredScore, rewinds));
    }

    void SaveToPlayerPrefsAndShowHighscore(EndLevelUIScript script){
        int previousScore = PlayerPrefs.GetInt(level.ToString(), 0);

        if(level >= PlayerPrefs.GetInt("lastPlayedLevel", 1)){
            PlayerPrefs.SetInt("lastPlayedLevel", level + 1);
        }
        
        if(previousScore < GetScore(levelInfos[level - 1].requiredScore, rewinds)){
            PlayerPrefs.SetInt(level + "", GetScore(levelInfos[level - 1].requiredScore, rewinds));
            script.ShowNewHighScore();
        }
    }

    //load a level and call NextLevel
    IEnumerator LoadLevelAsync(int level){
        resetables.Clear();

        asyncLoadLevel = SceneManager.LoadSceneAsync("Level"+ level);
        while(!asyncLoadLevel.isDone) yield return null;

        InstantiateMorphUI();
        
        NextLevel();
    }

    void InstantiateMorphUI(){
        morphUI = Instantiate(morphUIPrefab);
        morphUI.GetComponent<MorphUIScript>().SetMorph1(levelInfos[level - 1].morph1);
        morphUI.GetComponent<MorphUIScript>().SetMorph2(levelInfos[level - 1].morph2);
        morphUI.GetComponent<MorphUIScript>().SetMorph3(levelInfos[level - 1].morph3);

        if(!morphUI.GetComponent<MorphUIScript>().HasMorphs()) morphUI.SetActive(false);
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

    public int GetCurrentLvl(){
        return level;
    }

    public MorphUIScript GetMorphUIScript(){
        return morphUI.GetComponent<MorphUIScript>();
    }

    //spawns all players periodically on the spawnLoc
    IEnumerator SpawnPlayers(){
        foreach(GameObject player in players){ 
            player.transform.parent = null; 
            player.SetActive(false);    //deactivate all players
        }

        CleanResetableList();
        ResetResetables();

        yield return new WaitForSeconds(EraserScript.duration); //wait for the duration of the erase animation
        TimerScript.Instance.SetTime(0f); //reset the movement clock

        foreach(GameObject player in players){  //periodically spawn all players
            player.SetActive(true);
            player.GetComponent<PlayerMovement>().Reset();
            player.transform.position = spawnLoc;
            yield return new WaitForSeconds(spawnDelay);
        }        

    }

    void CleanResetableList(){
        int num = resetables.Count;

        for(int i = num - 1; i >= 0; --i){
            if(resetables[i] is PlayerMorphs){
                if((PlayerMorphs) resetables[i] == null){
                    resetables.RemoveAt(i);
                    continue;
                }
            }
            if(resetables[i] == null) resetables.RemoveAt(i);
        }
        
    }


    //calls the Reset() method on all the resetables in the scene
    //Reset() should reset the object to its starting state in the scene
    void ResetResetables(){
        foreach(IResetable resetable in resetables){
            resetable.Reset(eraserPrefab);
        }
    }

    int GetScore(int req, int rewinds){
        if(rewinds <= req) return 3;

        if(rewinds - 1 == req) return 2;

        return 1;
    }

    [System.Serializable]
    public class LevelInfo{
        public GameObject morph1;
        public GameObject morph2;
        public GameObject morph3;

        public int requiredScore;

        
    }
}
