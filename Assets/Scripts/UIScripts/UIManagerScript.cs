using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManagerScript : MonoBehaviour {

    TextMeshProUGUI completionTxt;

    void Start() {
        GameObject completionObj = GameObject.Find("CompletionTxt");
        if(completionObj != null){
            completionTxt = completionObj.GetComponent<TextMeshProUGUI>();
            SetCompletionText();
        }

        AudioManager.Instance.PlayMenuMusic();
    }
    public void LoadLastPlayedLevel(){
        int lastPlayedLevel = PlayerPrefs.GetInt("lastPlayedLevel", 1);
        
        if(lastPlayedLevel > LevelManagerScript.Instance.levelInfos.Length){
            lastPlayedLevel = LevelManagerScript.Instance.levelInfos.Length;
        }

        LevelManagerScript.Instance.LoadLevel(lastPlayedLevel);
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

    void SetCompletionText(){
        
        int numOfLevels = LevelManagerScript.Instance.levelInfos.Length;
        int maxStars = numOfLevels * 3;
        int collectedStars = 0;

        for(int i = 1; i <= numOfLevels; ++i){
            collectedStars += PlayerPrefs.GetInt(i.ToString(), 0);
        }

        completionTxt.text = collectedStars + "/" + maxStars;
    }

    public void ToggleMusic(bool toggle){
        if(toggle){
            AudioManager.Instance.MusicOff();
        } else {
            AudioManager.Instance.MusicOn();
        }
    }

    public void ToggleSound(bool toggle){
        if(toggle){
            AudioManager.Instance.SoundOff();
        } else {
            AudioManager.Instance.SoundOn();
        }
    }

}
