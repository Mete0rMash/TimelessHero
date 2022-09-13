using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damagePoint = 1;
    public float pushForce = 10f;

    private float cooldown = 0.5f;
    private float lastSwing;
    private CharacterController cc;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player")) return;

        if (collision.transform.CompareTag("Enemy"))
        {
            Damage dmg = new Damage
            {
                damageAmount = damagePoint,
                origin = transform.position,
                pushForce = pushForce
            };

            collision.gameObject.SendMessage("ReceiveDamage", dmg);


        }
    }
}
