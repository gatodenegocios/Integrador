using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour
{
    public float fallMultiplayer = 2.5f;
    public float lowJumpMultiplayer = 2f;   

    Jump jump;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jump = GetComponent<Jump>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(rb.velocity.y < 0){
            rb.gravityScale = fallMultiplayer;
        }else if(rb.velocity.y > 0 && !jump.jumping){
            rb.gravityScale = lowJumpMultiplayer;
        }else{
            rb.gravityScale = 1f;
        }
        
    }
}
