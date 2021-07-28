using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 弹药
/// </summary>
public class Projectile : MonoBehaviour
{
    public LayerMask collisionMask;
    public float damageValue;
    float speed = 10;
    // 生命周期
    float lifetime = 3;
    float skinWidth = 0.1f;

    private void Start()
    {
        Destroy(gameObject, lifetime);

        // 如果子弹在敌人内部产生，射线检测不会检测到
        Collider[] initialCollisions = Physics.OverlapSphere(transform.position, 0.1f, collisionMask);
        if(initialCollisions.Length > 0)
        {
            // 给第一个碰撞的物体造成伤害
            OnHitObject(initialCollisions[0]);
        }
    }

    /// <summary>
    /// 设置子弹速度，由Gun调用
    /// </summary>
    /// <param name="newSpeed"></param>
    public void SetSpeed(float newSpeed)
    {   
        speed = newSpeed;
    }

    private void Update()
    {
        // 这一帧运动的距离
        float moveDistance = speed * Time.deltaTime;
        CheckCollisions(moveDistance);
        // 子弹只需要向前
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    /// <summary>
    /// 检查是否是否发生碰撞
    /// </summary>
    /// <param name="moveDistance"></param>
    private void CheckCollisions(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        //Debug.DrawRay(transform.position, transform.forward, Color.red);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, moveDistance + skinWidth, collisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitObject(hit);
        }
    }

    /// <summary>
    /// 发生碰撞之后
    /// </summary>
    /// <param name="hit"></param>
    private void OnHitObject(RaycastHit hit)
    {
        print(hit.transform.gameObject.name);
        IDamaged damagedObj = hit.collider.GetComponent<IDamaged>();
        if(damagedObj != null)
        {
            damagedObj.TakeHit(damageValue, hit);
            Destroy(gameObject);
        }
    }

    private void OnHitObject(Collider c)
    {
        IDamaged damagedObj = c.GetComponent<IDamaged>();
        if (damagedObj != null)
        {
            damagedObj.TakeDamage(damageValue);
            Destroy(gameObject);
        }
    }
}
