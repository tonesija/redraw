using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerMovement playerMovement;


    Dictionary<float, UserInput> actions;

    bool playerControlled;

    PlayerMovement.MovementDelegate movementDelegate;
    void OnEnable()
    {
        TimerScript.Instance.SetTime(0f);
        actions = new Dictionary<float, UserInput>();
        playerMovement = GetComponent<PlayerMovement>();
        playerControlled = true;
        movementDelegate = null;
    }

    
    void Update()
    {
        if(playerControlled) CheckInput();
    }

    void CheckInput(){
        //---LEFT AND RIGHT LOGGING---
        if(Input.GetKeyDown(KeyCode.D)){
            AddActionEntry(TimerScript.Instance.GetTime(), KeyCode.D, true);
        }

        if(Input.GetKeyDown(KeyCode.A)){
            AddActionEntry(TimerScript.Instance.GetTime(), KeyCode.A, true);
        }

        if(Input.GetKeyUp(KeyCode.D)){
            AddActionEntry(TimerScript.Instance.GetTime(), KeyCode.D, false);
        }

        if(Input.GetKeyUp(KeyCode.A)){
            AddActionEntry(TimerScript.Instance.GetTime(), KeyCode.A, false);
        }


        //---LEFT AND RIGHT---
        if(Input.GetKey(KeyCode.A)){
            playerMovement.MoveLeft();
        }
        if(Input.GetKey(KeyCode.D)){
            playerMovement.MoveRight();
        }

        
        //---JUMP---
        if(Input.GetKeyDown(KeyCode.Space)){
            playerMovement.Jump();
            AddActionEntry(TimerScript.Instance.GetTime(), KeyCode.Space, true);
        }

        //---SIZEUP---
        if(Input.GetKeyDown(KeyCode.S)){
            playerMovement.SizeUp();
            AddActionEntry(TimerScript.Instance.GetTime(), KeyCode.S, true);
        }


        //print actions in the actions map
        if(Input.GetKeyDown(KeyCode.LeftAlt)){
            Dictionary<float, UserInput>.KeyCollection keys = actions.Keys;
            foreach(float time in keys){
                print(time + "   Action: " + actions[time]);
            }
        }

        //execute actions from the map
        if(Input.GetKeyDown(KeyCode.LeftControl)){
            TimerScript.Instance.SetTime(0f);
            playerControlled = false;
            
            transform.position = new Vector2(0, 0);
            InstantiateNewPlayer();
            //StartCoroutine("ExecuteActions");
        }
    }

    void InstantiateNewPlayer(){
        Instantiate(this, new Vector3(0,1,0), Quaternion.identity);
    }

    void FixedUpdate() {
        if(playerControlled == false){
            float time = TimerScript.Instance.GetTime();

            if(actions.ContainsKey(time)){
                if(actions[time].GetDown())
                ExecuteAction(actions[time].GetKey());
                else movementDelegate = null;
            }

            if(movementDelegate != null) movementDelegate();
        }
        
    }

    //not used
    IEnumerator ExecuteActions(){
        while(true){
        float time = TimerScript.Instance.GetTime();

            if(actions.ContainsKey(time)){
                if(actions[time].GetDown())
                ExecuteAction(actions[time].GetKey());
                else movementDelegate = null;
            }

        if(movementDelegate != null) movementDelegate();

        yield return null;
        }
    }

    void ExecuteAction(KeyCode key){
        switch (key){
            case KeyCode.D: movementDelegate = playerMovement.MoveRight; break;
            case KeyCode.A: movementDelegate = playerMovement.MoveLeft; break;
            case KeyCode.Space: playerMovement.Jump(); break;
            case KeyCode.S: playerMovement.SizeUp(); break;
            default: return;
        }
    }

    void AddActionEntry(float time, KeyCode key, bool down){
        UserInput toAdd = new UserInput(key, down);
        try{
            actions.Add(time, toAdd);
        }
        catch{
            actions.Add(time + Time.fixedDeltaTime, toAdd);
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
