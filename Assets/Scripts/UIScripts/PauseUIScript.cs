using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseUIScript : MonoBehaviour
{
    public Button mainMenuBtn;

    public TextMeshProUGUI rewindsTxt;

    void Start() {
        mainMenuBtn.onClick.AddListener(delegate { OnMainMenuClick(); });
    }

    void OnEnable()
    {
        rewindsTxt.text = "Rewinds: " + LevelManagerScript.Instance.GetRewinds();
    }

    void OnMainMenuClick(){
        LevelManagerScript.Instance.LoadMainMenu();
    }
}
