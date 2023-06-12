using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    public GameObject hitMarker;
    public int damageAmount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEntry2D(BoxCollider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealthController>().DamageEnemy(damageAmount);
        }

        if (other.tag == "Boss")
        {
            BossHealthController.instance.TakeDamage(damageAmount);
        }

        if (hitMarker != null)
        {
            Instantiate(hitMarker, transform.position, Quaternion.identity);
        }
    }
}
