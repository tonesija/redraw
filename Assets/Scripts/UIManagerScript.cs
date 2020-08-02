using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerScript : MonoBehaviour {
    public void LoadLastPlayedLevel(){
        SceneManager.LoadScene(PlayerPrefs.GetString("lastPlayedLevel", "Level1"));
    }

    public void LoadLevelsScene(){
        SceneManager.LoadScene("LevelsScene");
    }

    public void LoadSettingsScene(){
        SceneManager.LoadScene("SettingsScene");
    }

    public void LoadAboutScene(){
        SceneManager.LoadScene("AboutScene");
    }

    public void LoadMainMenuScene(){
        SceneManager.LoadScene("MainMenuScene");
    }

}
