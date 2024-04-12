using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputHandler : MonoBehaviour    {

    public static InputHandler Instance { get; private set; }

    public event EventHandler OnPausePerformed;

    public Vector3 moveTarget;
    public Vector3 mousePos;
    private float horizontal;
    private float vertical;
    private bool gamePaused = false;


    private void Awake() {
        Instance = this;
    }
    private void Update(){
        if (GameManager.Instance.isGameOver()) { return; }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7)) {
            OnPausePerformed?.Invoke(this, EventArgs.Empty);
        }

        if (gamePaused) { return; }

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        moveTarget = new Vector3 (horizontal, 0, vertical).normalized;
        mousePos = Input.mousePosition;


        }


    public void SetGamePaused(bool gamePaused){
        this.gamePaused = gamePaused;
    }

    public bool GetGamePaused() { 
        return gamePaused; }

    }
