using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour{

    [SerializeField] private TextMeshProUGUI scoreNumber;

    private void Start() {
        scoreNumber.text = GameManager.Instance.GetScoreCount().ToString();
        GameManager.Instance.OnScoreCountChange += GameManager_OnScoreCountChange;
    }

    private void GameManager_OnScoreCountChange(object sender, GameManager.OnKillCountChangeEventArgs e) {
        scoreNumber.text = e.scoreCount.ToString();
    }
}
