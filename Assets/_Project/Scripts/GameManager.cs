using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class GameManager : MonoBehaviour {

    [SerializeField] private float gameTime = 240f;
    [SerializeField] private Transform treasureLocation;
    [SerializeField] private GameObject[] treasureArray;
    [SerializeField] private int savedTreasureScore = 300;

    public static GameManager Instance { get; private set; }
    public event EventHandler OnGameOver;
    public event EventHandler OnTreasureLoot;
    public event EventHandler OnHalfTime;
    public event EventHandler<OnKillCountChangeEventArgs> OnScoreCountChange;
    public class OnKillCountChangeEventArgs : EventArgs {
        public int scoreCount;
    }

    private int scoreCount = 0;
    private bool gameOver = false;
    private float halfTime;

  

    private void Awake() {
        Instance = this;
        halfTime = gameTime / 2;
    }

    private void Update() {

    if (!InputHandler.Instance.GetGamePaused()) { 
    gameTime -= Time.deltaTime;
    }

    if (gameTime <= halfTime) {
            OnHalfTime?.Invoke(this, EventArgs.Empty);
        }

    if ((treasureArray.Length == 0 || gameTime <= 0f) && !gameOver) {
            GameManager.Instance.SetScoreCount(treasureArray.Length * savedTreasureScore);
            OnGameOver?.Invoke(this, EventArgs.Empty);
            gameOver = true;
    }
    }

    public int GetScoreCount() {
        return scoreCount;
    }

    public void SetScoreCount (int scoreCount) {
        this.scoreCount += scoreCount;
        OnScoreCountChange?.Invoke(this, new OnKillCountChangeEventArgs {
            scoreCount = this.scoreCount
        });
    }

    public float GetGameTime() {
        return gameTime;
    }

    public Transform GetTreasureLocation() {
        return treasureLocation;
    }


    public void LootTreasure() {
        if (treasureArray.Length > 0) {
            int randomIndex = UnityEngine.Random.Range(0, treasureArray.Length);
            GameObject selectedTreasure = treasureArray[randomIndex];
            treasureArray[randomIndex] = treasureArray[treasureArray.Length - 1];
            System.Array.Resize(ref treasureArray, treasureArray.Length - 1);
            Destroy(selectedTreasure);
            OnTreasureLoot?.Invoke(this, EventArgs.Empty);
        } else {
            Debug.LogWarning("The treasureArray is empty.");
        }
    }

    public int GetTreasureNumber() {
        return treasureArray.Length;
    }

    public bool isGameOver() {
        return gameOver;
    }

    }
