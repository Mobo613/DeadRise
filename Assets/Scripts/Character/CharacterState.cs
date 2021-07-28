using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : MonoBehaviour, IDamaged
{
    public float health;
    public bool living;
    public event System.Action OnDeath;

    protected void Start()
    {
        living = true;
    }

    public virtual void TakeDamage(float damageValue)
    {
        if (living == false) return;
        health -= damageValue;
        if (health <= 0)
        {
            Dead();
        }
    }

    public virtual void TakeHit(float damageValue, RaycastHit hit)
    {
        // Do some stuff here with hit
        TakeDamage(damageValue);
    }

    protected virtual void Dead()
    {
        living = false;
        if (OnDeath != null)
        {
            OnDeath();
        }
        print(this.gameObject.name + " is dead");
    }
}
