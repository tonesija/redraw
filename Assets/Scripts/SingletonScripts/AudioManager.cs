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

    float menuMusicVolume;

    float gameMusicVolume;

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
    }

    public void MusicOff(){
        gameMusic.volume = 0;
        menuMusic.volume = 0;
    }

    public void MusicOn(){
        gameMusic.volume = gameMusicVolume;
        menuMusic.volume = menuMusicVolume;
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

    public void PlayCollisionSound(float volume){
        print("Collision volume: " + volume);
        collisonSound.volume = volume;
        collisonSound.Play();
    }

    public void PlayTrampolineSound(){
        trampolineSound.Play();
    }

    
}
