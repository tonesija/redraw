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

    void Start() {
        nextLvlBtn.onClick.AddListener(delegate { OnNextLvlClick(); });
        mainMenuBtn.onClick.AddListener(delegate { OnMainMenuClick(); });

    }


    public void SetScore(int score){
        if(score == 1){
            firstStar.GetComponent<Image>().color = Color.red;
        }
        if(score == 2){
            firstStar.GetComponent<Image>().color = Color.red;
            secondStar.GetComponent<Image>().color = Color.red;
        }
        if(score == 3){
            firstStar.GetComponent<Image>().color = Color.red;
            secondStar.GetComponent<Image>().color = Color.red;
            thirdStar.GetComponent<Image>().color = Color.red;
        }

    }

    public void ShowNewHighScore(){
        highScoreText.SetActive(true);

        highScoreText.GetComponent<ParticleSystem>().Play();
    }

    void OnNextLvlClick(){
        LevelManagerScript.Instance.LoadNextLevel();
    }

    void OnMainMenuClick(){
        LevelManagerScript.Instance.LoadMainMenu();
    }
}
