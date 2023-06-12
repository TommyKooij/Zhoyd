using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchController : MonoBehaviour
{
#region VARIABLES
    public Rigidbody2D theRB;

    public GameObject hitMarker;
    public int damageAmount = 1;

    public float punchDelay;
    private float punchCounter;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        punchCounter = punchDelay;
    }

    // Update is called once per frame
    void Update()
    {
        punchCounter -= Time.deltaTime;
        if (punchCounter <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
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

        Destroy(gameObject);
    }
}
