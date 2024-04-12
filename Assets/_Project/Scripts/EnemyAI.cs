using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour    {

    private const string DEAD = "Dead";
    private const string GATHERING = "Gathering";
    private const string RUN = "Run";

    [SerializeField] private float lootFrequency;
    [SerializeField] private int killScore = 100;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform arrowParent;

    private Transform target;
    private NavMeshAgent navmeshAgent;
    private float lootTimer;
    private bool dead = false;
    private new Collider collider;


    private void Awake() {
        navmeshAgent = GetComponent<NavMeshAgent>();      
        collider = GetComponent<Collider>();
    }

    private void Start() {
        target = GameManager.Instance.GetTreasureLocation();
        navmeshAgent.SetDestination(target.position);
        GameManager.Instance.OnHalfTime += GameManager_OnHalfTime;
        lootTimer = lootFrequency;
    }

    private void GameManager_OnHalfTime(object sender, EventArgs e) {
        if (navmeshAgent != null) {
            navmeshAgent.speed = 4;
            animator.SetBool(RUN, true);
        }
    }

    private void Update() {

        // handle if we reach the treasure
        if ((target.position - transform.position).magnitude <= navmeshAgent.stoppingDistance) {
            if (navmeshAgent.isActiveAndEnabled) {
                navmeshAgent.enabled = false;
            }
            CollectingTreasure();
        }

        // handle if game paused
        if (InputHandler.Instance.GetGamePaused()) {
            navmeshAgent.enabled=false;
        } else if (!navmeshAgent.isActiveAndEnabled && !dead) { 
            navmeshAgent.enabled=true;
            navmeshAgent.SetDestination(target.position);
        }

    }

    private void CollectingTreasure() {
        if (!animator.GetBool(GATHERING)) {
            animator.SetBool(GATHERING, true);
        }
        lootTimer -= Time.deltaTime;
        if (lootTimer <= 0f)
        {
            Debug.Log("I am collecting the treasure");
            GameManager.Instance.LootTreasure();
            lootTimer = lootFrequency;
        }

    }

    public void Kill() {
        collider.enabled = false;
        dead = true;
        GameManager.Instance.OnHalfTime -= GameManager_OnHalfTime;
        navmeshAgent.enabled = false;
        GameManager.Instance.SetScoreCount(killScore);
        //animator.SetBool(WALK, false);
        animator.SetBool(DEAD, true);
        StartCoroutine(BodyDisappearTimer()); 
    }

    float bodyDecayTime = 5f;
    private IEnumerator BodyDisappearTimer() {
        yield return new WaitForSeconds(bodyDecayTime);
        GameObject.Destroy(this.gameObject);
    }

    public Transform GetArrowParent() {
        return arrowParent;
    }
}
