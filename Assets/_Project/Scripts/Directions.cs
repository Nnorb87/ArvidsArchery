using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Directions : MonoBehaviour
    {
    
        public Transform otherCube;
        private Vector3 direction;
        public Vector3 cube2WorldCoordinate;
        public Vector3 cube2LocalCoordinate;
        //public Vector3 cube2SelfCoordinate;



        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            direction = (otherCube.position - transform.position);

            cube2WorldCoordinate = otherCube.position;
            cube2LocalCoordinate = transform.InverseTransformPoint (cube2WorldCoordinate);
           //cube2SelfCoordinate = transform.position + cube2WorldCoordinate;

            // direction.y = 0;
            // Debug.Log(direction);
            Debug.DrawRay( transform.position, direction,  Color.red);
 //           Debug.DrawLine(transform.position, otherCube.position, Color.yellow);
            Vector3 right =  Quaternion.AngleAxis(90,Vector3.up) * direction;
            Debug.DrawRay(transform.position, right, Color.blue);
            //transform.rotation = Quaternion.LookRotation(direction,Vector3.up);
        }
           
    }
