using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelButtonManager : MonoBehaviour
{
    public int levelNumber = 1;
    private string levelSceneName;

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
        
    }

    public void LoadLevel(){
        SceneManager.LoadScene(levelSceneName);
    }
}
