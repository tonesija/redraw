﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public static event Action OnPlayerRewind;
    public static event Action OnPlayerFinish;

    public static event Action OnPauseBtnClick;

    PlayerMovement playerMovement;
    Dictionary<float, List<UserInput>> actions;

    bool playerControlled;

    PlayerMovement.MovementDelegate movementDelegate;

    MorphUIScript morphUI;

    void Awake()
    {
        TimerScript.Instance.SetTime(0f);
        actions = new Dictionary<float, List<UserInput>>();
        playerMovement = GetComponent<PlayerMovement>();
        playerControlled = true;
        movementDelegate = null;

        morphUI = LevelManagerScript.Instance.GetMorphUIScript();
    }

    
    void Update()
    {
        if(playerControlled) CheckInput();
    }

    void CheckInput(){
        bool gameRunning = Time.timeScale > 0.9f;

        //---LEFT AND RIGHT LOGGING---
        if(Input.GetKeyDown(KeyCode.D)){
            AddActionEntry(TimerScript.Instance.GetTime(), KeyCode.D, true);
            playerMovement.MoveRight();
        }

        if(Input.GetKeyDown(KeyCode.A)){
            AddActionEntry(TimerScript.Instance.GetTime(), KeyCode.A, true);
            playerMovement.MoveLeft();
        }

        if(Input.GetKeyUp(KeyCode.D)){
            AddActionEntry(TimerScript.Instance.GetTime(), KeyCode.D, false);
            playerMovement.StopRight();
        }

        if(Input.GetKeyUp(KeyCode.A)){
            AddActionEntry(TimerScript.Instance.GetTime(), KeyCode.A, false);
            playerMovement.StopLeft();
        }


        //---JUMP---
        if((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))  && gameRunning){
            playerMovement.Jump();
            AddActionEntry(TimerScript.Instance.GetTime(), KeyCode.Space, true);
        }

        //---PAUSE---
        if(Input.GetKeyDown(KeyCode.P)){
            OnPauseBtnClick();
        }

        //---MORPHING---
        if(gameRunning){
            if(Input.GetKeyDown(KeyCode.Alpha1)){
                GameObject morph = morphUI.GetMorph1();
                if(morph != null){
                    playerMovement.Morph(morph);
                    AddActionEntry(TimerScript.Instance.GetTime(), KeyCode.Alpha1, true);
                }
            }
            if(Input.GetKeyDown(KeyCode.Alpha2)){
                GameObject morph = morphUI.GetMorph2();
                if(morph != null){
                    playerMovement.Morph(morph);
                    AddActionEntry(TimerScript.Instance.GetTime(), KeyCode.Alpha2, true);
                }
            }
            if(Input.GetKeyDown(KeyCode.Alpha3)){
                GameObject morph = morphUI.GetMorph3();
                if(morph != null){
                    playerMovement.Morph(morph);
                    AddActionEntry(TimerScript.Instance.GetTime(), KeyCode.Alpha3, true);
                }
            }
        }


        //print actions in the actions map
        if(Input.GetKeyDown(KeyCode.LeftAlt)){
            Dictionary<float, List<UserInput>>.KeyCollection keys = actions.Keys;
            foreach(float time in keys){
                string toPrint = "Time: " + time + " ";
                List<UserInput> inputs = actions[time];

                inputs.ForEach((e) => {
                    toPrint += e.ToString();
                });
                
                print(toPrint);
            }
        }

        //execute actions from the map
        if(Input.GetKeyDown(KeyCode.LeftControl) && gameRunning){
            OnPlayerRewind();
            playerControlled = false;
        }
    }

    void FixedUpdate() {
        if(playerControlled == false){  //if the player is not playerControlled move him via the action map and Timer singleton
            float time = TimerScript.Instance.GetTime();

            if(actions.ContainsKey(time)){
                List<UserInput> tmp = actions[time];
                foreach(UserInput userInput in tmp){
                    ExecuteAction(userInput.GetKey(), userInput.GetDown());
                
                    if(movementDelegate != null) movementDelegate();
                }
                
            } 
        }
        
    }

    //execute the player action
    void ExecuteAction(KeyCode key, bool down){
        if(down){
            switch (key){
                case KeyCode.D: movementDelegate = playerMovement.MoveRight; break;
                case KeyCode.A: movementDelegate = playerMovement.MoveLeft; break;
                case KeyCode.Space: playerMovement.Jump(); break;
                case KeyCode.Alpha1: playerMovement.Morph(morphUI.GetMorph1()); break;
                case KeyCode.Alpha2: playerMovement.Morph(morphUI.GetMorph2()); break;
                case KeyCode.Alpha3: playerMovement.Morph(morphUI.GetMorph3()); break;
                default: return;
            }
        } else {
            switch (key){
                case KeyCode.D: movementDelegate = playerMovement.StopRight; break;
                case KeyCode.A: movementDelegate = playerMovement.StopLeft; break;
                default: return;
            }
        }
    }

    //add action to the action map
    void AddActionEntry(float time, KeyCode key, bool down){
        UserInput toAdd = new UserInput(key, down);
        if(!actions.ContainsKey(time)){
            List<UserInput> tmp = new List<UserInput>();
            tmp.Add(toAdd);
            actions.Add(time, tmp);
        } else {
            actions[time].Add(toAdd);
        }
    
    }

    //trigger the OnPlayerFinish event when some player touches the LevelEnd gameObject
    void OnTriggerEnter2D(Collider2D other) {
        if(other.name == "LevelEnd"){
            OnPlayerFinish();
        }
    }

    struct UserInput{
        KeyCode key;

        bool down;
        public UserInput(KeyCode key, bool down){
            this.key = key;
            this.down = down;
        }
        
        public override string ToString(){
            return key.ToString() + "   " + "Down: " + down;
        }
        public KeyCode GetKey(){
            return key;
        }
        public bool GetDown(){
            return down;
        }
    }

}
