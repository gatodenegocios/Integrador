using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
	public float jumpForce;
	bool canJump = true;
	public 	bool jumping = false;
	

	Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
    	rb = GetComponent<Rigidbody2D>();
    }

    public void JumpRelease(){
    	jumping = false;
    }

    public void JumpPress(){
    	if(!canJump)
    		return;

    	// Debug.DrawRay(new Vector2(transform.position.x+0.5f,transform.position.y), Vector2.down, Color.red);
        // Debug.DrawRay(new Vector2(transform.position.x-0.5f,transform.position.y), Vector2.down, Color.red);
        // Debug.DrawRay(transform.position, Vector2.down, Color.red);
    	RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x+0.5f,transform.position.y), Vector2.down, 1f);
        RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(transform.position.x-0.5f,transform.position.y), Vector2.down, 1f);
    	
    	if(hit.collider == null && hit2.collider == null)
    		return;

    	jumping = true;
    	rb.AddForce(Vector2.up*jumpForce, ForceMode2D.Impulse);

    	// canJump = false;
    }
}
