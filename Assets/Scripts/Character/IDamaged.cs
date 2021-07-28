using UnityEngine;

public interface IDamaged
{
    void TakeDamage(float damageValue);

    void TakeHit(float damageValue, RaycastHit hit);
}
