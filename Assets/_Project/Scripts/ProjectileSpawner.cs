using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class ProjectileSpawner : MonoBehaviour
    {
        #region Variables

        [SerializeField]
        private GameObject _projectile;
        [SerializeField]
        private GameObject _spawnPoint;
        [SerializeField]
        public GameObject _arrow;

        [HideInInspector]
        public bool _isFiring;

        private float _timer = 1.0f;

        #endregion

        private void Update(){
            _isFiring = false;
            _timer += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Mouse0) && !InputHandler.Instance.GetGamePaused())
            {
                _isFiring = true;
            }
        }

        public void SpawnArrow()
        {
            if (_timer >= 1.0)
            {
                _arrow.gameObject.SetActive(false);
                Instantiate(_projectile, _spawnPoint.transform.position, _spawnPoint.transform.rotation);
                _isFiring = false;
                _timer = 0f;
            }
        }
    }
