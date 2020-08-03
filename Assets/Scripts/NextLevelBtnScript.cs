using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class NextLevelBtnScript : MonoBehaviour
{
    Button thisBtn;

    void Start() {
        thisBtn = GetComponent<Button>();
        thisBtn.onClick.AddListener(delegate { OnClick(); });
    }

    void OnClick(){
         LevelManagerScript.Instance.LoadNextLevel();
    }

    
}
