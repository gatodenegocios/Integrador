using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class Projectile : MonoBehaviourPunCallbacks
{
	Rigidbody2D rb;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire(bool left, float vel){
    	if(rb == null)
    		rb = GetComponent<Rigidbody2D>();
    			
    	rb.velocity = new Vector2(left?-vel:vel,0);
    }

    [PunRPC]
    void RpcDestroy(){
    	PhotonNetwork.Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D col) {
    	if(!PhotonNetwork.IsMasterClient)
    		return;
    	
    	RpcDestroy();
    }



}
