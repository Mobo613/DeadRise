using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ɫ��������
/// </summary>
public class WeapenController : MonoBehaviour
{
    // ��ҳ�ǹλ��
    public Transform weaponHold;
    // ��ʼǹ֧
    public Gun startingGun;
    
    // �Ѿ������ǹ֧
    Gun equippedGun;

    private void Start()
    {
        if (startingGun != null)
        {
            EquipeGun(startingGun);
        }
    }

    /// <summary>
    /// װ��ǹ
    /// </summary>
    /// <param name="gunToEquip"></param>
    public void EquipeGun(Gun gunToEquip)
    {
        // �����ǹ�������е�ǹ֧����
        if (equippedGun != null)
        {
            Destroy(equippedGun.gameObject);
        }
        // ����ҳ�ǹλ��ʵ����һ��ǹ֧
        equippedGun = Instantiate(gunToEquip, weaponHold.position, weaponHold.rotation);
        // ����ǹ֧��Ϊ��ǹλ�õ�����
        equippedGun.transform.parent = weaponHold;
    }
    
    /// <summary>
    /// ����ǹ֧���������
    /// </summary>
    public void Shoot()
    {
        if(equippedGun != null)
        {
            equippedGun.Shoot();
        }
    }
}
