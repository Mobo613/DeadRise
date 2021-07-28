using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 角色移动
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
    /// 角色移动具体实现
    /// </summary>
    /// <param name="direction"></param>
    public void Move(Vector3 direction)
    {
        velocity = direction * speed;
    }

    /// <summary>
    /// 角色看向鼠标位置
    /// </summary>
    /// <param name="mousePosition"></param>
    public void LookAt(Vector3 mousePosition)
    {
        // 从摄像机向鼠标位置发射一条射线
        Ray ray = viewCamera.ScreenPointToRay(mousePosition);
        // 代码创建一个法向量向上的平面用来拦截射线，位置在原点
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        
        // 后面需要用到
        float rayDistance;
        // 如果平台与射线相交
        if(groundPlane.Raycast(ray, out rayDistance))
        {
            // 获取射线与平台的焦点
            Vector3 point = ray.GetPoint(rayDistance);
            //Debug.DrawLine(ray.origin, point, Color.red);
            point = new Vector3(point.x, transform.position.y, point.z);
            transform.LookAt(point);
        }
    }
}
