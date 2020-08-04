using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpawnerScript : MonoBehaviour
{
    public int numberOfLevels;
    public GameObject levelButtonPrefab;
    public Transform content;

    void Start()
    {
        /* 
            put code here to fetch the number of levels from levelmanager object
        */

        
        for(int i=0; i<numberOfLevels; i++){
            GameObject levelButtonObj = Instantiate(levelButtonPrefab);
            levelButtonObj.transform.SetParent(content, false);

            levelButtonObj.GetComponent<LevelButtonManager>().levelNumber = i + 1;

            levelButtonObj.SetActive(true);
        }
    }
}
