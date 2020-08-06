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
            firstStar.GetComponent<Image>().color = Color.white;
        }
        if(score == 2){
            firstStar.GetComponent<Image>().color = Color.white;
            secondStar.GetComponent<Image>().color = Color.white;
        }
        if(score == 3){
            firstStar.GetComponent<Image>().color = Color.white;
            secondStar.GetComponent<Image>().color = Color.white;
            thirdStar.GetComponent<Image>().color = Color.white;
        }

    }

    public void DisableNextLvlBtn(){
        nextLvlBtn.gameObject.SetActive(false);
        mainMenuBtn.transform.localPosition = new Vector2(0, mainMenuBtn.transform.localPosition.y);
    }

    public void ShowNewHighScore(){
        highScoreText.SetActive(true);

        // highScoreText.GetComponent<ParticleSystem>().Play();
    }

    void OnNextLvlClick(){
        LevelManagerScript.Instance.LoadNextLevel();
    }

    void OnMainMenuClick(){
        LevelManagerScript.Instance.LoadMainMenu();
    }
}
