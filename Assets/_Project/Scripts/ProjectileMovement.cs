using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class ProjectileMovement : MonoBehaviour
    {
    [SerializeField]
    private float _speed = 1f;

    private bool _inactiveProjectile;        
    private GameObject _spawnedProjectile;        
    private ProjectileMovement _spawnedScript;
    private Rigidbody _spawnedRigidbody;
    private Collider _spawnedCollider;
    private Transform arrowParent;
    //private bool needScaling = false;

    void Update()
    {
        if (!_inactiveProjectile)
        {
            transform.Translate(0, 0, _speed * Time.deltaTime);
        }
        Destroy(gameObject, 2f);

    }

    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.TryGetComponent<EnemyAI>(out EnemyAI enemyAI)) {
            enemyAI.Kill();
            arrowParent = enemyAI.GetArrowParent();
            //needScaling = true;
        } else {
            arrowParent = collision.gameObject.transform;
        }

        Vector3 spawnLoc = collision.GetContact(0).point;
        Destroy(gameObject);                     

        _spawnedProjectile = Instantiate(gameObject,spawnLoc,transform.rotation,arrowParent);

        // calculate neccesary scale correction
        Vector3 parentScale = arrowParent.lossyScale;
        Vector3 inverseParentScale = new Vector3(1f / parentScale.x, 1f / parentScale.y, 1f / parentScale.z);
        Vector3 scaleCorrection = Vector3.Scale(_spawnedProjectile.transform.localScale, inverseParentScale);


        // rescale the spawned arrow
        _spawnedProjectile.transform.localScale = scaleCorrection;


        // rescale the spawned projectile because the model has a 0,01 scale
        //Debug.Log(needScaling);
        //if (needScaling) {
        //    _spawnedProjectile.transform.localScale = new Vector3(100f, 100f, 100f);
        //    needScaling = false;
        //}



        _spawnedScript = _spawnedProjectile.GetComponent<ProjectileMovement>();
        _spawnedRigidbody = _spawnedProjectile.GetComponent<Rigidbody>();
        _spawnedCollider = _spawnedProjectile.GetComponent<BoxCollider>();

        _spawnedScript.enabled = true;
        _spawnedScript._inactiveProjectile = true;
        Object.Destroy(_spawnedCollider);
        Object.Destroy(_spawnedRigidbody);

     }
    }
