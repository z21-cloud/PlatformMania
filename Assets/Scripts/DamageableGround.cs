using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DamageableGround : MonoBehaviour, IDamageable
{
    public void TakeDamage(float damage)
    {
        Debug.Log("Bullet destroyed");
        //effects
    }
}
