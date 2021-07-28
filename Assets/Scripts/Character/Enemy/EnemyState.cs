using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// µÐÈË×´Ì¬Àà
/// </summary>
public class EnemyState : CharacterState
{
    protected void Start()
    {
        base.Start();
        print(living);

    }

    protected override void Dead()
    {
        base.Dead();
        Destroy(this.gameObject);
    }
}
