using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSoul : MonoBehaviour
{
	public float velocity;
	// public float accelerationVelocity;
	// public float limitVelocity;
	// floar velocityX;
	// floar velocityY;
    // Start is called before the first frame update
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void MoveLeft(){
    	rb.velocity = new Vector2(-velocity,rb.velocity.y);
    }
    public void MoveRight(){
    	rb.velocity = new Vector2(velocity,rb.velocity.y);	
    }
    public void MoveUp(){
    	rb.velocity = new Vector2(rb.velocity.x,velocity);	
    }
    public void MoveDown(){
    	rb.velocity = new Vector2(rb.velocity.x,-velocity);		
    }

    public void SetPossess(GameObject hat){
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = new Vector2(0,0);
        rb = hat.GetComponent<Rigidbody2D>();
        // hat.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        
        velocity = (float) (velocity*0.5);
    }
}
