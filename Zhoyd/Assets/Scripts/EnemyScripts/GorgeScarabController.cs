using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorgeScarabController : MonoBehaviour
{
    public float rangeToEmerge, rangeToChase;
    private bool isEmerged, isChasing;

    public float moveSpeed;

    private Transform player;

    public Rigidbody2D theRB;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerHealthController.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Before enemy is initialized
        /*if (!isEmerged)
        {
            if (Vector3.Distance(transform.position, player.position) < rangeToEmerge)
            {
                isEmerged = true;

                anim.SetBool("gorgeScarabEmerging", isEmerged);
            }
        }
        else
        {

        }*/

        //When enemy can walk on ground
        if (!isChasing)
        {
            if (Vector3.Distance(transform.position, player.position) < rangeToChase)
            {
                isChasing = true;

                anim.SetBool("isChasing", isChasing);
            }
        }
        else
        {
            if (player.gameObject.activeSelf)
            {
                transform.position += transform.right * moveSpeed * Time.deltaTime;
            }
        }
    }
}
