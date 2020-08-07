using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EndLevelUIScript : MonoBehaviour
{
    public GameObject firstStar;
    public GameObject secondStar;
    public GameObject thirdStar;

    public GameObject highScoreText;

    public Button nextLvlBtn;

    public Button mainMenuBtn;

    public Button restartLvlBtn;

    int score;

    void Start() {
        nextLvlBtn.onClick.AddListener(delegate { OnNextLvlClick(); });
        mainMenuBtn.onClick.AddListener(delegate { OnMainMenuClick(); });
        restartLvlBtn.onClick.AddListener(delegate { OnRestartLvlClick(); });
    }



    public void SetScore(int score){
        this.score = score;

        firstStar.GetComponent<Image>().color = Color.black;
        secondStar.GetComponent<Image>().color = Color.black;
        thirdStar.GetComponent<Image>().color = Color.black;

        StartCoroutine("SpawnStars");
    }

    public void DisableNextLvlBtn(){
        nextLvlBtn.gameObject.SetActive(false);
        mainMenuBtn.transform.localPosition = new Vector2(0, mainMenuBtn.transform.localPosition.y);
    }

    public void ShowNewHighScore(){
        highScoreText.SetActive(true);
    }

    void OnNextLvlClick(){
        AudioManager.Instance.PlayButtonClickSound();
        LevelManagerScript.Instance.LoadNextLevel();
    }

    void OnMainMenuClick(){
        AudioManager.Instance.PlayButtonClickSound();
        LevelManagerScript.Instance.LoadMainMenu();
    }

    void OnRestartLvlClick(){
        AudioManager.Instance.PlayButtonClickSound();
        AudioManager.Instance.PlayRestarLevelSound();
        LevelManagerScript.Instance.LoadLevel(LevelManagerScript.Instance.GetCurrentLvl());
    }

    IEnumerator ShowUI(){
        yield return StartCoroutine("WaitForSeconds", 0.6f);
        gameObject.SetActive(true);
    }

    IEnumerator SpawnStars(){
            switch (score){
            case 1:yield return StartCoroutine("WaitForSeconds",0.3f);
        First(); break;
            case 2:yield return StartCoroutine("WaitForSeconds",0.3f);
        First();
        yield return StartCoroutine("WaitForSeconds",0.3f);
        Second(); break;
            case 3: yield return StartCoroutine("WaitForSeconds",0.3f);
        First();
        yield return StartCoroutine("WaitForSeconds",0.3f);
        Second();
        yield return StartCoroutine("WaitForSeconds",0.3f);
        Third(); break;
        }
    }

    void First(){
        firstStar.GetComponent<Image>().color = Color.white;
        AudioManager.Instance.PlayStar1Sound();
    }
    void Second(){
        secondStar.GetComponent<Image>().color = Color.white;
        AudioManager.Instance.PlayStar2Sound();
    }
    void Third(){
        thirdStar.GetComponent<Image>().color = Color.white;
        AudioManager.Instance.PlayStar3Sound();
    }

    IEnumerator WaitForSeconds (float seconds) {
    float startTime = Time.realtimeSinceStartup; 
    while (Time.realtimeSinceStartup-startTime < seconds) {
        yield return null;
    }
}
}
