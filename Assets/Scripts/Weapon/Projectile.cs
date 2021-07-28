using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ��ҩ
/// </summary>
public class Projectile : MonoBehaviour
{
    public LayerMask collisionMask;
    public float damageValue;
    float speed = 10;
    // ��������
    float lifetime = 3;
    float skinWidth = 0.1f;

    private void Start()
    {
        Destroy(gameObject, lifetime);

        // ����ӵ��ڵ����ڲ����������߼�ⲻ���⵽
        Collider[] initialCollisions = Physics.OverlapSphere(transform.position, 0.1f, collisionMask);
        if(initialCollisions.Length > 0)
        {
            // ����һ����ײ����������˺�
            OnHitObject(initialCollisions[0]);
        }
    }

    /// <summary>
    /// �����ӵ��ٶȣ���Gun����
    /// </summary>
    /// <param name="newSpeed"></param>
    public void SetSpeed(float newSpeed)
    {   
        speed = newSpeed;
    }

    private void Update()
    {
        // ��һ֡�˶��ľ���
        float moveDistance = speed * Time.deltaTime;
        CheckCollisions(moveDistance);
        // �ӵ�ֻ��Ҫ��ǰ
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    /// <summary>
    /// ����Ƿ��Ƿ�����ײ
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
    /// ������ײ֮��
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
