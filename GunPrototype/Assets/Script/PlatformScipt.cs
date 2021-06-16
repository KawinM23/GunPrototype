using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScipt : MonoBehaviour
{

    private GameObject player;
    private BoxCollider2D playerBc;
    private BoxCollider2D bc;

    private float distance;
    public bool abovePlatform;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerBc = player.GetComponent<BoxCollider2D>();
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = (transform.position.y - transform.localScale.y) - (player.transform.position.y - player.transform.localScale.y);
        if (distance < 0 && !abovePlatform) { //above
            Physics2D.IgnoreCollision(playerBc, bc, false);
            abovePlatform = true;
        } else if (distance > 2 && abovePlatform) {
            Physics2D.IgnoreCollision(playerBc, bc, true);
            abovePlatform = false;
        }
    }
}
