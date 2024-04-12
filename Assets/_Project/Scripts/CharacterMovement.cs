using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class CharacterMovement : MonoBehaviour
    {

        #region Variables

        [SerializeField]
        private float _walkSpeedInput;
        [SerializeField]
        private float _runSpeedInput;
        [SerializeField]
        private float _turnSpeedInput;
        [SerializeField]
        private float _jumpForce;
        [SerializeField]
        private bool _lookAtMouse;     

        [HideInInspector]
        public bool IsJumping;
        [HideInInspector]
        public bool IsSprinting;
        [HideInInspector]
        public bool IsInAir;
        
        private float _velocity;
        public Vector3 AnimationDirection = new Vector3(0,0,0);
        private Quaternion _relativeMouseRotation;
        private Vector3 _targetPosition;
    
        private float _movementSpeed;
        private Camera _camera;
        private InputHandler _input;
        private Rigidbody _rigidBody;

        #endregion

        #region SetValues

        void Awake()
        {
            _input = GetComponent<InputHandler>();
            _camera = Camera.main;
            _rigidBody = GetComponent<Rigidbody>();
        }
        #endregion

        #region Main

        void Update()
        {
            IsSprinting = false;
            IsJumping = false;
            // Sprint
            if (!Input.GetKey(KeyCode.LeftShift)) 
            {
                _movementSpeed = _walkSpeedInput * Time.deltaTime;
                IsSprinting = false;
            }
            else 
            {
                _movementSpeed = _runSpeedInput * Time.deltaTime;
                IsSprinting = true;
            }

            // Move
            Vector3 movementVector = MoveTowardTarget(_input.moveTarget, _movementSpeed);

            // Rotate
            if (!_lookAtMouse) {RotateTowardTarget(movementVector, _turnSpeedInput);}
            else {RotateTowardMouse(_camera, _input.mousePos);}

            // Jump
            if (Input.GetKeyDown(KeyCode.Space) && !IsInAir) {Jump();}
            

        }
        #endregion

        #region MovementAndRotationMethods

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Ground")
            {
                IsInAir = false;
            }
        }

        private Vector3 MoveTowardTarget(Vector3 targetVector, float _speed)
        {
            //Kamerához relatív mozgás (kamera elfordulási szöge)
            targetVector = Quaternion.Euler(0, _camera.transform.rotation.eulerAngles.y, 0) * targetVector;

            _targetPosition = transform.position + targetVector * _speed;
            _velocity = Vector3.Distance(transform.position, _targetPosition) / Time.deltaTime;

            // A mozgatási input forgatása az origóban, az egérpozíciójához képest a playerhez
            AnimationDirection = Quaternion.Inverse(_relativeMouseRotation) * ((targetVector* _speed) / Time.deltaTime);

            transform.position = _targetPosition;
            return targetVector;
        }

        private void RotateTowardTarget(Vector3 targetVector, float turnSpeed)
        {
            if (targetVector.magnitude == 0 ) { return; }
            Quaternion rotation = Quaternion.LookRotation(targetVector);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, turnSpeed);
        }

        private void RotateTowardMouse(Camera cam,Vector3 mousePos)
        {
            LayerMask groundMask = ~7;
            Ray ray = cam.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 100f, groundMask))
            {
                if (hitInfo.collider.gameObject.CompareTag("Player")) { return; }

                Vector3 hitPoint = hitInfo.point;
                hitPoint.y = transform.position.y;
                transform.LookAt(hitPoint, Vector3.up);
                Vector3 relativeMousePosition = hitPoint - transform.position;
                _relativeMouseRotation = Quaternion.LookRotation( relativeMousePosition, Vector3.up);               
            }
        }

        private void Jump() 
        {

            _rigidBody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            IsInAir = true;
            IsJumping = true;

        }
        #endregion

    }


