using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TreasureUI : MonoBehaviour{

    [SerializeField] private TextMeshProUGUI treasureText;


    private void Start() {
        GameManager.Instance.OnTreasureLoot += GameManager_OnTreasureLoot;
        treasureText.text = GameManager.Instance.GetTreasureNumber().ToString();
    }

    private void GameManager_OnTreasureLoot(object sender, System.EventArgs e) {
        treasureText.text = GameManager.Instance.GetTreasureNumber().ToString();
    }
}
