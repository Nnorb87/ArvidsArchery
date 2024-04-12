using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class AnimationController : MonoBehaviour
    {

        #region Variables

        [SerializeField]
        private GameObject _skeletalMesh;
        [SerializeField]
        private ProjectileSpawner _arrowSpawner;
        [SerializeField]
        private Animator _bowAnimController;
        [SerializeField]
        private Animator _arrowAnimController;
        
        private Animator _playerAnimController;
        private CharacterMovement _movScript;
        
        private float _horizontal = 0.0f;
        private float _vertical = 0.0f;

        #endregion

        void Start()
        {
            _playerAnimController = _skeletalMesh.GetComponent<Animator>();
            _movScript = GetComponent<CharacterMovement>();
        }

        void Update()
        {
            _horizontal = _movScript.AnimationDirection.x;
            _vertical = _movScript.AnimationDirection.z;

            _playerAnimController.SetFloat("Horizontal", _horizontal);
            _playerAnimController.SetFloat("Vertical", _vertical);

            if (_movScript.IsJumping) { _playerAnimController.SetBool("Jump", true); }
            else { _playerAnimController.SetBool("Jump", false); }


            _playerAnimController.SetBool("Fire",_arrowSpawner._isFiring);
            _bowAnimController.SetBool("Fire", _arrowSpawner._isFiring);
            _arrowAnimController.SetBool("Fire", _arrowSpawner._isFiring);


        }
    }
