using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyEffects : MonoBehaviour
{
    public float dashVelocity;
    public float dashDurability;
    public float dashReloadTime;
    bool canDash = true;
    public bool dashing = false;
    Rigidbody2D rb;
    public float forceHitter;
    bool freezed = false;
    // Player player;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // player = GetComponent<Player>();
    }

    public void Freeze(){
    	rb.constraints = RigidbodyConstraints2D.FreezePositionY|RigidbodyConstraints2D.FreezePositionX|RigidbodyConstraints2D.FreezeRotation;
        freezed = true;
    }

    public void Defreeze(){
    	rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
    	transform.eulerAngles = new Vector3(0f,0f,0f);
        freezed = false;
    }

    public void Dash(bool left){
        if(!freezed && canDash){
                float dVelocity = left? -dashVelocity:dashVelocity;
                rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                rb.velocity = new Vector2(dVelocity, 0);
                canDash = false;
                dashing = true;
                StartCoroutine(EndDash());
        }
    }
    IEnumerator EndDash(){
            yield return new WaitForSeconds(dashDurability);
            rb.velocity = new Vector2(0, 0);
            
            dashing = false;
            if(!freezed)
                Defreeze();

            yield return new WaitForSeconds(dashReloadTime);
            canDash = true;
    }

    public void HitterEffect(){
        rb.velocity = new Vector2(rb.velocity.x,forceHitter);//AddForce(Vector2.up*forceHitter, ForceMode2D.Impulse);
    }

    public void JumpEffect(){
        rb.velocity = new Vector2(rb.velocity.x,20f);//AddForce(Vector2.up*forceHitter, ForceMode2D.Impulse);   
    }



}
