﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] int maxHP;
    [SerializeField] int damage;

    int hp;
    [HideInInspector] public bool isAlive = true;

    Animator animator;
    [HideInInspector] public Vector3 target;
    [HideInInspector] public Vector3 facingDirection = Vector3.zero;

    [SerializeField] AudioSource attackFX;

    [SerializeField] AudioClip dead;

    Stack<PathFind.Point> currentPath = new Stack<PathFind.Point>();

    [HideInInspector] public bool attacking;
    GameObject enemyToAttack;

    float stoppingDistance;
    float attackStopDist = .8f;
    float moveStopDist = .15f;

    float attackTimer = 0.0f;

    void Start()
    {
        isAlive = true;
        attacking = false;
        hp = maxHP;
        animator = GetComponent<Animator>();
        target = this.transform.position;
        stoppingDistance = Vector3.Distance(transform.position, target);
    }

    void Update()
    {
        if (isAlive)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target);

            distanceToTarget = HandleMoving(distanceToTarget);

            distanceToTarget = HandleAttacking(distanceToTarget);

            SetSpriteDirection();

            if (distanceToTarget > stoppingDistance)
                transform.position = Vector3.MoveTowards(transform.position, target, 1.5f * Time.deltaTime);

            if (animator.GetBool("Attack"))
                DoDamage();

            attackTimer += Time.deltaTime;
        }
        else
        {
            StartCoroutine(Delete());
        }
    }

    float HandleMoving(float distanceToTarget)
    {
        if (currentPath.Count > 1 && distanceToTarget <= moveStopDist)
        {
            if (attacking && LayerMask.LayerToName(enemyToAttack.layer) != "Building")
                StartMove(enemyToAttack.transform.position, true);
            stoppingDistance = moveStopDist;
            if (currentPath.Count > 0) currentPath.Pop();
            facingDirection = (target - this.transform.position).normalized;
            if (currentPath.Count > 0)
                target = new Vector3(currentPath.Peek().x, currentPath.Peek().y, transform.position.z);
            distanceToTarget = Vector3.Distance(transform.position, target);
        }
        else if (attacking == false && distanceToTarget < moveStopDist)
        {
            stoppingDistance = moveStopDist;
            animator.SetBool("Moving", false);
        }
        return distanceToTarget;
    }

    float HandleAttacking(float distanceToTarget)
    {
        if (!enemyToAttack) attacking = false;
        if (attacking && Vector3.Distance(transform.position, enemyToAttack.transform.position) <= attackStopDist)
        {
            StartAttack();
        }
        else if (attacking && LayerMask.LayerToName(enemyToAttack.layer) == "Building" && distanceToTarget < moveStopDist)
        {
            target = enemyToAttack.transform.position;
            StartAttack();
        }
        return distanceToTarget;
    }

    void SetSpriteDirection()
    {
        facingDirection = (target - this.transform.position).normalized;
        if (facingDirection.x < 0)
            GetComponent<SpriteRenderer>().flipX = true;
        else
            GetComponent<SpriteRenderer>().flipX = false;
        if (animator.GetBool("Attack"))
            facingDirection = (enemyToAttack.transform.position - this.transform.position).normalized;
        if (facingDirection.x != 0 || facingDirection.y != 0)
        {
            animator.SetFloat("XDir", facingDirection.x);
            animator.SetFloat("YDir", facingDirection.y);
        }
    }

    void DoDamage()
    {
        if (enemyToAttack.tag == "Enemy" || enemyToAttack.tag == "Player")
        {
            if (attackTimer > 1.0f)
                enemyToAttack.GetComponent<CharacterController>().ReceiveDamage(damage);
            if (enemyToAttack.GetComponent<CharacterController>().isAlive == false)
                StopAttack();
        }
        else if (LayerMask.LayerToName(enemyToAttack.layer) == "Building")
        {
            if (attackTimer > 1.0f)
                enemyToAttack.GetComponent<Building>().ReceiveDamage(damage);
            if (enemyToAttack.GetComponent<Building>().isAlive == false)
                StopAttack();
        }
        if (attackTimer > 1.0f)
        {
            attackFX.PlayOneShot(attackFX.clip);
            attackTimer = 0.0f;
        }
    }

    void StopAttack()
    {
        attacking = false;
        animator.SetBool("Attack", false);
        animator.SetBool("Moving", false);

        if (this.tag == "Enemy")
            GetComponent<EnemyAI>().inactive = true;
    }

    void StartAttack()
    {
        stoppingDistance = attackStopDist;
        animator.SetBool("Attack", true);
        animator.SetBool("Moving", false);
    }

    public void OnSelect()
    {
        GetComponent<SpriteOutline>().enabled = true;
        GetComponent<SpriteRenderer>().sortingOrder = 2;
    }

    public void OnDeselect()
    {
        GetComponent<SpriteRenderer>().sortingOrder = 0;
        GetComponent<SpriteOutline>().enabled = false;
    }

    public void StartMove(Vector3 tilePos, bool attackMove = false)
    {
        currentPath.Clear();
        currentPath = Navigator.GetPath((int)transform.position.x, (int)-transform.position.y,
                                        (int)tilePos.x, (int)-tilePos.y);
        if (currentPath.Count > 0)
        {
            target = new Vector3(currentPath.Peek().x, currentPath.Peek().y, this.transform.position.z);
            animator.SetBool("Moving", true);
        }
        if (!attackMove)
        {
            animator.SetBool("Attack", false);
            attacking = false;
            stoppingDistance = moveStopDist;
            attackTimer = 0.0f;
        }
    }

    public void Attack(GameObject enemyTarget)
    {
        if (LayerMask.LayerToName(enemyTarget.layer) == "Building")
            attackStopDist = 1.5f;
        else
            attackStopDist = 1.0f;
        enemyToAttack = enemyTarget;
        StartMove(enemyTarget.transform.position, false);
        attacking = true;
    }

    public void ReceiveDamage(int damage)
    {
        hp -= damage;

        Debug.Log(transform.name + "[" + hp + "/" + maxHP + "]HP has been attacked.");

        if (hp <= 0)
        {
            if (tag == "Player")
            {
                GetComponent<SpriteOutline>().enabled = false;
                UnitManager.RemoveUnit(gameObject);
            }

            attackFX.PlayOneShot(dead);
            isAlive = false;
            animator.SetTrigger("Die");
        }
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(2.2f);
        Destroy(this.gameObject);
    }
}
