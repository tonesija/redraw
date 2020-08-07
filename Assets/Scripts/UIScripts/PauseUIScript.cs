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
        AudioManager.Instance.PlayButtonClickSound();
        LevelManagerScript.Instance.LoadMainMenu();
    }

    void OnRestartClick(){
        AudioManager.Instance.PlayButtonClickSound();
        AudioManager.Instance.PlayRestarLevelSound();
        LevelManagerScript.Instance.LoadLevel(LevelManagerScript.Instance.GetCurrentLvl());
    }
}
