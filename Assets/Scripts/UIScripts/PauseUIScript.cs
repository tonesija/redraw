using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseUIScript : MonoBehaviour
{
    public Button mainMenuBtn;

    public Button restartBtn;

    public TextMeshProUGUI rewindsTxt;

    void Start() {
        mainMenuBtn.onClick.AddListener(delegate { OnMainMenuClick(); });
        restartBtn.onClick.AddListener(delegate { OnRestartClick(); });
    }

    void OnEnable()
    {
        rewindsTxt.text = "Rewinds: " + LevelManagerScript.Instance.GetRewinds();
    }

    void OnMainMenuClick(){
        LevelManagerScript.Instance.LoadMainMenu();
    }

    void OnRestartClick(){
        LevelManagerScript.Instance.LoadLevel(LevelManagerScript.Instance.GetCurrentLvl());
    }
}
