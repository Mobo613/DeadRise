using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Íæ¼Ò×´Ì¬Àà
/// </summary>
public class PlayerState : CharacterState
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
