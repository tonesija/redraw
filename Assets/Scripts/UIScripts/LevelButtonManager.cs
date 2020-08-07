using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelButtonManager : MonoBehaviour
{
    public int levelNumber = 1;
    private string levelSceneName;

    public GameObject firstStar;
    public GameObject secondStar;
    public GameObject thirdStar;

    void OnEnable(){
        // Setting the name of the button's scene
        levelSceneName = "Level" + levelNumber.ToString();

        // Setting the position of the button inside the scroll view
        RectTransform rectTransform;
        float xPos;
        float yPos;

        xPos = -480.0f + ((levelNumber - 1) % 7) * 160.0f;
        yPos = -120.0f - ((levelNumber - 1) / 7) * 174.0f;

        rectTransform = GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector2(xPos, yPos);

        // Setting the text inside the button
        TextMeshProUGUI buttonText = GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = levelNumber.ToString();

        ShowStars();

        if(levelNumber > PlayerPrefs.GetInt("lastPlayedLevel", 1)){
            GreyOut();
        }
        
    }

    public void LoadLevel(){
        //SceneManager.LoadScene(levelSceneName);
        LevelManagerScript.Instance.LoadLevel(levelNumber);
    }

    void ShowStars(){
        int numOfStars = PlayerPrefs.GetInt(levelNumber.ToString(), 0);

        firstStar.SetActive(false);
        secondStar.SetActive(false);
        thirdStar.SetActive(false);

        if(numOfStars == 1){
            firstStar.SetActive(true);
        }
        if(numOfStars == 2){
            firstStar.SetActive(true);
            secondStar.SetActive(true);
        }
        if(numOfStars == 3){
            firstStar.SetActive(true);
            secondStar.SetActive(true);
            thirdStar.SetActive(true);
        }
    }

    void GreyOut(){
        GetComponent<Button>().interactable = false;
    }
}
