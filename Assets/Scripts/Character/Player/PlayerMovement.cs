using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ��ɫ�ƶ�
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float speed;

    Vector3 velocity;
    Rigidbody myRigid;
    Camera viewCamera;
    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
        viewCamera = Camera.main;
    }


    private void FixedUpdate()
    {
        myRigid.MovePosition(myRigid.position + velocity * Time.fixedDeltaTime);
    }

    /// <summary>
    /// ��ɫ�ƶ�����ʵ��
    /// </summary>
    /// <param name="direction"></param>
    public void Move(Vector3 direction)
    {
        velocity = direction * speed;
    }

    /// <summary>
    /// ��ɫ�������λ��
    /// </summary>
    /// <param name="mousePosition"></param>
    public void LookAt(Vector3 mousePosition)
    {
        // ������������λ�÷���һ������
        Ray ray = viewCamera.ScreenPointToRay(mousePosition);
        // ���봴��һ�����������ϵ�ƽ�������������ߣ�λ����ԭ��
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        
        // ������Ҫ�õ�
        float rayDistance;
        // ���ƽ̨�������ཻ
        if(groundPlane.Raycast(ray, out rayDistance))
        {
            // ��ȡ������ƽ̨�Ľ���
            Vector3 point = ray.GetPoint(rayDistance);
            //Debug.DrawLine(ray.origin, point, Color.red);
            point = new Vector3(point.x, transform.position.y, point.z);
            transform.LookAt(point);
        }
    }
}
