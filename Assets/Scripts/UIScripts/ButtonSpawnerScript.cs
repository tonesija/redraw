using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpawnerScript : MonoBehaviour
{
    private int numberOfLevels;
    public GameObject levelButtonPrefab;
    public Transform content;

    void Start()
    {

        // Gets the number of levels
        numberOfLevels = LevelManagerScript.Instance.levelInfos.Length;
        
        // Spawns level buttons
        for(int i=0; i<numberOfLevels; i++){
            GameObject levelButtonObj = Instantiate(levelButtonPrefab);
            levelButtonObj.transform.SetParent(content, false);

            levelButtonObj.GetComponent<LevelButtonManager>().levelNumber = i + 1;

            levelButtonObj.SetActive(true);
        }
    }
}
