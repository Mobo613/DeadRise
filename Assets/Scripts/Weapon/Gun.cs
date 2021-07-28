using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器
/// </summary>
public class Gun : MonoBehaviour
{
    // 枪口位置
    public Transform muzzle;
    // 使用弹药类型
    public Projectile projectile;
    // 射击频率
    public float msBetweenShots = 100;
    // 子弹初速度
    public float muzzleVelocity = 35;

    // 下次可以攻击的时间
    float nextShotTime;
    /// <summary>
    /// 射击
    /// </summary>
    public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            // 计算下次攻击的时间
            nextShotTime = Time.time + msBetweenShots / 1000;
            // 初始化子弹
            Projectile newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation);
            // 设置子弹速度
            newProjectile.SetSpeed(muzzleVelocity);
        }
    }
}
