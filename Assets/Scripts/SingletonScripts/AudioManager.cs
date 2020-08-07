using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {get; private set;}

    public AudioSource menuMusic;
    public AudioSource gameMusic;


    public AudioSource eraserSound;

    public AudioSource restartLevelSound;

    public AudioSource star1Sound;
    public AudioSource star2Sound;
    public AudioSource star3Sound;

    public AudioSource trampolineSound;

    public AudioSource cannonSound;

    public AudioSource collisonSound;

    public AudioSource levelFinishedSound;

    float menuMusicVolume;

    float gameMusicVolume;

    float starsVolume;

    float eraserVolume;
    float restartVolume;
    float trampolineVolume;

    float cannonVolume;
    float collisionVolume;

    float levelFinishedVolume;

    void Awake()
    {
        if(Instance == null){
            Instance = this;
            InitializeSingleton();
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
        
    }

    void InitializeSingleton(){
        menuMusicVolume = menuMusic.volume;
        gameMusicVolume = gameMusic.volume;

        starsVolume = star1Sound.volume;
        eraserVolume = eraserSound.volume;
        restartVolume = restartLevelSound.volume;
        trampolineVolume = trampolineSound.volume;
        cannonVolume = cannonSound.volume;
        collisionVolume = collisonSound.volume;
        levelFinishedVolume = levelFinishedSound.volume;
    }

    public void MusicOff(){
        PlayerPrefs.SetInt("music", 0);
        gameMusic.volume = 0;
        menuMusic.volume = 0;
    }

    public void MusicOn(){
        PlayerPrefs.SetInt("music", 1);
        gameMusic.volume = gameMusicVolume;
        menuMusic.volume = menuMusicVolume;
    }

    public void SoundOn(){
        PlayerPrefs.SetInt("sound", 1);
        star1Sound.volume = starsVolume;
        star2Sound.volume = starsVolume;
        star3Sound.volume = starsVolume;
        eraserSound.volume = eraserVolume;
        restartLevelSound.volume = restartVolume;
        trampolineSound.volume = trampolineVolume;
        cannonSound.volume = cannonVolume;
        collisonSound.volume = collisionVolume;
        levelFinishedSound.volume = levelFinishedVolume;
    }

    public void SoundOff(){
        PlayerPrefs.SetInt("sound", 0);
        star1Sound.volume = 0;
        star2Sound.volume = 0;
        star3Sound.volume = 0;
        eraserSound.volume = 0;
        restartLevelSound.volume = 0;
        trampolineSound.volume = 0;
        cannonSound.volume = 0;
        collisonSound.volume = 0;
        levelFinishedSound.volume = 0;
    }

    public void PlayMenuMusic(){
        if(menuMusic.isPlaying) return;
        gameMusic.Stop();
        menuMusic.Play();
    }

    public void PlayGameMusic(){
        if(gameMusic.isPlaying) return;
        menuMusic.Stop();
        gameMusic.Play();
    }

    public void PlayEraserSound(){
        eraserSound.Play();
    }

    public void PlayStar1Sound(){
        star1Sound.Play();
    }
    public void PlayStar2Sound(){
        star2Sound.Play();
    }
    public void PlayStar3Sound(){
        star3Sound.Play();
    }

    public void PlayRestarLevelSound(){
        restartLevelSound.Play();
    }

    public void PlayCannonSound(){
        cannonSound.Play();
    }

    public void PlayCollisionSound(float multiplier){
        collisonSound.volume = collisonSound.volume * multiplier;
        collisonSound.Play();
    }

    public void PlayTrampolineSound(){
        trampolineSound.Play();
    }

    public void PlayLevelFinishedSound(){
        levelFinishedSound.Play();
    }

    
}
