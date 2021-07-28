using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色武器控制
/// </summary>
public class WeapenController : MonoBehaviour
{
    // 玩家持枪位置
    public Transform weaponHold;
    // 初始枪支
    public Gun startingGun;
    
    // 已经佩戴的枪支
    Gun equippedGun;

    private void Start()
    {
        if (startingGun != null)
        {
            EquipeGun(startingGun);
        }
    }

    /// <summary>
    /// 装备枪
    /// </summary>
    /// <param name="gunToEquip"></param>
    public void EquipeGun(Gun gunToEquip)
    {
        // 如果有枪，则将现有的枪支销毁
        if (equippedGun != null)
        {
            Destroy(equippedGun.gameObject);
        }
        // 在玩家持枪位置实例化一个枪支
        equippedGun = Instantiate(gunToEquip, weaponHold.position, weaponHold.rotation);
        // 并将枪支作为持枪位置的子项
        equippedGun.transform.parent = weaponHold;
    }
    
    /// <summary>
    /// 调用枪支的射击功能
    /// </summary>
    public void Shoot()
    {
        if(equippedGun != null)
        {
            equippedGun.Shoot();
        }
    }
}
