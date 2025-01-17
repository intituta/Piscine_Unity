﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
    public GameObject player;
    private PlayerController playerController;
    private NavMeshAgent navMeshAgent;
    private Animator animator;

    // enemy stat
    [SerializeField] private float hp;
    [SerializeField] private float damage;
    [SerializeField] private float detectRange;
    [SerializeField] private float attackRange;
    [SerializeField] private bool isKilled;
    [SerializeField] private bool isAttacked;
    [SerializeField] private bool isPlayerDetected;

    private Vector3 lastPosition;

    public float Damage{ get { return damage; } set { damage = value; }}
    public bool IsKilled { get { return isKilled; } }
    public bool IsAttacked { get { return isAttacked; } }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        lastPosition = player.transform.position;
        navMeshAgent.SetDestination(player.transform.position);
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (!isKilled && !isAttacked)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance <= detectRange)
            {
                lastPosition = player.transform.position;
                navMeshAgent.SetDestination(player.transform.position);
                navMeshAgent.isStopped = false;
                isPlayerDetected = true;
            }
            if (distance <= attackRange)
            {
                transform.LookAt(player.transform.position);
                navMeshAgent.isStopped = true;
                animator.SetBool("attack", true);
            }
            else
                animator.SetBool("attack", false);
            if (distance > detectRange)
            {
                navMeshAgent.SetDestination(lastPosition);
                navMeshAgent.isStopped = false;
                isPlayerDetected = false;
            }
            if (navMeshAgent.isStopped)
                animator.SetBool("running", false);
            else
                animator.SetBool("running", true);
        }
    }

    private IEnumerator Attacked(float damaged)
    {
        if (!isKilled)
        {
            if (!isPlayerDetected)
            {
                lastPosition = player.transform.position;
                navMeshAgent.SetDestination(player.transform.position);
                navMeshAgent.isStopped = false;
                isPlayerDetected = true;
            }
            hp -= damaged;
            if (hp <= 0)
            {
                isKilled = true;
                navMeshAgent.isStopped = true;
                animator.SetTrigger("death");
                yield return new WaitForSeconds(2.5f);
                Destroy(this.gameObject);
            }
            else
            {
                isAttacked = true;
                animator.SetTrigger("attacked");
                yield return new WaitForSeconds(0.5f);
                isAttacked = false;
            }
        }
    }

    public void Attack()
    {
        playerController.Attacked(damage);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Bullet")
        {
            StartCoroutine(Attacked(collision.gameObject.GetComponent<Bullet>().damage));
        }
    }
}
