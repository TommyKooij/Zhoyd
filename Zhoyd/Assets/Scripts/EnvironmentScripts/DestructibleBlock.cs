using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBlock : MonoBehaviour
{
    public bool normalAttack, rocket;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (normalAttack)
        {
            if (other.tag == "Bullet" || other.tag == "Punch")
            {
                Destroy(gameObject);
            }

            if (other.tag == "Rocket")
            {
                Destroy(gameObject);
            }
        }

        if (rocket)
        {
            if (other.tag == "Rocket")
            {
                Destroy(gameObject);
            }
        }
    }
}
