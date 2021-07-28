using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ½ÇÉ«ÊäÈë¿ØÖÆ
/// </summary>
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(WeapenController))]
[RequireComponent(typeof(PlayerState))]
public class PlayerInput : MonoBehaviour
{
    PlayerMovement movement;
    WeapenController weapen;
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        weapen = GetComponent<WeapenController>();
    }

    void Update()
    {
        Move();
        LookAt();
        Shoot();
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(h, 0, v).normalized;
        movement.Move(direction);
    }

    private void LookAt()
    {
        movement.LookAt(Input.mousePosition);
    }

    private void Shoot()
    {
        if (Input.GetMouseButton(0))
        {
            weapen.Shoot();
        }
    }
}
