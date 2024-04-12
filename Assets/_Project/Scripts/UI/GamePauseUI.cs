using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    public class GamePauseUI : MonoBehaviour {

        [SerializeField] Button mainMenuButton;
        [SerializeField] Button resumeButton;
        [SerializeField] GameObject gamePauseUI;


        private void Awake() {
            Hide();
        }

        private void GameInput_OnPausePerformed(object sender, System.EventArgs e) {
            //Time.timeScale = 0f;
            if (gamePauseUI.activeSelf) {
                Hide();
                InputHandler.Instance.SetGamePaused(false);
            } else {
                Show();
            InputHandler.Instance.SetGamePaused(true);
        }

        }

        private void Start() {
            InputHandler.Instance.OnPausePerformed += GameInput_OnPausePerformed;

            mainMenuButton.onClick.AddListener(() => {

                Loader.Load(Loader.Scene.MainMenuScene);

            });

            resumeButton.onClick.AddListener(() => {
                //Time.timeScale = 1f;
                InputHandler.Instance.SetGamePaused(false);
                Hide();

            });

        }

        private void Show() {
        gamePauseUI.SetActive(true);
        resumeButton.Select();
        Time.timeScale = 0f;
        }

        private void Hide() {
        gamePauseUI.SetActive(false);
        Time.timeScale = 1.0f;
        }

    }
