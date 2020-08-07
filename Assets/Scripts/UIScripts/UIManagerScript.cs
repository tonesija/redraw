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
        AudioManager.Instance.PlayButtonClickSound();
        int lastPlayedLevel = PlayerPrefs.GetInt("lastPlayedLevel", 1);
        
        if(lastPlayedLevel > LevelManagerScript.Instance.levelInfos.Length){
            lastPlayedLevel = LevelManagerScript.Instance.levelInfos.Length;
        }

        LevelManagerScript.Instance.LoadLevel(lastPlayedLevel);
    }

    public void LoadLevelsScene(){
        AudioManager.Instance.PlayButtonClickSound();
        SceneManager.LoadScene("LevelsScene");
    }

    public void LoadSettingsScene(){
        AudioManager.Instance.PlayButtonClickSound();
        SceneManager.LoadScene("SettingsScene");
    }

    public void LoadAboutScene(){
        AudioManager.Instance.PlayButtonClickSound();
        SceneManager.LoadScene("AboutScene");
    }

    public void LoadMainMenuScene(){
        AudioManager.Instance.PlayButtonClickSound();
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
        AudioManager.Instance.PlayButtonClickSound();
        if(toggle){
            AudioManager.Instance.MusicOff();
        } else {
            AudioManager.Instance.MusicOn();
        }
    }

    public void ToggleSound(bool toggle){
        AudioManager.Instance.PlayButtonClickSound();
        if(toggle){
            AudioManager.Instance.SoundOff();
        } else {
            AudioManager.Instance.SoundOn();
        }
    }

}
