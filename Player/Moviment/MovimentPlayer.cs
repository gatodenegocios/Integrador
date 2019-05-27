using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentPlayer : MonoBehaviour
{
	Rigidbody2D rb;
	public float vel;

    //public bool moving = false;
    //public bool left = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
    }

    public void MoveLeft(){
    	rb.velocity = new Vector2(-vel,rb.velocity.y);
    	// rb.AddForce(Vector2.left*vel, ForceMode2D.Impulse);
    }

    public void MoveRight(){
    	rb.velocity = new Vector2(vel,rb.velocity.y);	
    	// rb.AddForce(Vector2.left*v, ForceMode2D.Impulse);
    }

    public void StopMove(){
    	rb.velocity = new Vector2(0,rb.velocity.y);	
    }

    // public void Jump(){
    	
    // }

    // public void Velocity(){

    // }
}
