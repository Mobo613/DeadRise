using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����
/// </summary>
public class Gun : MonoBehaviour
{
    // ǹ��λ��
    public Transform muzzle;
    // ʹ�õ�ҩ����
    public Projectile projectile;
    // ���Ƶ��
    public float msBetweenShots = 100;
    // �ӵ����ٶ�
    public float muzzleVelocity = 35;

    // �´ο��Թ�����ʱ��
    float nextShotTime;
    /// <summary>
    /// ���
    /// </summary>
    public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            // �����´ι�����ʱ��
            nextShotTime = Time.time + msBetweenShots / 1000;
            // ��ʼ���ӵ�
            Projectile newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation);
            // �����ӵ��ٶ�
            newProjectile.SetSpeed(muzzleVelocity);
        }
    }
}
