using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 敌人类
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyState))]
public class Enemy : MonoBehaviour
{ 
    public enum State { Idle, Chasing, Attacking };
    private State currentState;

    private NavMeshAgent pathfinder;
    private Transform target;
    private CharacterState targetEntity;
    private Material skinMaterial;
    // 敌人的颜色
    private Color originalColor;

    // 攻击相关参数
    private float attackDistanceThreshold = 0.5f;
    private float timeBetweenAttck = 1f;
    private float nextAttackTime;
    public float damageValue = 1;
    // 碰撞组件半径
    private float myCollisionRedius;
    private float targetCollisionRedius;

    private bool hasTarget;

    void Start()
    {
        // 获取组件
        pathfinder = GetComponent<NavMeshAgent>();
        skinMaterial = GetComponent<Renderer>().material;
        originalColor = skinMaterial.color;

        // 如果场景中存在Player
        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            hasTarget = true;
            currentState = State.Chasing;
            target = GameObject.FindGameObjectWithTag("Player").transform;
            targetEntity = target.GetComponent<CharacterState>();
            targetEntity.OnDeath += OnTargetDeath;

            myCollisionRedius = GetComponent<CapsuleCollider>().radius;
            targetCollisionRedius = target.GetComponent<CapsuleCollider>().radius;

            StartCoroutine(UpdatePath());
        }
    }


    private void Update()
    {
        if (!hasTarget) return; 
        if (Time.time > nextAttackTime)
        {
            nextAttackTime = Time.time + timeBetweenAttck;

            float sqrDstToTarget = (target.position - this.transform.position).sqrMagnitude;
            if (sqrDstToTarget < Mathf.Pow(attackDistanceThreshold + myCollisionRedius +targetCollisionRedius, 2))
            {
                StartCoroutine(Attack());
            }
        }
    }

    private void OnTargetDeath()
    {
        hasTarget = false;
        currentState = State.Idle;
    }

    IEnumerator Attack()
    {
        currentState = State.Attacking;
        pathfinder.enabled = false;

        Vector3 originalPosition = transform.position;
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        Vector3 targetPosition = target.position - dirToTarget * (myCollisionRedius);

        float attackSpeed = 3;
        float percent = 0;

        skinMaterial.color = Color.red;
        bool hasAppliedDamaged = false;

        while (percent <= 1)
        {
            if(percent > 0.5f && !hasAppliedDamaged)
            {
                hasAppliedDamaged = true;
                targetEntity.TakeDamage(damageValue);
            }
            percent += Time.deltaTime * attackSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector3.Lerp(originalPosition, targetPosition, interpolation);

            yield return null;
        }

        skinMaterial.color = originalColor;

        currentState = State.Chasing;
        pathfinder.enabled = true;
    }

    IEnumerator UpdatePath()
    {
        float refreshRate = 0.25f;
        EnemyState state = this.gameObject.GetComponent<EnemyState>();

        while(hasTarget)
        {
            if(currentState == State.Chasing)
            {
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                Vector3 targetPostion = target.position - dirToTarget * (myCollisionRedius + targetCollisionRedius + attackDistanceThreshold/2);
                pathfinder.SetDestination(targetPostion);
                //print(state.living);
                //if (!state.living)
                //{
                //    pathfinder.SetDestination(targetPostion);
                //}
            }
            yield return new WaitForSeconds(refreshRate);
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
